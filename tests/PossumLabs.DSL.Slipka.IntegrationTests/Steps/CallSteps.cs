using BoDi;
using PossumLabs.DSL.Core;
using PossumLabs.DSL.Slipka;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace PossumLabs.DSL.Slipka.IntegrationTests
{
    [Binding]
    sealed public class CallSteps : RepositoryStepBase<Call>, IDisposable
    {
        public CallSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
            Handler = new HttpClientHandler { AllowAutoRedirect = false, UseCookies = false };
            Client = new HttpClient(Handler);
        }

        private HttpClientHandler Handler { get; }
        private HttpClient Client { get; }

        [Given(@"the Calls?")]
        public void GivenTheCalls(Dictionary<string, Call> calls)
            => calls.Keys.ToList().ForEach(k => Add(k, calls[k]));

        [Given(@"the headers to the Call '(.*)'")]
        public void GivenTheCallHasHeaders(Call call, List<Header> headers)
            => call.Request.Headers.AddRange(headers);


        [When(@"the Call '(.*)' is executed")]

        [When(@"the Call '(.*)' is executed")]
        public void WhenTheCallIsExecuted(Call c)
        {
            Executor.Execute(()=>ExecuteCall(c));
        }

        private void ExecuteCall(Call c)
        {
            Stopwatch stopWatch = new Stopwatch();

            HttpRequestMessage request = new HttpRequestMessage();
            if (c.Request != null)
            {
                foreach (var h in c.Request.Headers)
                    request.Headers.Add(h.Key, h.Values);

                if (c.Request.Content != null)
                {
                    request.Content = new StringContent(c.Request.Content);
                }
            }

            switch(c.Method)
            {
                case "GET":
                    request.Method = HttpMethod.Get;
                    break;
                case "PUT":
                    request.Method = HttpMethod.Put;
                    break;
                case "POST":
                    request.Method = HttpMethod.Post;
                    break;
                case "DELETE":
                    request.Method = HttpMethod.Delete;
                    break;
                case "HEAD":
                    request.Method = HttpMethod.Head;
                    break;
                case "OPTIONS":
                    request.Method = HttpMethod.Options;
                    break;
                case "TRACE":
                    request.Method = HttpMethod.Trace;
                    break;
                default:
                    throw new Exception($"can't map method {c.Method} please use GET, PUT, POST, DELETE, HEAD, OPTIONS, TRACE");
            }

            request.RequestUri = c.Uri;

            stopWatch.Start();
            var response = Client.SendAsync(request).Result;
            stopWatch.Stop();

            c.StatusCode = ((int)response.StatusCode).ToString();
            c.Duration = stopWatch.ElapsedMilliseconds;

            c.StatusCode = ((int)response.StatusCode).ToString();

            c.Response = new Message()
            {
                Headers = response.Headers.Select(h => new Header(h.Key, new List<string>() { h.Value.ToString() })).ToList()
            };

            if (response.Content != null)
            {
                c.Response.Content = response.Content.ReadAsStringAsync().Result;
            }
        }

        public void Dispose()
        {
            Handler.Dispose();
            Client.Dispose();
        }
    }
}
