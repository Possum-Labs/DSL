using OpenQA.Selenium;

namespace PossumLabs.DSL.Web.ApplicationElements
{
    public interface IApplicationCommandSet
    {
        bool IsClassMatch(string s);
        void Set(string textareaId, IWebDriver driver, string s);
        void Clear(string textareaId, IWebDriver driver);
        string Get(string textareaId, IWebDriver driver);
    }
}
