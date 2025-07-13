using Reqnroll.BoDi;
using PossumLabs.DSL.Core;
using PossumLabs.DSL.Core.Validations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PossumLabs.DSL
{
    public abstract class ErrorStepsBase : StepsBase
    {
        public ErrorStepsBase(IObjectContainer objectContainer) : base(objectContainer)
        {

        }

        protected virtual void GivenAnErrorIsExpected()
            => Executor.ExpectException = true;

        protected virtual void ThenTheErrorHasValues(IEnumerable<Validation> validations)
            => Executor.Execute(() =>
            {
                if (Executor.Exception == null)
                    throw new GherkinException("No excetion was caught.");
                Flatten(Executor.Exception).Contains(validations);
            });

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
