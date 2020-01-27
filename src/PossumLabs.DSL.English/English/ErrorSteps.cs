using BoDi;
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
        public new void GivenAnErrorIsExpected()
            => base.GivenAnErrorIsExpected();

        [Then(@"the Error has values")]
        public new void ThenTheErrorHasValues(IEnumerable<Validation> validations)
            => base.ThenTheErrorHasValues(validations);
    }
}
