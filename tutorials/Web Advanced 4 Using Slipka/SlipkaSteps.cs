using PossumLabs.DSL;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace UsingSlipka
{
    [Binding]
    public class SlipkaSteps : RepositoryStepBase<ProxyFile>
    {
        public SlipkaSteps(IObjectContainer objectContainer) :
base(objectContainer)
        {
            _fileSteps = ObjectContainer.Resolve<FileSteps>();
            var proxy = new Uri(Config.Proxy);
            var webIz = new Uri(Config.Url);
            Proxy = new Lazy<ProxyWrapper>(() => new ProxyWrapper(
                new Uri($"http://{proxy.Host}:{proxy.Port}"),
                new Uri($"http://{webIz.Host}:{webIz.Port}")));
            _excelSteps = ObjectContainer.Resolve<ExcelSteps>();
        }

        public Lazy<ProxyWrapper> Proxy { get; }

        [BeforeScenario("proxy", Order = -400)]
        [Given("Using a proxy")]
        public void GivenUsingAProxy()
            => Pages.Override(host: this.Config.ProxyHost, port:
Proxy.Value.Port);

        [Given("proxy logs responses of type '(.*)' with value '(.*)'")]
        public void GivenProxyLogsResponsesOfType(string type, string value)
            => Proxy.Value.RegisterRecording(new CallTemplate
            {
                Response = new Message
                {
                    Headers = new List<Header>() { new
Header(type, value) }
                }
            });

        [Given(@"proxy logs calls to '(.*)'")]
        public void GivenProxyLogsCallsTo(string url)
            => Proxy.Value.RegisterRecording(new CallTemplate { Uri = url });

        [When("retrieving the file from proxy as '(.*)'")]
        public void WhenRetrievingTheFileFromProxyAs(string name)
        {
            var file = new ProxyFile();

            var calls = Proxy.Value.GetCalls().ToList();
            var potentials = calls.Where(x => x.StatusCode != "302");
            potentials.Where(x =>
x.Recorded).Should().HaveCountGreaterThan(0, "no where recorded, there
is likely some error.");
            potentials.Where(x => x.Recorded).Should().HaveCount(1,
"multiple calls where recorded, there is likely some error.");
            var call = potentials.FirstOrDefault(x => x.Recorded);

            call.Should().NotBeNull("There was no recorded call
returned from the proxy");

            file.Stream = new
MemoryStream(Proxy.Value.DownloadResponse(calls.IndexOf(call)));

            base.Repository.Add(name, file);
        }

        [AfterScenario()]
        public void LogFilesAndCleanUp()
            => OnError.Continue(() =>
            {
                if (MovieLogger.IsEnabled || ScenarioContext.TestError != null)
                {
                    foreach (var item in
Repository.AsDictionary().Values.Cast<ProxyFile>())
                    {
                        item.Stream.Position = 0;
                        FileManager.PersistFile(item, "ProxyFile", "file");
                    }
                }

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
        public void ReportAttribute()
        {
            GivenProxyLogsCallsTo("/Reports/.*/View.aspx");
            GivenProxyLogsResponsesOfType("Content-Type", "application/pdf");
            GivenProxyLogsResponsesOfType("Content-Type",
"application/vnd.ms-excel");
            GivenProxyLogsResponsesOfType("Content-Type",
"application/vnd.openxml");
        }

        [Then(@"the report contains")]
        public void ThenTheReportContains(Table table)
        {
            string id = $"Report{IdGenerator.Alpha(10)}";
            WhenClosingTheProxy();
            WhenRetrievingTheFileFromProxyAs(id);
            _excelSteps.ThenTheFileContains(Interpeter.Get<IFile>(id), table);
        }

        [Then(@"the no header CSV report contains")]
        public void VerifyCsvReportContains(Table table)
        {
            string id = $"Report{IdGenerator.Alpha(10)}";
            WhenClosingTheProxy();
            WhenRetrievingTheFileFromProxyAs(id);
            _excelSteps.VerifyCsvFileContains(Interpeter.Get<IFile>(id), table);
        }



        protected override void UICreate(ProxyFile item)
        {
        }
    }

}
