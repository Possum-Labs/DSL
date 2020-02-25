using PossumLabs.DSL.Core;
using PossumLabs.DSL.Core.Variables;
using System.Collections.Generic;

namespace PossumLabs.DSL.Slipka
{
    public class Session : IValueObject
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Call> Calls { get; set; }
        public int ProxyPort { get; set; }
        public string TargetHost { get; set; }
        public int TargetPort { get; set; }
        public List<string> Tags { get; set; }
        public List<CallTemplate> RecordedCalls { get; set; }
        public List<CallTemplate> OverriddenCalls { get; set; }
    }
}
