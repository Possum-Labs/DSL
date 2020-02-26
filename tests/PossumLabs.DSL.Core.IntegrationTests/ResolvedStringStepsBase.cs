using BoDi;
using PossumLabs.DSL.Core;

namespace PossumLabs.DSL.Core.IntegrationTests
{
    public abstract class ResolvedStringStepsBase : StepsBase
    {
        public ResolvedStringStepsBase(IObjectContainer objectContainer) : base(objectContainer)
        {
        }

        public ResolvedString Transform(string id) => Interpeter.Get<string>(id);
    }
}
