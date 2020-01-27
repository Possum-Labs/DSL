using BoDi;
using LegacyTest;
using LegacyTest.Framework;
using OpenQA.Selenium;
using PossumLabs.Specflow.Selenium;
using PossumLabs.Specflow.Selenium.Selectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Shim.Selenium
{
    public abstract class WebDriverStepBase:StepBase
    {
        public WebDriverStepBase(IObjectContainer objectContainer) : base(objectContainer)
        { }

        protected WebDriver WebDriver => ObjectContainer.Resolve<WebDriverManager>().Current;

        protected WebDriverManager WebDriverManager => ObjectContainer.Resolve<WebDriverManager>();
        protected WebValidationFactory WebValidationFactory => ObjectContainer.Resolve<WebValidationFactory>();
        protected SelectorFactory SelectorFactory => ObjectContainer.Resolve<SelectorFactory>();

        protected ElementFactory ElementFactory => ObjectContainer.Resolve<ElementFactory>();
        protected XpathProvider XpathProvider => ObjectContainer.Resolve<XpathProvider>();
    }
}
