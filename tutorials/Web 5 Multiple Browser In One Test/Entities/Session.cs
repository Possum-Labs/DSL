using BoDi;
using FluentAssertions;
using PossumLabs.DSL;
using PossumLabs.DSL.Core;
using PossumLabs.DSL.Core.Exceptions;
using PossumLabs.DSL.Core.Logging;
using PossumLabs.DSL.Web;
using PossumLabs.DSL.Web.Configuration;
using PossumLabs.DSL.Web.Selectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace DSL.Documentation.Example
{
    public class Session : PossumLabs.DSL.Core.Variables.IValueObject
    {
        public WebDriver Driver { get; set; }
    }

    [Binding]
    public class SessionSteps : RepositoryStepBase<Session>
    {
        public SessionSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
            _keepAlive = new Timer(checkDrivers);

            _keepAlive.Change(10000, 10000);
        }

        protected WebDriver WebDriver => ObjectContainer.Resolve<WebDriverManager>().Current;
        protected WebDriverManager WebDriverManager => ObjectContainer.Resolve<WebDriverManager>();

        //we need to regularly communicate with all browsers to keep out connection alive.
        // we just grab that url for this requirement.
        private readonly Timer _keepAlive;
        private void checkDrivers(object state)
        {
            var currentUrls = new List<string>();
            Parallel.ForEach(base.Repository, driver =>
            {
                if (this.WebDriver != this[driver.Key].Driver && !driver.Value.Driver.Disposed)
                {
                    try
                    {
                        //bogus check, keep allive
                        var url = this[driver.Key].Driver.SeleniumDriver.Url;
                        lock (currentUrls)
                            currentUrls.Add(url);
                    }
                    catch { }
                }
            });
        }

        public WebDriver BuildDriver()
            => new WebDriver(
                WebDriverManager.Create(),
                () => WebDriverManager.BaseUrl,
                ObjectContainer.Resolve<SeleniumGridConfiguration>(),
                ObjectContainer.Resolve<RetryExecutor>(),
                ObjectContainer.Resolve<SelectorFactory>(),
                ObjectContainer.Resolve<ElementFactory>(),
                ObjectContainer.Resolve<XpathProvider>(),
                ObjectContainer.Resolve<MovieLogger>(),
                ObjectContainer.Resolve<WebElementSourceLog>());

        // creates sessions to use later, we will leave it on the last session created.
        [Given(@"the Sessions?")]
        private void CreateSession(Dictionary<string, Session> sessions)
        {
            foreach(var s in sessions.Values)
                s.Driver = BuildDriver();
            foreach(var key in sessions.Keys)
                base.Repository.Add(key, sessions[key]);
            if(sessions.Values.Any())
                SwapSession(sessions.Values.Last());
        }

        //swap to an other session.
        [Given(@"the Session '(\w+)'")]
        private void SwapSession(Session s)
        {
            WebDriverManager.SetCurrentDriver(s.Driver);
            s.Driver.Should().Be(WebDriver, "the selenium driver was not properly swapped");
        }

        [AfterScenario(Order = 100)]
        public void TeardownSelenium()
            => OnError.Continue(() =>
            {
                _keepAlive.Dispose();
            });
    }
}
