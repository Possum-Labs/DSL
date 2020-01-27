using BoDi;
using TechTalk.SpecFlow;

namespace PossumLabs.DSL.English
{
    [Binding]
    public class FrameworkInitializationSteps : FrameworkInitializationStepsBase
    {
        public FrameworkInitializationSteps(IObjectContainer objectContainer) : base(objectContainer) { }

        [BeforeScenario(Order = int.MinValue + 1)]
        public new void Setup()
            => base.Setup();

        [AfterStep]
        public new void LogStep()
            => base.LogStep();

        [BeforeStep]
        public new void NetworkWatcherHook()
            => base.NetworkWatcherHook();

        [AfterScenario]
        public new void LogScreenshots()
            => base.LogScreenshots();

        [AfterScenario(Order = int.MinValue + 1)]
        public new void LogHtml()
            => base.LogHtml();

        [AfterScenario(Order = int.MinValue)]
        public new void CheckForAlerts()
            => base.CheckForAlerts();

        [BeforeScenario(Order = int.MinValue+1)]
        public new void SetupInfrastructure()
            => base.SetupInfrastructure();

        [BeforeScenario(Order = 1)]
        public new void SetupExistingData()
            => base.SetupExistingData();
    }
}
