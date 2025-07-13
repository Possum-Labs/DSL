using Reqnroll.BoDi;
using OpenQA.Selenium;
using PossumLabs.DSL.Web;
using PossumLabs.DSL.Web.Selectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reqnroll;

namespace PossumLabs.DSL.Web.Integration
{
    public abstract class WebDriverStepBase:StepBase
    {
        public WebDriverStepBase(IObjectContainer objectContainer) : base(objectContainer)
        { }

        protected WebDriver WebDriver => ObjectContainer.Resolve<WebDriverManager>().Current;

        protected WebDriverManager WebDriverManager => ObjectContainer.Resolve<WebDriverManager>();
        protected IWebValidationFactory WebValidationFactory => ObjectContainer.Resolve<IWebValidationFactory>();
        protected ISelectorFactory SelectorFactory => ObjectContainer.Resolve<ISelectorFactory>();

        protected IElementFactory ElementFactory => ObjectContainer.Resolve<IElementFactory>();
        protected IXpathProvider XpathProvider => ObjectContainer.Resolve<IXpathProvider>();
    }
}
