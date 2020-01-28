using PossumLabs.DSL.Core.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Web.Diagnostic
{
    public class NetworkWatcher
    {
        public Predicate<string> UrlErrorTester { get; set; }
        public void AddUrl(string url)
        {
            if (UrlErrorTester != null && UrlErrorTester(url))
                ErrorOut(url);
            LastGoodUrl = url;
        }
        public void ErrorOut(string url)
        {
            BadUrl = url;
            throw new Exception("Network Watcher notified of invalid url, terminating test.");
        }

        public string LastGoodUrl { get; private set; }
        public string BadUrl { get; set; }

        public void Log(ILog logger)
        {
            logger.Section(this.GetType().Name, new { LastGoodUrl = LastGoodUrl, BadUrl = BadUrl });
        }
    }
}
