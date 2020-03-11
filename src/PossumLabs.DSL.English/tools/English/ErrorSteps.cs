using BoDi;
using PossumLabs.DSL;
using PossumLabs.DSL.Core.Validations;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace PossumLabs.DSL.English
{
    [Binding]
    public class ErrorSteps : ErrorStepsBase
    {
        public ErrorSteps(IObjectContainer objectContainer) : base(objectContainer)
        {

        }

        [Given(@"an error is expected")]
        public  void GivenAnErrorIsExpectedEnglish()
            => base.GivenAnErrorIsExpected();

        [Then(@"the Error has values")]
        public  void ThenTheErrorHasValuesEnglish(IEnumerable<Validation> validations)
            => base.ThenTheErrorHasValues(validations);
    }
}
