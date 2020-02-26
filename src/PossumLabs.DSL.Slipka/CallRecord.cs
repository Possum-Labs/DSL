using PossumLabs.DSL.Core;
using PossumLabs.DSL.Core.Variables;
using System;
using System.Collections.Generic;

namespace PossumLabs.DSL.Slipka
{
    public class CallRecord : IEntity
    {
        public CallRecord()
        {

        }

        public Message Response { get; set; }
        public Message Request { get; set; }
        public string StatusCode { get; set; }

        public string Method { get; set; }
        public Uri Uri { get; set; }
        public string Path { get; set; }

        public double? Duration { get; set; }

        public bool Intercepted { get; set; }
        public bool Recorded { get; set; }

        public List<string> Tags { get; set; }

        public DateTime Recieved { get; set; }

        public string LogFormat()
            => $"{Method} {Uri}";
    }
}
