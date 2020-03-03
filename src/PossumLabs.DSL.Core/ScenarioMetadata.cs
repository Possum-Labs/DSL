using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Core
{
    public class ScenarioMetadata
    {
        public ScenarioMetadata(Func<bool> isErrorPresent)
        {
            MetaData = new Dictionary<string, string>();
            IsErrorPresent = isErrorPresent;
        }
        public string FeatureName { get; set; }
        public string ScenarioName { get; set; }
        public Dictionary<string,string> MetaData { get; set; }
        public Func<bool> IsErrorPresent { get; }
    }
}
