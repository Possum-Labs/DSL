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

        [AfterScenario(Order = 1)]
        public new void WebDriverStepBasedLogging()
            => base.WebDriverStepBasedLogging();

        [AfterScenario(Order = int.MinValue + 1)]
        public new void ErrorScreenLogging()
            => base.ErrorScreenLogging();

        [AfterScenario(Order = int.MinValue)]
        public new void CheckForAlerts()
            => base.CheckForAlerts();

        [BeforeScenario(Order = int.MinValue+1)]
        public new void SetupInfrastructure()
            => base.SetupInfrastructure();

        [BeforeScenario("Movie-Logger")]
        public new void EnableMovieLogger()
            => base.EnableMovieLogger();

        [AfterScenario]
        public new void CreateMovie()
            => base.CreateMovie();
    }
}
