using BoDi;
using PossumLabs.DSL.Core.Validations;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace PossumLabs.DSL.English
{
    [Binding]
    public class ValidationSteps: ValidationStepsBase
    {
        public ValidationSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
        }

        [StepArgumentTransformation]
        public new IEnumerable<IEnumerable<Validation>> TransformForContains(Table table)
            => base.TransformForContains(table);

        [StepArgumentTransformation]
        public new IEnumerable<Validation> TransformForHas(Table table)
            => base.TransformForHas(table);

        [StepArgumentTransformation]
        public new Validation TransformValidation(string Constructor) 
            => base.TransformValidation(Constructor);

        [StepArgumentTransformation]
        public new object Transform(string id) => Interpeter.Get<object>(id);

        [Then(@"'(.*)' has the values")]
        public new void ThenTheCallHasTheValues(object o, IEnumerable<Validation> validations)
            => base.ThenTheCallHasTheValues(o, validations);

        [Then(@"'(.*)' has the value '(.*)'")]
        public new void ThenTheCallHasTheValue(object o, Validation validation)
            => base.ThenTheCallHasTheValue(o, validation);

        [Then(@"'(.*)' contains the values?")]
        public new void ThenTheCallContainsTheValues(object o, IEnumerable<IEnumerable<Validation>> validations)
            => base.ThenTheCallContainsTheValues(o, validations);

        [Then(@"'(.*)' contains the value '(.*)'")]
        public new void ThenTheCallContainsTheValue(object o, Validation validation)
            => base.ThenTheCallContainsTheValue(o, validation);
    }
}
