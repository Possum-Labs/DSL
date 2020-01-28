using BoDi;
using PossumLabs.DSL.Core.Validations;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace PossumLabs.DSL.Web.Integration
{
    [Binding]
    public class LogSteps: WebDriverStepBase
    {
        public LogSteps(IObjectContainer objectContainer) : base(objectContainer)
        { }


        [Then(@"the Browser Logs has the value '(.*)'")]
        public void ThenTheBrowserLogsHasTheValue(Validation validation)
            => Executor.Execute(() => WebDriver.BrowserLogs.Validate(validation));

    }
}
