using OpenQA.Selenium;

namespace PossumLabs.DSL.Web
{
    public interface IRichTextEditorCommandSet
    {
        bool IsClassMatch(string s);
        void Set(string textareaId, IWebDriver driver, string s);
        void Clear(string textareaId, IWebDriver driver);
        string Get(string textareaId, IWebDriver driver);
    }
}
