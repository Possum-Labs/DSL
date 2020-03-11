using BoDi;
using PossumLabs.DSL;
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
        public  void WhenAcceptingTheAlertEnglish()
           => base.WhenAcceptingTheAlert();

        [When(@"dismissing the alert")]
        public  void WhenDismissingTheAlertEnglish()
           => base.WhenDismissingTheAlert();

        [Then(@"the alert has the value '(.*)'")]
        public  void ThenTheCallHasTheValueEnglish(Validation validation)
            => base.ThenTheCallHasTheValue(validation);
    }
}
