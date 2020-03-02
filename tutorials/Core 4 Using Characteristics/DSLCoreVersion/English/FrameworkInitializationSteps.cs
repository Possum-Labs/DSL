using BoDi;
using TechTalk.SpecFlow;

namespace DSL.Documentation.Example
{
    [Binding]
    public class FrameworkInitializationSteps : FrameworkInitializationStepsBase
    {
        public FrameworkInitializationSteps(IObjectContainer objectContainer) : base(objectContainer) { }

        [BeforeScenario(Order = int.MinValue + 1)]
        public virtual void SetupInfrastructureEnglish()
            => base.SetupInfrastructure();

        [BeforeScenario(Order = 0)]
        public virtual void LoadExistingDataEnglish()
            => LoadExistingData();
    }
}
