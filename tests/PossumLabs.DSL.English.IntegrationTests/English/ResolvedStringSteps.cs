using Reqnroll.BoDi;
using PossumLabs.DSL.Core;
using Reqnroll;

namespace PossumLabs.DSL.English
{
    [Binding]
    public class ResolvedStringSteps : ResolvedStringStepsBase
    {
        public ResolvedStringSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
        }

        [StepArgumentTransformation]
        public  ResolvedString TransformEnglish(string id) 
            => base.Transform(id);
    }
}
