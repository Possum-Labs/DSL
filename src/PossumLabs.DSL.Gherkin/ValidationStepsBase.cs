using Reqnroll.BoDi;
using PossumLabs.DSL.Core.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using Reqnroll;

namespace PossumLabs.DSL
{
    public abstract class ValidationStepsBase: StepsBase
    {
        public ValidationStepsBase(IObjectContainer objectContainer) : base(objectContainer)
        {
            ValidationFactory = new ValidationFactory(base.Interpeter);
        }

        private ValidationFactory ValidationFactory;

        protected virtual IEnumerable<IEnumerable<Validation>> TransformForContains(Table table) => 
            table.Rows.Select(r=>
                table.Header
                    .Where(h=>!String.IsNullOrWhiteSpace(r[h]))
                    .Select(h=>ValidationFactory.Create(r[h],h)))
            .ToArray();

        protected virtual IEnumerable<Validation> TransformForHas(Table table) =>
            table.Rows.SelectMany(r =>
                table.Header
                    .Where(h => !String.IsNullOrWhiteSpace(r[h]))
                    .Select(h => ValidationFactory.Create(r[h], h)))
            .ToArray();

        protected virtual Validation TransformValidation(string Constructor) => 
            ValidationFactory.Create(Constructor);

        protected virtual object Transform(string id) => Interpeter.Get<object>(id);

        protected virtual void ThenTheCallHasTheValues(object o, IEnumerable<Validation> validations)
            => Executor.Execute(() => o.Validate(validations));

        protected virtual void ThenTheCallHasTheValue(object o, Validation validation)
            => Executor.Execute(() => o.Validate(validation));

        protected virtual void ThenTheCallContainsTheValues(object o, IEnumerable<IEnumerable<Validation>> validations)
            => Executor.Execute(() => o.ValidateContains(validations));

        protected virtual void ThenTheCallContainsTheValue(object o, Validation validation)
            => Executor.Execute(() => o.ValidateContains(validation));
    }
}
