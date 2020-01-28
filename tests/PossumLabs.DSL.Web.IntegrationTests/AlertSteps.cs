using BoDi;
using PossumLabs.DSL.Core.Validations;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace PossumLabs.DSL.Web.Integration
{
    [Binding]
    public class AlertSteps : WebDriverStepBase
    {
        public AlertSteps(IObjectContainer objectContainer) : base(objectContainer)
        { }


        [When(@"accepting the alert")]
        public void WhenAcceptingTheAlert()
           => Executor.Execute(()
           => WebDriver.AcceptAlert());

        [When(@"dismissing the alert")]
        public void WhenDismissingTheAlert()
           => Executor.Execute(()
           => WebDriver.DismissAlert());

        [Then(@"the alert has the value '(.*)'")]
        public void ThenTheCallHasTheValue(Validation validation)
            => Executor.Execute(() => WebDriver.AlertText.Validate(validation));
    }
}
