using PossumLabs.DSL.Core.Logging;
using System;

namespace PossumLabs.DSL.Web.Diagnostic
{
    public interface INetworkWatcher
    {
        string BadUrl { get; set; }
        string LastGoodUrl { get; }
        Predicate<string> UrlErrorTester { get; set; }

        void AddUrl(string url);
        void ErrorOut(string url);
        void Log(ILog logger);
    }
}