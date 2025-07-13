using Reqnroll.BoDi;
using PossumLabs.DSL.Web;
using PossumLabs.DSL.Web.Selectors;

namespace PossumLabs.DSL
{
    public abstract class WebDriverStepsBase : StepsBase
    {
        public WebDriverStepsBase(IObjectContainer objectContainer) : base(objectContainer)
        { }

        protected WebDriver WebDriver => ObjectContainer.Resolve<WebDriverManager>().Current;

        protected WebDriverManager WebDriverManager => ObjectContainer.Resolve<WebDriverManager>();
        protected IWebValidationFactory WebValidationFactory => ObjectContainer.Resolve<IWebValidationFactory>();
        protected ISelectorFactory SelectorFactory => ObjectContainer.Resolve<ISelectorFactory>();

        protected IElementFactory ElementFactory => ObjectContainer.Resolve<IElementFactory>();
        protected IXpathProvider XpathProvider => ObjectContainer.Resolve<IXpathProvider>();
    }
}
