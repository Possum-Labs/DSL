using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Slipka
{
    public class SlipkaInjection : CallTemplate
    {
        public SlipkaInjection():base()
        {
            Method = "GET";
            Duration = 0;
            Response = new Message();
            EncodingHeader = new Header("Content-type", new string[] { "application/json", "charset=utf-8" });
            Response.Headers.Add(EncodingHeader);
            StatusCode = "OK";
        }

        private Header EncodingHeader { get; set; }

        public List<string> Encoding
        {
            set
            {
                EncodingHeader.Values = value;
            }
        }

        public string Content
        {
            set
            {
                Response.Content = value;
            }
        }

        public string Path
        {
            set
            {
                Uri = $"^.*{value}$";
            }
        }

        public string Html
        {
            set
            {
                EncodingHeader.Values = new List<string> { "text/html", "charset=utf-8" };
                Content = $"<html><head></head><body>{value}</body></html>";
            }
        }

        public string Form
        {
            set
            {
                EncodingHeader.Values = new List<string> { "text/html;", "charset=utf-8" };
                Content = $"<html><head></head><body><form>{value}</form></body></html>";
            }
        }
    }
}
