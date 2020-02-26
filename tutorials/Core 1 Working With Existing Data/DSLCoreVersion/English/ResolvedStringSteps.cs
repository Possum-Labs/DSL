using BoDi;
using PossumLabs.DSL.Core;
using TechTalk.SpecFlow;

namespace DSL.Documentation.Example
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
