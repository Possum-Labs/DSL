using BoDi;
using PossumLabs.DSL.Core;
using TechTalk.SpecFlow;

namespace PossumLabs.DSL.Core.IntegrationTests
{
    [Binding]
    public class ResolvedStringSteps : ResolvedStringStepsBase
    {
        public ResolvedStringSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
        }

        [StepArgumentTransformation]
        public new ResolvedString Transform(string id) 
            => base.Transform(id);
    }
}
