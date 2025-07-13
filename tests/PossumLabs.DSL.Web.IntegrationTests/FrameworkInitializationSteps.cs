using Reqnroll.BoDi;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using PossumLabs.DSL.Core;
using PossumLabs.DSL.Core.Configuration;
using PossumLabs.DSL.Core.Exceptions;
using PossumLabs.DSL.Core.Files;
using PossumLabs.DSL.Core.Logging;
using PossumLabs.DSL.Core.Variables;
using PossumLabs.DSL.Web;
using PossumLabs.DSL.Web.ApplicationElements;
using PossumLabs.DSL.Web.Configuration;
using PossumLabs.DSL.Web.Diagnostic;
using PossumLabs.DSL.Web.Selectors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Reqnroll;

namespace PossumLabs.DSL.Web.Integration
{
    [Binding]
    public class FrameworkInitializationSteps : WebDriverStepBase
    {
        public FrameworkInitializationSteps(IObjectContainer objectContainer) : base(objectContainer) { }


        protected IScreenshotProcessor ScreenshotProcessor => ObjectContainer.Resolve<IScreenshotProcessor>();
        protected IImageLogging ImageLogging => ObjectContainer.Resolve<IImageLogging>();
        protected IMovieLogger MovieLogger => ObjectContainer.Resolve<IMovieLogger>();
        protected IWebElementSourceLog WebElementSourceLog => ObjectContainer.Resolve<IWebElementSourceLog>();
        protected INetworkWatcher NetworkWatcher => ObjectContainer.Resolve<INetworkWatcher>();
        protected ILog Logger => ObjectContainer.Resolve<ILog>();

        [AfterStep]
        public void LogStep()
        {
            MovieLogger.StepEnd($"{ScenarioContext.StepContext.StepInfo.StepDefinitionType} {ScenarioContext.StepContext.StepInfo.Text}");
        }

        [BeforeStep]
        public void NetworkWatcherHook()
        {
            if (WebDriverManager.IsInitialized)
                if(!WebDriver.HasAlert)
                    NetworkWatcher.AddUrl(WebDriver.Url);
        }

        [AfterScenario]
        public void LogScreenshots()
        {
            if(MovieLogger.IsEnabled)
                MovieLogger.ComposeMovie();
            WebElementSourceLog.Log(Logger);
            NetworkWatcher.Log(Logger);

        }

        [AfterScenario(Order = int.MinValue + 1)]
        public void ErrorScreenLogging()
        {
            if (ScenarioContext.TestError == null)
                return;

            if (WebDriverManager.ActiveDriver)
            {
                if (NetworkWatcher.BadUrl == null)
                {
                    NetworkWatcher.BadUrl = WebDriver.Url;
                }

                try
                {
                    FileManager.PersistFile(WebDriver.GetScreenshots().Last(), $"final", "bmp");
                    FileManager.PersistFile(Encoding.UTF8.GetBytes(WebDriverManager.Current.PageSource), "source", "html");
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


        protected virtual void IoCInitialialization()
        {
            ObjectContainer.RegisterTypeAs<ScreenshotProcessor, IScreenshotProcessor>();
            ObjectContainer.RegisterTypeAs<NetworkWatcher, INetworkWatcher>();
            ObjectContainer.RegisterTypeAs<ConfigurationFactory, IConfigurationFactory>();
            ObjectContainer.RegisterTypeAs<Interpeter, IInterpeter>();
            ObjectContainer.RegisterTypeAs<ActionExecutor, PossumLabs.DSL.Core.Exceptions.IActionExecutor>();
            ObjectContainer.RegisterTypeAs<ObjectFactory, IObjectFactory>();
            ObjectContainer.RegisterTypeAs<TemplateManager, ITemplateManager>();
            ObjectContainer.RegisterTypeAs<WebElementSourceLog, IWebElementSourceLog>();
            ObjectContainer.RegisterTypeAs<ImageLogging, IImageLogging>();
            ObjectContainer.RegisterTypeAs<FileManager, IFileManager>();
            ObjectContainer.RegisterTypeAs<MovieLogger, IMovieLogger>();
            ObjectContainer.RegisterTypeAs<YamlLogFormatter, ILogFormatter>();
            ObjectContainer.RegisterTypeAs<DefaultLogger, ILog>();
            ObjectContainer.RegisterTypeAs<ApplicationElementRegistry, IApplicationElementRegistry>();
            ObjectContainer.RegisterTypeAs<ElementFactory, IElementFactory>();
            ObjectContainer.RegisterTypeAs<XpathProvider, IXpathProvider>();
            ObjectContainer.RegisterTypeAs<SelectorFactory, ISelectorFactory>();
            ObjectContainer.RegisterTypeAs<ExistingDataManager, IExistingDataManager>();
            ObjectContainer.RegisterTypeAs<RetryExecutor, IRetryExecutor>();
            ObjectContainer.RegisterTypeAs<WebValidationFactory, IWebValidationFactory>();
        }

        [BeforeScenario(Order = int.MinValue + 1)]
        protected virtual void SetupInfrastructure()
        {
            IConfiguration config = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .AddEnvironmentVariables()
              .Build();
            ObjectContainer.RegisterInstanceAs(config);
            ObjectContainer.RegisterInstanceAs(new DatetimeManager(() => DateTime.Now));
            ObjectContainer.RegisterInstanceAs(new DirectoryInfo(Environment.CurrentDirectory));
            ObjectContainer.RegisterInstanceAs(new ScenarioMetadata(() => ScenarioContext.TestError != null));

            IoCInitialialization();

            var configFactory = ObjectContainer.Resolve<IConfigurationFactory>();
            ObjectContainer.RegisterInstanceAs(configFactory.Create<MovieLoggerConfig>());
            ObjectContainer.RegisterInstanceAs(configFactory.Create<ImageLoggingConfig>());
            ObjectContainer.RegisterInstanceAs(configFactory.Create<SeleniumGridConfiguration>());

            ObjectContainer.Resolve<ISelectorFactory>().UseBootstrap();
            ObjectContainer.Resolve<IFileManager>()
                .Initialize(FeatureContext.FeatureInfo.Title,
                ScenarioContext.ScenarioInfo.Title,
                null /*Specflow limitation*/);

            Log.Message($"Feature: {FeatureContext.FeatureInfo.Title} Scenario: {ScenarioContext.ScenarioInfo.Title} \n" +
                $"Tags: {FeatureContext.FeatureInfo.Tags.LogFormat()} {ScenarioContext.ScenarioInfo.Tags.LogFormat()}");

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            WebDriverManager.Initialize(BuildDriver);
            WebDriverManager.WebDriverFactory = WebdriverFactory;
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
                ObjectContainer.Resolve<MovieLogger>(), 
                WebElementSourceLog);

        protected virtual RemoteWebDriver WebdriverFactory()
        {
            var options = new ChromeOptions();

            //grid
            options.AddAdditionalOption("username", WebDriverManager.SeleniumGridConfiguration.Username);
            options.AddAdditionalOption("accessKey", WebDriverManager.SeleniumGridConfiguration.AccessKey);

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

        [BeforeScenario(Order = 11)]
        protected void LoadTemplates()
        {
            ObjectContainer.Resolve<ITemplateManager>()
                .Initialize(this.GetType().Assembly);
        }
        protected void LoadExistingData()
        {
            ObjectContainer.Resolve<IExistingDataManager>()
                .Initialize(this.GetType().Assembly);

        }
    }
}
