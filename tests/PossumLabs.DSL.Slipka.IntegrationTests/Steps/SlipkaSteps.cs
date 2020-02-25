using BoDi;
using PossumLabs.DSL.Slipka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using TechTalk.SpecFlow;

namespace PossumLabs.DSL.Slipka.IntegrationTests
{
    [Binding]
    public sealed class SlipkaSteps: StepBase
    {
        public SlipkaSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
            ProxySteps = new ProxySteps(objectContainer);
            ProxyAdministration = new Uri("http://localhost:4445");
            Wrapper = new ProxyWrapper(ProxyAdministration);
        }

        private Uri ProxyAdministration { get; }

        private ProxySteps ProxySteps { get; }

        private ProxyWrapper Wrapper { get; }

        [BeforeScenario("Slipka")]
        public void BeforeScenario()
        {
            Wrapper.Open(ProxyAdministration);
            Wrapper.RegisterRecording(new CallTemplate { Method = "POST" });
            Wrapper.RegisterRecording(new CallTemplate { Method = "PUT" });
            ProxySteps.AddDefault("Host", Wrapper.ProxyUri.ToString());
        }

        [Given(@"injecting")]
        public void GivenInjecting(List<SlipkaInjection> injections)
            => injections.ForEach(i => Wrapper.RegisterInject(i));


        [AfterScenario("Slipka")]
        public void AfterScenario()
        {
            Wrapper.CloseAsync();

            var content = $"### Full session is availble at {ProxyAdministration}/Session/{Wrapper.Id}\n";
            //TODO: v2 Better solution
            Thread.Sleep(1000);
            foreach (var call in Wrapper.GetCalls(recorded: true))
                content += call.ToHttpFormat();

            Log.Section("Slipka Logs", content);
        }
    }
}
