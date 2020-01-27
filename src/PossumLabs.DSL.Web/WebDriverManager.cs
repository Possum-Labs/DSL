using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using PossumLabs.DSL.Core.Variables;
using PossumLabs.DSL.Web.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace PossumLabs.DSL.Web
{
    public class WebDriverManager:RepositoryBase<WebDriver>, IDisposable
    {
        public WebDriverManager(
            Interpeter interpeter, 
            ObjectFactory objectFactory, 
            SeleniumGridConfiguration seleniumGridConfiguration) : 
            base(interpeter, objectFactory)
        {
            SeleniumGridConfiguration = seleniumGridConfiguration;
            Screenshots = new List<byte[]>();
            DefaultDriver = new Lazy<WebDriver>(()=>DefaultDriverFactory());
            Drivers = new List<RemoteWebDriver>();
            DefaultSize = new System.Drawing.Size(SeleniumGridConfiguration.Width, SeleniumGridConfiguration.Height);
        }


        public void Initialize(Func<WebDriver> defaultDriverFactory)
        {
            DefaultDriverFactory = defaultDriverFactory;
        }
        public SeleniumGridConfiguration SeleniumGridConfiguration { get; }
        public WebDriver Current
        {
            get => OverWrittenDriver ?? DefaultDriver.Value;
        }
        public void SetCurrentDriver(WebDriver webdriver)
        { OverWrittenDriver = webdriver; }
        private WebDriver OverWrittenDriver { get; set; }
        private Lazy<WebDriver> DefaultDriver { get; set; }
        public Func<WebDriver> DefaultDriverFactory { get; private set; }
        public Uri BaseUrl { get; set; }

        public bool ActiveDriver => OverWrittenDriver != null || DefaultDriver.IsValueCreated;

        public List<byte[]> Screenshots { get; }

        public Func<RemoteWebDriver> WebDriverFactory { get
            {
                if (_WebDriverFactory == null)
                    throw new Exception("The WebDriverFactory needs to be set on WebDriverManager.");
                return _WebDriverFactory;
            }
            set => _WebDriverFactory = value; }

        private Func<RemoteWebDriver> _WebDriverFactory { get; set; }

        public IWebDriver Create() 
        {
            var driver = WebDriverFactory();
            Drivers.Add(driver);
            return driver;
        }

        public Size DefaultSize { get; set; }
        public bool IsInitialized => DefaultDriver.IsValueCreated;

        private List<RemoteWebDriver> Drivers;

        public DisposableDriverScope DisposableDriver()
        {
            var c = OverWrittenDriver;
            OverWrittenDriver = DefaultDriverFactory();
            return new DisposableDriverScope(
                () =>
                {
                    var d = ((IWebDriverWrapper)OverWrittenDriver).IWebDriver;
                    d.Quit();
                    d.Dispose();
                    OverWrittenDriver.Disposed = true;
                    OverWrittenDriver = c;
                });
        }

        public void Dispose()
        {
            foreach (var d in Drivers)
            {
                try
                {
                    d.Quit();
                    d.Dispose();
                }
                catch { }
            }
        }

        public class DisposableDriverScope : IDisposable
        {
            public DisposableDriverScope(Action disposal)
            {
                Disposal = disposal;
            }
            Action Disposal { get; }
            public void Dispose()
                => Disposal();
        }
    }
}
