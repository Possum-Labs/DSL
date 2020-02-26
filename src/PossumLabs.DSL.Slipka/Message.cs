using System.Collections.Generic;

namespace PossumLabs.DSL.Slipka
{
    public class Message
    {
        public Message()
        {
            Headers = new List<Header>();
        }
        public List<Header> Headers { get; set; }
        public string Content { get; set; }
    }
}
