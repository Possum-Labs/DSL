﻿using BoDi;
using DSL.Documentation.Example;
using FluentAssertions;
using PossumLabs.DSL;
using PossumLabs.DSL.Core;
using PossumLabs.DSL.Core.Variables;
using PossumLabs.DSL.Slipka;
using PossumLabs.DSL.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using TechTalk.SpecFlow;

namespace DSL.Documentation.Example
{
    public class ProxyFile : IEntity
    {
        public MemoryStream Stream { get; internal set; }

        public string LogFormat()
        => "it's a file";
    }

    [Binding]
    public class SlipkaSteps : RepositoryStepBase<ProxyFile>
    {
        public SlipkaSteps(
            IObjectContainer objectContainer, 
            WebDriverManager webDriverManager) : base(objectContainer)
        {
            WebDriverManager = webDriverManager;
            var proxy = new Uri("http://localhost:4445");
            var target = new Uri("http://possumlabs.com");
            Proxy = new Lazy<ProxyWrapper>(() => new ProxyWrapper(
                new Uri($"http://{proxy.Host}:{proxy.Port}"),
                new Uri($"http://{target.Host}:{target.Port}")));
        }

        public Lazy<ProxyWrapper> Proxy { get; }
        public WebDriverManager WebDriverManager { get; }

        [BeforeScenario("proxy", Order = -400)]
        [Given("using a Proxy")]
        public void GivenUsingAProxy()
            => WebDriverManager.BaseUrl = 
            new Uri($"http://slipka:{Proxy.Value.ProxyUri.Port}");

        [Given("proxy logs responses of type '(.*)' with value '(.*)'")]
        public void GivenProxyLogsResponsesOfType(string type, string value)
            => Proxy.Value.RegisterRecording(new CallTemplate
            {
                Response = new Message
                {
                    Headers = new List<Header>() { new Header(type, new List<string>{ value }) }
                }
            });

        [Given(@"proxy logs calls to '(.*)'")]
        public void GivenProxyLogsCallsTo(string url)
            => Proxy.Value.RegisterRecording(new CallTemplate { Uri = url });

        [When("retrieving the file from proxy as '([^']*)'")]
        public void WhenRetrievingTheFileFromProxyAs(string name)
        {
            var file = new ProxyFile();

            var calls = Proxy.Value.GetCalls().ToList();
            var potentials = calls.Where(x => x.StatusCode != "302");
            potentials.Where(x => x.Recorded).Should().HaveCountGreaterThan(0, "no where recorded, there is likely some error.");
            potentials.Where(x => x.Recorded).Should().HaveCount(1, "multiple calls where recorded, there is likely some error.");
            var call = potentials.FirstOrDefault(x => x.Recorded);

            call.Should().NotBeNull("There was no recorded call returned from the proxy");

            file.Stream = new MemoryStream(Proxy.Value.DownloadResponse(calls.IndexOf(call)));

            base.Repository.Add(name, file);
        }

        [When("sleep for '(.*)' seconds")]
        public void WhenSleepFor(int i)
            => Thread.Sleep(i * 1000);

        [AfterScenario()]
        public void LogFilesAndCleanUp()
            => OnError.Continue(() =>
            {
                foreach (var f in base.Repository)
                {
                    try
                    {
                        f.Value.Stream.Dispose();
                    }
                    catch { } //dump them all, even if one fails.
                }
            });

        [AfterScenario()]
        public void WhenClosingTheProxy()
            => OnError.Continue(() =>
            {
                if (Proxy.IsValueCreated) Proxy.Value.Close();
            });

        [BeforeScenario("report")]

        [Given("configure Proxy to look for reports")]
        public void ReportAttribute()
        {
            //assuming you have a nice server
            GivenProxyLogsResponsesOfType("Content-Type", "application/pdf");
            GivenProxyLogsResponsesOfType("Content-Type", "application/vnd.ms-excel");
            GivenProxyLogsResponsesOfType("Content-Type", "application/vnd.openxml");
            //the test server is dumb
            GivenProxyLogsCallsTo(".*.pdf");
        }
    }

}
