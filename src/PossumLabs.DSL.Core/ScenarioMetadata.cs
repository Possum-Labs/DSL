using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Core
{
    public class ScenarioMetadata
    {
        public ScenarioMetadata()
        {
            MetaData = new Dictionary<string, string>();
        }
        public string FeatureName { get; set; }
        public string ScenarioName { get; set; }
        public Dictionary<string,string> MetaData { get; set; }
    }
}
