using OpenQA.Selenium;
using PossumLabs.DSL.Core.Logging;

namespace PossumLabs.DSL.Web.Selectors
{
    public interface IWebElementSourceLog
    {
        void Add(IWebElement e, string page, By by);
        WebElementSourceLog.WebElementSource Get(IWebElement e);
        void Log(ILog logger);
    }
}