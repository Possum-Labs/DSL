using BoDi;
using FluentAssertions;
using PossumLabs.Specflow.Core;
using PossumLabs.Specflow.Core.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;

namespace Variables
{
    [Binding]
    public class ValidationSteps: StepBase
    {
        public ValidationSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
            ValidationFactory = new ValidationFactory(base.Interpeter);
        }

        private ValidationFactory ValidationFactory;

     

        [StepArgumentTransformation]
        public IEnumerable<IEnumerable<Validation>> TransformForContains(Table table) => 
            table.Rows.Select(r=>
                table.Header
                    .Where(h=>!String.IsNullOrWhiteSpace(r[h]))
                    .Select(h=>ValidationFactory.Create(r[h],h)))
            .ToArray();

        [StepArgumentTransformation]
        public IEnumerable<Validation> TransformForHas(Table table) =>
            table.Rows.SelectMany(r =>
                table.Header
                    .Where(h => !String.IsNullOrWhiteSpace(r[h]))
                    .Select(h => ValidationFactory.Create(r[h], h)))
            .ToArray();

        [StepArgumentTransformation]
        public Validation TransformValidation(string Constructor) => 
            ValidationFactory.Create(Constructor);

        [Then(@"'(.*)' has the values")]
        public void ThenTheCallHasTheValues(object o, IEnumerable<Validation> validations)
            => Executor.Execute(() => o.Validate(validations));

        [Then(@"'(.*)' has the value '(.*)'")]
        public void ThenTheCallHasTheValue(object o, Validation validation)
            => Executor.Execute(() => o.Validate(validation));

        [Then(@"'(.*)' contains the values?")]
        public void ThenTheCallContainsTheValues(object o, IEnumerable<IEnumerable<Validation>> validations)
            => Executor.Execute(() => o.ValidateContains(validations));

        [Then(@"'(.*)' contains the value '(.*)'")]
        public void ThenTheCallContainsTheValue(object o, Validation validation)
            => Executor.Execute(() => o.ValidateContains(validation));
    }
}
