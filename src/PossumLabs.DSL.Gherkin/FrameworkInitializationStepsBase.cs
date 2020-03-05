﻿using BoDi;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using PossumLabs.DSL.DataGeneration;
using PossumLabs.DSL.Core;
using PossumLabs.DSL.Core.Configuration;
using PossumLabs.DSL.Core.Exceptions;
using PossumLabs.DSL.Core.Files;
using PossumLabs.DSL.Core.Logging;
using PossumLabs.DSL.Web;
using PossumLabs.DSL.Web.Configuration;
using PossumLabs.DSL.Web.Diagnostic;
using PossumLabs.DSL.Web.Selectors;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using TechTalk.SpecFlow;
using System.Linq;
using PossumLabs.DSL.Core.Variables;

namespace PossumLabs.DSL
{
    public abstract class FrameworkInitializationStepsBase : WebDriverStepsBase
    {
        public FrameworkInitializationStepsBase(IObjectContainer objectContainer) : base(objectContainer) { }

        protected ScreenshotProcessor ScreenshotProcessor { get; set; }
        protected ImageLogging ImageLogging { get; set; }
        protected MovieLogger MovieLogger { get; set; }
        protected WebElementSourceLog WebElementSourceLog { get; set; }
        protected NetworkWatcher NetworkWatcher { get; set; }

        protected DefaultLogger Logger { get; set; }

        protected virtual void LogStep()
        {
            MovieLogger.StepEnd($"{ScenarioContext.StepContext.StepInfo.StepDefinitionType} {ScenarioContext.StepContext.StepInfo.Text}");
        }

        protected virtual void NetworkWatcherHook()
        {
            if (WebDriverManager.IsInitialized)
                if(!WebDriver.HasAlert)
                    NetworkWatcher.AddUrl(WebDriver.Url);
        }

        protected virtual void WebDriverStepBasedLogging()
        {
            if(MovieLogger.IsEnabled)
                MovieLogger.ComposeMovie();
            WebElementSourceLog.Log(Logger);
            NetworkWatcher.Log(Logger);

        }

        protected virtual void ErrorScreenLogging()
        {
            if (ScenarioContext.TestError == null)
                return;

            if(WebDriverManager.ActiveDriver)
            {
                if(NetworkWatcher.BadUrl == null)
                {
                    NetworkWatcher.BadUrl = WebDriver.Url;
                }

                try
                {
                    FileManager.PersistFile(WebDriver.GetScreenshots().Last(),  $"final", "bmp");
                    FileManager.PersistFile(Encoding.UTF8.GetBytes(WebDriverManager.Current.PageSource), "source", "html");
                }
                catch (UnhandledAlertException) { return; }
            }
        }

        protected virtual void CheckForAlerts()
        {
            if (!WebDriverManager.ActiveDriver)
                return;
            WebDriver.AlertText.Should().BeNull($"An allert was left open at the end of the test with text:{WebDriver.AlertText}");
        }

        protected virtual void SetupInfrastructure()
        {
            IConfiguration config = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .AddEnvironmentVariables()
              .Build();

            NetworkWatcher = new NetworkWatcher();

            ScreenshotProcessor = ObjectContainer.Resolve<ScreenshotProcessor>();

            var configFactory = new ConfigurationFactory(config);

            ObjectContainer.RegisterInstanceAs(new ScenarioMetadata(() => ScenarioContext.TestError != null));
            ObjectContainer.RegisterInstanceAs(configFactory.Create<MovieLoggerConfig>());
            ObjectContainer.RegisterInstanceAs(configFactory.Create<ImageLoggingConfig>());
            WebElementSourceLog = new WebElementSourceLog();

            ImageLogging = new ImageLogging(ObjectContainer.Resolve<ImageLoggingConfig>());
            Register(new FileManager(new DatetimeManager(() => DateTime.Now)));
            FileManager.Initialize(FeatureContext.FeatureInfo.Title, ScenarioContext.ScenarioInfo.Title, null /*Specflow limitation*/);
            MovieLogger = new MovieLogger(FileManager, ObjectContainer.Resolve<MovieLoggerConfig>(), Metadata);

            ObjectContainer.RegisterInstanceAs(ImageLogging);
            ObjectContainer.RegisterInstanceAs(MovieLogger);

            Logger = new DefaultLogger(new DirectoryInfo(Environment.CurrentDirectory), new YamlLogFormatter());
            Register((PossumLabs.DSL.Core.Logging.ILog)Logger);
            Register<ElementFactory>(new ElementFactory());
            Register<XpathProvider>(new XpathProvider());
            Register<SelectorFactory>(new SelectorFactory(ElementFactory, XpathProvider).UseBootstrap());
            Register(new PossumLabs.DSL.Web.WebDriverManager(
                this.Interpeter,
                this.ObjectFactory,
                new SeleniumGridConfiguration()));

            var dataGeneratorRepository = new DataGeneratorRepository(Interpeter, ObjectFactory);
            Register<DataGenerator>(dataGeneratorRepository.BuildGenerator());

            Log.Message($"Feature: {FeatureContext.FeatureInfo.Title} Scenario: {ScenarioContext.ScenarioInfo.Title} \n" +
                $"Tags: {FeatureContext.FeatureInfo.Tags.LogFormat()} {ScenarioContext.ScenarioInfo.Tags.LogFormat()}");

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            WebDriverManager.Initialize(BuildDriver);
            WebDriverManager.WebDriverFactory = WebdriverFactory;
        }

        protected void LoadTemplates()
        {
            var templateManager = new PossumLabs.DSL.Core.Variables.TemplateManager();
            templateManager.Initialize(this.GetType().Assembly);
            Register(templateManager);
        }

        protected virtual Characteristics Transform(string id) => id;

        protected void LoadExistingData()
        {
            new PossumLabs.DSL.Core.Variables.ExistingDataManager(this.Interpeter, this.TemplateManager)
                .Initialize(this.GetType().Assembly);

        }

        protected virtual RemoteWebDriver WebdriverFactory()
        {
            var options = new ChromeOptions();

            //grid
            options.AddAdditionalCapability("username", WebDriverManager.SeleniumGridConfiguration.Username, true);
            options.AddAdditionalCapability("accessKey", WebDriverManager.SeleniumGridConfiguration.AccessKey, true);

            var driver = new RemoteWebDriver(new Uri(WebDriverManager.SeleniumGridConfiguration.Url), options.ToCapabilities(), TimeSpan.FromSeconds(180));
            //do not change this, the site is a bloody nightmare with overlaying buttons etc.
            driver.Manage().Window.Size = WebDriverManager.DefaultSize;
            var allowsDetection = driver as IAllowsFileDetection;
            if (allowsDetection != null)
            {
                allowsDetection.FileDetector = new LocalFileDetector();
            }
            return driver;
        }

        protected virtual WebDriver BuildDriver()
            => new WebDriver(
                WebDriverManager.Create(),
                () => WebDriverManager.BaseUrl,
                ObjectContainer.Resolve<SeleniumGridConfiguration>(),
                ObjectContainer.Resolve<RetryExecutor>(),
                SelectorFactory,
                ElementFactory,
                XpathProvider,
                ObjectContainer.Resolve<MovieLogger>(), 
                WebElementSourceLog);


        protected virtual void EnableMovieLogger()
            => MovieLogger.IsEnabled = true;

        protected virtual void CreateMovie()
        {
            if (MovieLogger.IsEnabled)
                MovieLogger.ComposeMovie();
        }
    }
}