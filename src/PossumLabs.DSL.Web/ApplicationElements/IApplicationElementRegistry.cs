using System.Collections.Generic;

namespace PossumLabs.DSL.Web.ApplicationElements
{
    public interface IApplicationElementRegistry
    {
        List<IApplicationCommandSet> ApplicationCommandSets { get; }
    }
}