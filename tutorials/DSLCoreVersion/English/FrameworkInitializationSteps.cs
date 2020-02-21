using BoDi;
using TechTalk.SpecFlow;

namespace DSL.Documentation.Example.English
{
    [Binding]
    public class FrameworkInitializationSteps : FrameworkInitializationStepsBase
    {
        public FrameworkInitializationSteps(IObjectContainer objectContainer) : base(objectContainer) { }

        [BeforeScenario(Order = int.MinValue+1)]
        public new void SetupInfrastructure()
            => base.SetupInfrastructure();

        [BeforeScenario(Order = 1)]
        public new void SetupExistingData()
            => base.SetupExistingData();
    }
}
