using BoDi;
using PossumLabs.DSL.Core.Variables;
using TechTalk.SpecFlow;

namespace DSL.Documentation.Example
{
    [Binding]
    public class FrameworkInitializationSteps : FrameworkInitializationStepsBase
    {
        public FrameworkInitializationSteps(IObjectContainer objectContainer) : base(objectContainer) { }

        [StepArgumentTransformation]
        public Characteristics TransformEnglish(string id) => base.Transform(id);

        [BeforeScenario(Order = int.MinValue + 1)]
        public void SetupInfrastructureEnglish()
            => base.SetupInfrastructure();

        [BeforeScenario(Order = int.MinValue + 12)]
        protected void LoadTemplatesEnglish()
    => base.LoadTemplates();

        [BeforeScenario(Order = int.MinValue + 13)]
        protected void LoadExistingDataEnglish()
            => base.LoadExistingData();
    }
}
