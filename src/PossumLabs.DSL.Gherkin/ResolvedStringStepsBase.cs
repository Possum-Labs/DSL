using BoDi;
using PossumLabs.DSL.Core;

namespace PossumLabs.DSL
{
    public abstract class ResolvedStringStepsBase : StepsBase
    {
        public ResolvedStringStepsBase(IObjectContainer objectContainer) : base(objectContainer)
        {
        }

        public ResolvedString Transform(string id) => Interpeter.Get<string>(id);
    }
}
