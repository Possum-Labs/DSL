using System;
using System.Collections.Generic;

namespace PossumLabs.DSL.Slipka
{
    public class SessionSummary
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int? CallsCount { get; set; }
        public int ProxyPort { get; set; }
        public string TargetHost { get; set; }
        public int TargetPort { get; set; }
        public List<string> Tags { get; set; }
        public List<Call> RecordedCalls { get; set; }
        public List<Call> OverriddenCalls { get; set; }
        public List<Call> Calls { get; set; }
        public string OpenFor { get; set; }
        public string RetainedFor { get; set; }
    }
}
