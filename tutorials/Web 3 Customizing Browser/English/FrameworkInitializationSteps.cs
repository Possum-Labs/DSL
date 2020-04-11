using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using PossumLabs.DSL.Core.Variables;
using System;
using TechTalk.SpecFlow;

namespace PossumLabs.DSL.English
{
    [Binding]
    public class FrameworkInitializationSteps : FrameworkInitializationStepsBase
    {
        public FrameworkInitializationSteps(IObjectContainer objectContainer) : base(objectContainer) { }

        /// <summary>
        /// All urls will automatically be prefixed with the specified url.
        /// This is ideally configuration driven, and would allow you re-traget your tests
        /// with a configuraiton change.
        /// </summary>
        [BeforeScenario]
        public void SetDefaultUrl()
            => WebDriverManager.BaseUrl = new Uri(@"http://www.PossumLabs.com/");

        /// <summary>
        /// you can swap this to return any other kind of RemoteWebDriver
        /// </summary>
        protected override RemoteWebDriver WebdriverFactory()
        {
            var options = new ChromeOptions();

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

        [StepArgumentTransformation]
        public Characteristics TransformEnglish(string id) => base.Transform(id);

        [AfterStep]
        public  void LogStepEnglish()
            => base.LogStep();

        [BeforeStep]
        public  void NetworkWatcherHookEnglish()
            => base.NetworkWatcherHook();

        [AfterScenario(Order = 1)]
        public  void WebDriverStepBasedLoggingEnglish()
            => base.WebDriverStepBasedLogging();

        [AfterScenario(Order = int.MinValue + 1)]
        public  void ErrorScreenLoggingEnglish()
            => base.ErrorScreenLogging();

        [AfterScenario(Order = int.MinValue)]
        public  void CheckForAlertsEnglish()
            => base.CheckForAlerts();

        [BeforeScenario(Order = int.MinValue+1)]
        public  virtual void SetupInfrastructureEnglish()
            => base.SetupInfrastructure();

        [BeforeScenario(Order = int.MinValue + 11)]
        public virtual void LoadTemplatesEnglish()
        => LoadTemplates();
        

        [BeforeScenario(Order = int.MinValue + 12)]
        public virtual void LoadExistingDataEnglish()
            => LoadExistingData();

        [BeforeScenario("Movie-Logger")]
        public  void EnableMovieLoggerEnglish()
            => base.EnableMovieLogger();

        [AfterScenario]
        public  void CreateMovieEnglish()
            => base.CreateMovie();
    }
}
