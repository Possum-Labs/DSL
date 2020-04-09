using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Web.ApplicationElements
{
    public class ApplicationElementRegistry
    {
        public ApplicationElementRegistry()
        {
            ApplicationCommandSets = new List<IApplicationCommandSet>()
            {
                new CKE4Commands(),
                //new CKE5Commands(),
                new TinyMCE4Commands(),
                new TinyMCE5Commands(),
            };
        }

        public List<IApplicationCommandSet> ApplicationCommandSets { get; }
    }
}
