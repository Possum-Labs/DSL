using BoDi;
using FluentAssertions;
using LegacyTest;
using LegacyTest.Framework;
using LegacyTest.ValueObjects;
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

namespace Shim.Selenium
{
    [Binding]
    public class FrameworkInitializationSteps : WebDriverStepBase
    {
        public FrameworkInitializationSteps(IObjectContainer objectContainer) : base(objectContainer) { }


        private ScreenshotProcessor ScreenshotProcessor { get; set; }
        private ImageLogging ImageLogging { get; set; }
        private MovieLogger MovieLogger { get; set; }

        [BeforeScenario(Order = int.MinValue + 1)]
        public void Setup()
        {
            ScreenshotProcessor = ObjectContainer.Resolve<ScreenshotProcessor>();
        }

        [AfterStep]
        public void LogStep()
        {
            MovieLogger.StepEnd($"{ScenarioContext.StepContext.StepInfo.StepDefinitionType} {ScenarioContext.StepContext.StepInfo.Text}");
        }

        [AfterScenario]
        public void LogScreenshots()
        {
            MovieLogger.ComposeMovie();
        }

        [AfterScenario(Order = int.MinValue + 1)]
        public void LogHtml()
        {
            if (WebDriverManager.ActiveDriver)
            {
                try
                {
                    FileManager.CreateFile(Encoding.UTF8.GetBytes(WebDriverManager.Current.PageSource), "source", "html");
                }
                catch (UnhandledAlertException) { return; }
            }
        }

        [AfterScenario(Order = int.MinValue)]
        public void CheckForAlerts()
        {
            if (!WebDriverManager.ActiveDriver)
                return;
            WebDriver.AlertText.Should().BeNull($"An allert was left open at the end of the test with text:{WebDriver.AlertText}");
        }

        [BeforeScenario(Order = int.MinValue+1)]
        public void SetupInfrastructure()
        {
            IConfiguration config = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .AddEnvironmentVariables()
              .Build();

            var configFactory = new ConfigurationFactory(config);

            ObjectContainer.RegisterInstanceAs(configFactory.Create<MovieLoggerConfig>());
            ObjectContainer.RegisterInstanceAs(configFactory.Create<ImageLoggingConfig>());

            ImageLogging = new ImageLogging(ObjectContainer.Resolve<ImageLoggingConfig>());
            MovieLogger = new MovieLogger(ObjectContainer.Resolve<MovieLoggerConfig>(), Metadata);

            ObjectContainer.RegisterInstanceAs(ImageLogging);
            ObjectContainer.RegisterInstanceAs(MovieLogger);

            Register<ElementFactory>(new SpecializedElementFactory());
            Register<XpathProvider>(new XpathProvider());
            Register<SelectorFactory>(new SpecializedSelectorFactory(ElementFactory, XpathProvider).UseBootstrap());

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
