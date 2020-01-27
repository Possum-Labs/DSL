using BoDi;
using PossumLabs.DSL.Core.Validations;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace PossumLabs.DSL.English
{
    [Binding]
    public class AlertSteps : AlertStepsBase
    {
        public AlertSteps(IObjectContainer objectContainer) : base(objectContainer)
        { }


        [When(@"accepting the alert")]
        public new void WhenAcceptingTheAlert()
           => Executor.Execute(()
           => WebDriver.AcceptAlert());

        [When(@"dismissing the alert")]
        public new void WhenDismissingTheAlert()
           => Executor.Execute(()
           => WebDriver.DismissAlert());

        [Then(@"the alert has the value '(.*)'")]
        public new void ThenTheCallHasTheValue(Validation validation)
            => Executor.Execute(() => WebDriver.AlertText.Validate(validation));
    }
}
