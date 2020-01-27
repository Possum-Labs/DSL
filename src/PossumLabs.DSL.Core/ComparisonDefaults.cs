using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Core
{
    public static class ComparisonDefaults
    {
        static ComparisonDefaults()
        {
            StringComparison = StringComparison.CurrentCulture;
        }
        public static StringComparison StringComparison { get; set; }
    }
}
