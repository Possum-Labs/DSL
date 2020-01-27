using BoDi;
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
        protected WebValidationFactory WebValidationFactory => ObjectContainer.Resolve<WebValidationFactory>();
        protected SelectorFactory SelectorFactory => ObjectContainer.Resolve<SelectorFactory>();

        protected ElementFactory ElementFactory => ObjectContainer.Resolve<ElementFactory>();
        protected XpathProvider XpathProvider => ObjectContainer.Resolve<XpathProvider>();
    }
}
