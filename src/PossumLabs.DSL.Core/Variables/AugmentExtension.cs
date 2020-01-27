using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Core.Variables
{
    public static class AugmentExtension
    {
        public static Dictionary<string, KeyValuePair<string,string>> Augment(this Dictionary<string, KeyValuePair<string, string>> provided, Dictionary<string, string> defaults)
        {
            foreach (var key in defaults.Keys)
            {
                if (provided.ContainsKey(key.ToUpper()))
                    continue;
                provided.Add(key.ToUpper(), new KeyValuePair<string, string>(key, defaults[key]));
            }
            return provided;
        }
    }
}
