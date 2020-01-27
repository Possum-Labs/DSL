using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Web.Selectors
{
    public class EmptySelectorPrefix : SelectorPrefix
    {
        public EmptySelectorPrefix()
        {
        }

        public override IEnumerable<string> CreateXpathPrefixes()
            => new string[]{ string.Empty };
    }
}
