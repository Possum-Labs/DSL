using BoDi;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using PossumLabs.Specflow.Core;
using PossumLabs.Specflow.Core.Configuration;
using PossumLabs.Specflow.Core.Exceptions;
using PossumLabs.Specflow.Core.Files;
using PossumLabs.Specflow.Core.Logging;
using PossumLabs.Specflow.Selenium;
using PossumLabs.Specflow.Selenium.Configuration;
using PossumLabs.Specflow.Selenium.Diagnostic;
using PossumLabs.Specflow.Selenium.Selectors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Selenium_Tables
{
    [Binding]
    public class FrameworkInitializationSteps : StepBase
    {
        public FrameworkInitializationSteps(IObjectContainer objectContainer) : base(objectContainer) { }

        [StepArgumentTransformation]
        public object Transform(string id) => Interpeter.Get<object>(id);

        [StepArgumentTransformation]
        public ResolvedString TransformResolvedString(string id) => Interpeter.Get<string>(id);

        [BeforeScenario(Order = int.MinValue)]
        public void SetupInfrastructure()
        {
            var metadata = new ScenarioMetadata
            {
                FeatureName = FeatureContext.FeatureInfo.Title,
                ScenarioName = ScenarioContext.ScenarioInfo.Title
            };
            Register(metadata);

            var factory = new PossumLabs.Specflow.Core.Variables.ObjectFactory();
            Register(factory);
            Register(new PossumLabs.Specflow.Core.Variables.Interpeter(factory));
            var logger = new DefaultLogger(new DirectoryInfo(Environment.CurrentDirectory));
            Register((PossumLabs.Specflow.Core.Logging.ILog)logger);

            IConfiguration config = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .Build();

            var configFactory = new ConfigurationFactory(config);

            ObjectContainer.RegisterInstanceAs(configFactory.Create<MovieLoggerConfig>());
            ObjectContainer.RegisterInstanceAs(configFactory.Create<ImageLoggingConfig>());

            Register<ElementFactory>(new ElementFactory());
            Register<XpathProvider>(new XpathProvider());
            Register<SelectorFactory>(new SelectorFactory(ElementFactory, XpathProvider).UseBootstrap());

            Register(new PossumLabs.Specflow.Selenium.WebDriverManager(
                this.Interpeter,
                this.ObjectFactory,
                new PossumLabs.Specflow.Selenium.Configuration.SeleniumGridConfiguration()));

            Log.Message($"feature: {FeatureContext.FeatureInfo.Title} scenario: {ScenarioContext.ScenarioInfo.Title} \n" +
                $"Tags: {FeatureContext.FeatureInfo.Tags.LogFormat()} {ScenarioContext.ScenarioInfo.Tags.LogFormat()}");

            WebDriverManager.Initialize(BuildDriver);
        }

        public WebDriver BuildDriver()
            => new WebDriver(
                WebDriverManager.Create(),
                () => WebDriverManager.BaseUrl,
                ObjectContainer.Resolve<SeleniumGridConfiguration>(),
                ObjectContainer.Resolve<RetryExecutor>(),
                SelectorFactory,
                ElementFactory,
                XpathProvider,
                ObjectContainer.Resolve<MovieLogger>());

        [BeforeScenario(Order = 1)]
        public void SetupExistingData()
        {
            new PossumLabs.Specflow.Core.Variables.ExistingDataManager(this.Interpeter).Initialize(Assembly.GetExecutingAssembly());
        }
    }
}
