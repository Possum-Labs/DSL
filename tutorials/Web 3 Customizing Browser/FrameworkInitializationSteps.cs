using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;

namespace DSL.Documentation.Example
{
    public class FrameworkInitializationSteps : PossumLabs.DSL.English.FrameworkInitializationSteps
    {
        public FrameworkInitializationSteps(IObjectContainer objectContainer) : base(objectContainer) { }

        protected override RemoteWebDriver WebdriverFactory()
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
    }
}
