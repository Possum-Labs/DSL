using PossumLabs.DSL.Slipka.IntegrationTests;
using PossumLabs.DSL.Core.Files;
using PossumLabs.DSL.Slipka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reqnroll;
using Reqnroll.BoDi;

namespace PossumLabs.DSL.Slipka.IntegrationTests
{
    [Binding]
    public class ProxySteps : RepositoryStepBase<ProxyWrapper>
    {
        public ProxySteps(IObjectContainer objectContainer) : base(objectContainer)
        {
        }

        protected override void Create(ProxyWrapper item)
        {
            //Constructor tackles this
        }

        [AfterScenario(Order =int.MinValue)]
        public void CloseAllProxies()
            => base.Repository.ToList().ForEach(w => w.Value.CloseAsync());

        [Given(@"the Slipka Proxy")]
        [Given(@"the Slipka Proxies")]
        public void GivenTheSlipkaProxyFor(Dictionary<string, ProxyWrapper> proxies)
            => proxies.Keys.ToList().ForEach(k=> Add(k, proxies[k]));

        [Given(@"the Proxy '(.*)' injects the calls")]
        public void GivenTheProxyinjectsTheCalls(ProxyWrapper proxy, List<CallTemplate> calls)
            => Executor.Execute(()=> calls.ForEach(c=> proxy.RegisterInject(c)));

        [Given(@"the Proxy '(.*)' records the calls")]
        public void GivenTheProxyRecordsTheCalls(ProxyWrapper proxy, List<CallTemplate> calls)
            => Executor.Execute(() => calls.ForEach(c => proxy.RegisterRecording(c)));

        [Given(@"the Proxy '(.*)' tags the calls")]
        public void GivenTheProxyTagsTheCalls(ProxyWrapper proxy, List<CallTemplate> calls)
            => Executor.Execute(() => calls.ForEach(c => proxy.RegisterTag(c)));

        [Given(@"the Proxy '(.*)' decorates with")]
        public void GivenTheProxyDecoratesWith(ProxyWrapper proxy, List<Header> decorations)
            => Executor.Execute(() => decorations.ForEach(d => proxy.RegisterDecoration(d)));

        [Then(@"retrieving the (u?n?)recorded calls from Proxy '(.*)' as '(.*)'")]
        public void ThenRetrievingTheRecordedCallsFromProxyAs(string un, ProxyWrapper proxy, string name)
            => Interpeter.Add(name, proxy.GetCalls(recorded:un!="un"));

        [Then(@"retrieving the tagged calls from Proxy '(.*)' with tag '(.*)' as '(.*)'")]
        public void ThenRetrievingTheTaggedCallsFromProxyAs(ProxyWrapper proxy, string tag, string name)
            => Interpeter.Add(name, proxy.GetCalls(tag:tag));

        [Then(@"retrieving the calls from Proxy '(.*)' as '(.*)'")]
        public void ThenRetrievingTheCallsFromProxyAs(ProxyWrapper proxy, string name)
            => Interpeter.Add(name, proxy.GetCalls());

        [Then(@"close the Proxy '(.*)'")]
        public void ThenCloseTheProxy(ProxyWrapper proxy)
            => proxy.Close();

        [Then(@"retrieving the Session from Proxy '(.*)' as '(.*)'")]
        public void ThenRetrievingTheSessionFromProxyAs(ProxyWrapper proxy, string name)
            => Interpeter.Add(name, proxy.GetSession());

        [Then(@"retrieving the response of call '(.*)' for Proxy '(.*)' as File '(.*)'")]
        public void ThenRetrievingTheResponseOfCallAsFile(int number, ProxyWrapper proxy, string name)
            => Interpeter.Add(name, (IFile) new InMemoryFile(proxy.DownloadResponse(number)));

        [Then(@"retrieving the request of call '(.*)' for Proxy '(.*)' as File '(.*)'")]
        public void ThenRetrievingTheRequestOfCallAsFile(int number, ProxyWrapper proxy, string name)
            => Interpeter.Add(name, (IFile)new InMemoryFile(proxy.DownloadRequest(number)));

    }
}
