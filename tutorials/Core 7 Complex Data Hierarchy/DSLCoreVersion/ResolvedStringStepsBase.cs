using BoDi;
using PossumLabs.DSL.Core;

namespace DSL.Documentation.Example
{
    public abstract class ResolvedStringStepsBase : StepsBase
    {
        public ResolvedStringStepsBase(IObjectContainer objectContainer) : base(objectContainer)
        {
        }

        public ResolvedString Transform(string id) => Interpeter.Get<string>(id);
    }
}
