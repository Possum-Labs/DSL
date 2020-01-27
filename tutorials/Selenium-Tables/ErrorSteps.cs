using BoDi;
using FluentAssertions;
using PossumLabs.Specflow.Core;
using PossumLabs.Specflow.Core.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Selenium_Tables
{
    [Binding]
    public class ErrorSteps : StepBase
    {
        public ErrorSteps(IObjectContainer objectContainer) : base(objectContainer)
        {

        }

        [Given(@"an error is expected")]
        public void GivenAnErrorIsExpected()
            => Executor.ExpectException = true;

        [Then(@"the Error has values")]
        public void ThenTheErrorHasValues(IEnumerable<Validation> validations)
        {
            if (Executor.Exception == null)
                throw new GherkinException("No excetion was caught.");
            Flatten(Executor.Exception).Contains(validations).Should().BeTrue("found the exceptions :"+Flatten(Executor.Exception).Select(x=>x.Message).LogFormat());
        }

        private IEnumerable<Exception> Flatten(Exception ex)
        {
            var l = new List<Exception>();
            if(ex is AggregateException)
            {
                foreach (var e in ((AggregateException)ex).InnerExceptions)
                    l.AddRange(Flatten(e));
            }
            if (ex.InnerException != null)
                l.AddRange(Flatten(ex.InnerException));
            l.Add(ex);
            return l;
        }
    }
}
