using BoDi;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Selenium_Tables
{
    [Binding]
    public class MockHtmlSteps : StepBase
    {
        public MockHtmlSteps(IObjectContainer objectContainer) : base(objectContainer)
        { }

        [Given(@"injecting browser content")]
        public void GivenInjectingBrowserContent(List<HtmlInjection> htmlInjections)
        {
            htmlInjections.Should().HaveCount(1, "you can only set one piece of html");
            WebDriver.LoadPage(htmlInjections.First().Content);
        }

        [AfterScenario("injected-html", Order = Int32.MinValue)]
        public void ThenLogHtml()
        {
            Console.WriteLine(WebDriver.PageSource);
        }
    }
}
