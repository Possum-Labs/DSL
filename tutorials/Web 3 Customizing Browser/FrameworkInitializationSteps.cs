using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using TechTalk.SpecFlow;

namespace DSL.Documentation.Example
{
    public class FrameworkInitializationSteps : PossumLabs.DSL.English.FrameworkInitializationSteps
    {
        public FrameworkInitializationSteps(IObjectContainer objectContainer) : base(objectContainer) { }

        /// <summary>
        /// All urls will automatically be prefixed with the specified url.
        /// This is ideally configuration driven, and would allow you re-traget your tests
        /// with a configuraiton change.
        /// </summary>
        [BeforeScenario]
        public void SetDefaultUrl()
            => WebDriverManager.BaseUrl = new Uri(@"https://www.google.com/");

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
    }
}
