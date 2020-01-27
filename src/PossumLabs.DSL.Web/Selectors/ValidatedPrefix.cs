using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PossumLabs.DSL.Web.Selectors
{
    public class ValidatedPrefix : SelectorPrefix
    {
        public ValidatedPrefix()
        {
        }

        private IEnumerable<string> Xpaths {get;set;}
        public bool IsInitialized { get => Xpaths?.Any() == true; }

        internal void Init(string v, IEnumerable<string> valid)
            => Xpaths = valid;

        public override IEnumerable<string> CreateXpathPrefixes()
            => Xpaths;
    }
}
