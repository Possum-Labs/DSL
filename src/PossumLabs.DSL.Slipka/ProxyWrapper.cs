using GraphQL;
using GraphQL.Client;
using GraphQL.Client.Http;
using PossumLabs.DSL.Core;
using PossumLabs.DSL.Core.Variables;
using PossumLabs.DSL.Slipka.ValueObjects;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GraphQL.Client.Serializer.Newtonsoft;

namespace PossumLabs.DSL.Slipka
{
    public class ProxyWrapper : IEntity
    {
        public ProxyWrapper(Uri host, Uri destination, TimeSpan? openFor = null, TimeSpan? retainedFor =  null) : this(host)
        {
            Open(destination, openFor, retainedFor);
        }

        public ProxyWrapper(Uri host)
        {
            AdministrationUri = host;
            AdministrationClient = new RestClient(host);
            GraphQLHttpClient = new GraphQLHttpClient($"{host}graphql", new NewtonsoftJsonSerializer());
        }

        private Uri AdministrationUri { get; }
        private RestClient AdministrationClient { get; }
        private RestClient ProxyClient { get; set; }
        private SessionSummary ProxySession { get; set; }
        private GraphQLHttpClient GraphQLHttpClient { get;}

        public Uri ProxyUri { get; private set; }
        public string Id { get => ProxySession.Id; }

        public void Open(Uri destination, TimeSpan? openFor = null, TimeSpan? retainedFor = null)
        {
            ProxySession = new SessionSummary
            {
                TargetHost = destination.Host,
                TargetPort = destination.Port,
                OpenFor = openFor.HasValue ? openFor.ToString() : null,
                RetainedFor = retainedFor.HasValue ? retainedFor.ToString() : null
            };

            var request = new RestRequest("/api/proxies", Method.Post)
            {
                RequestFormat = DataFormat.Json
            };
            request.AddJsonBody(ProxySession);
            var response = AdministrationClient.Execute<SessionSummary>(request);
            if (!response.IsSuccessful)
                throw new Exception($"Was unable to open the proxy, error was {response.StatusCode} {response.StatusDescription}");
            ProxySession = response.Data;
            ProxyUri = new Uri($"http://{AdministrationUri.Host}:{ProxySession.ProxyPort}");
            ProxyClient = new RestClient(ProxyUri);
        }

        public void LogsResponsesOfType(string type, string value)
        {
            var request = new RestRequest($"/api/proxies/{ProxySession.Id}/record", Method.Put);
            var call = new Call
            {
                Response = new Message
                {
                    Headers = new List<Header>()
                }
            };
            call.Response.Headers.Add(new Header(type, new List<string>() { value }));
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(call);
            AdministrationClient.Execute(request);
        }

        private void Execute(RestRequest request)
        {
            var response = AdministrationClient.Execute(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new InvalidOperationException(response.StatusCode.ToString());
        }

        public void LogsCallsTo(Uri uri)
        {
            var request = new RestRequest($"/api/proxies/{ProxySession.Id}/record", Method.Put);
            var call = new Call
            {
                Uri = uri
            };
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(call);
            Execute(request);
        }

        public void RegisterTag(CallTemplate call)
        {
            var request = new RestRequest($"/api/proxies/{ProxySession.Id}/tag", Method.Put)
            {
                RequestFormat = DataFormat.Json
            };
            request.AddJsonBody(call);
            Execute(request);
        }

        public void RegisterRecording(CallTemplate call)
        {
            var request = new RestRequest($"/api/proxies/{ProxySession.Id}/record", Method.Put)
            {
                RequestFormat = DataFormat.Json
            };
            request.AddJsonBody(call);
            Execute(request);
        }

        public void RegisterInject(CallTemplate call)
        {
            var request = new RestRequest($"/api/proxies/{ProxySession.Id}/inject", Method.Put)
            {
                RequestFormat = DataFormat.Json
            };
            request.AddJsonBody(call);
            Execute(request);
        }

        public void RegisterDecoration(Header header)
        {
            var request = new RestRequest($"/api/proxies/{ProxySession.Id}/decorate", Method.Put)
            {
                RequestFormat = DataFormat.Json
            };
            request.AddJsonBody(header);
            Execute(request);
        }

        public Session GetSession()
        {
            var paramaters = $"sessionId: \"{ProxySession.Id}\"";
            var queury = new GraphQLRequest
            {
                Query = @"
{
    session("+ paramaters + @") {
    id
    name
    targetPort
    tags
    calls {
        recorded
        response {
            content
            }
        }
    }
}
"
            };
            var task = GraphQLHttpClient.SendQueryAsync<Session>(queury);
            var graphQLResponse = task.Result;
            return graphQLResponse.Data; 
        }

        public CallCollection GetCalls(bool? recorded = null, string tag = null)
        {
            var paramaters = $"sessionId: \"{ProxySession.Id}\"";
            if (recorded.HasValue)
                paramaters += $" recorded: {recorded.ToString().ToLower()}";
            if (tag != null)
                paramaters += $" tag: \"{tag}\"";
            var queury = new GraphQLRequest
            {
                Query = @"
{
    calls(" + paramaters + @") {
        duration
        recorded
        injected
        statusCode
        method
        path
        uri
        tags
        request {
            content
            headers {
                key
                values
            }
        }
        response {
            content
            headers {
                key
                values
            }
        }
    }
}
"
            };
            var task = GraphQLHttpClient.SendQueryAsync<GetCallsRet>(queury);
            var graphQLResponse = task.Result;
            return new CallCollection(graphQLResponse.Data?.Data ?? new CallRecord[0]);
        }

        class GetCallsRet { public CallRecord[] Data; }

        public void CloseAsync()
        {
            if (ProxySession == null)
                return;
            AdministrationClient.ExecuteAsync(new RestRequest(
                $"/api/proxies/{ProxySession.Id}",
                Method.Delete));
        }

        public void Close()
        {
            if (ProxySession == null)
                return;
            AdministrationClient.Execute(new RestRequest(
                $"/api/proxies/{ProxySession.Id}",
                Method.Delete));
        }

        public RestResponse Call(string path, Method method)
        {
            var request = new RestRequest(path, method)
            {
                RequestFormat = DataFormat.Json
            };
            request.AddJsonBody(ProxySession);
            return AdministrationClient.Execute(request);
        }

        public RestResponse<T> Call<T>(string path, Method method) where T : new()
        {
            var request = new RestRequest(path, method)
            {
                RequestFormat = DataFormat.Json
            };
            request.AddJsonBody(ProxySession);
            return AdministrationClient.Execute<T>(request);
        }

        public byte[] DownloadRequest(int number)
            => AdministrationClient.DownloadData(new RestRequest($"/api/sessions/{ProxySession.Id}/request/{number}"));

        public byte[] DownloadResponse(int number)
            => AdministrationClient.DownloadData(new RestRequest($"/api/sessions/{ProxySession.Id}/response/{number}"));

        public string LogFormat()
            => ProxySession.Id;
    }
}
