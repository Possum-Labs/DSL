using BoDi;
using TechTalk.SpecFlow;

namespace PossumLabs.DSL.Core.IntegrationTests
{
    [Binding]
    public class FrameworkInitializationSteps : FrameworkInitializationStepsBase
    {
        public FrameworkInitializationSteps(IObjectContainer objectContainer) : base(objectContainer) { }

        [BeforeScenario(Order = int.MinValue+1)]
        public new void SetupInfrastructure()
            => base.SetupInfrastructure();
    }
}
