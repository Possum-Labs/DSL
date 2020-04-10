using OpenQA.Selenium;
using PossumLabs.DSL.Web.ApplicationElements;

namespace PossumLabs.DSL.Web.Selectors
{
    public interface IElementFactory
    {
        Element Create(IWebDriver driver, IWebElement e);
    }
}