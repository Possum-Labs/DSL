using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PossumLabs.DSL.Web.Selectors
{
    public abstract class SelectorPrefix
    {
        public void Init(string constructor, string xpathPrefix)
        {
            this.Constructor = constructor;
            this.XpathPrefix = xpathPrefix;
        }

        public void Init(string constructor, List<Func<string, IEnumerable<string>>> prefixes)
        {
            this.Constructor = constructor;
            this.Prefixes = prefixes;
        }

        private string Constructor { get; set; }
        private string XpathPrefix { get; set; }
        private List<Func<string, IEnumerable<string>>> Prefixes { get; set; }

        public virtual IEnumerable<string> CreateXpathPrefixes()
            => Prefixes != null ? Prefixes.SelectMany(f => f(Constructor)) : new string[] { XpathPrefix };

        public virtual string Type => PrefixNames.Unknown;
    }
}
