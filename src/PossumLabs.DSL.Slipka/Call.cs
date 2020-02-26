using PossumLabs.DSL.Core;
using PossumLabs.DSL.Core.Variables;
using System;
using System.Collections.Generic;

namespace PossumLabs.DSL.Slipka
{
    public class Call : IEntity
    {
        public Call()
        {
            Request = new Message();
            Response = new Message();
        }

        public Call(string Host, string Path)
        {
            if(Host.EndsWith("/"))
                Uri = new Uri($"{Host}{Path}");
            else
                Uri = new Uri($"{Host}/{Path}");
        }

        public Message Response { get; set; }
        public Message Request { get; set; }
        public string StatusCode { get; set; }

        public string Method { get; set; }
        public Uri Uri { get; set; }

        public double? Duration { get; set; }

        public string LogFormat()
            => $"{Method} {Uri}";
    }
}
