using BoDi;
using OpenQA.Selenium;
using PossumLabs.Specflow.Core;
using PossumLabs.Specflow.Core.Exceptions;
using PossumLabs.Specflow.Core.Files;
using PossumLabs.Specflow.Core.Logging;
using PossumLabs.Specflow.Core.Variables;
using PossumLabs.Specflow.Selenium;
using PossumLabs.Specflow.Selenium.Selectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Selenium_Tables
{
    public abstract class StepBase
    {
        public StepBase(IObjectContainer objectContainer) 
        {
            ObjectContainer = objectContainer;
        }

        protected IObjectContainer ObjectContainer { get; }
        protected ScenarioContext ScenarioContext { get => ObjectContainer.Resolve<ScenarioContext>(); }
        protected FeatureContext FeatureContext { get => ObjectContainer.Resolve<FeatureContext>(); }

        protected WebDriver WebDriver => ObjectContainer.Resolve<WebDriverManager>().Current;
        protected WebDriverManager WebDriverManager => ObjectContainer.Resolve<WebDriverManager>();
        protected WebValidationFactory WebValidationFactory => ObjectContainer.Resolve<WebValidationFactory>();
        protected SelectorFactory SelectorFactory => ObjectContainer.Resolve<SelectorFactory>();
        protected ElementFactory ElementFactory => ObjectContainer.Resolve<ElementFactory>();
        protected XpathProvider XpathProvider => ObjectContainer.Resolve<XpathProvider>();
        protected Interpeter Interpeter => ObjectContainer.Resolve<Interpeter>();
        protected ActionExecutor Executor => ObjectContainer.Resolve<ActionExecutor>();
        protected ILog Log => ObjectContainer.Resolve<ILog>();
        protected ObjectFactory ObjectFactory => ObjectContainer.Resolve<ObjectFactory>();

        internal void Register<T>(T item) where T : class
            => ObjectContainer.RegisterInstanceAs<T>(item, dispose: true);
    }
}
