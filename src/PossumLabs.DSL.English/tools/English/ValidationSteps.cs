using BoDi;
using PossumLabs.DSL;
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
        public  IEnumerable<IEnumerable<Validation>> TransformForContainsEnglish(Table table)
            => base.TransformForContains(table);

        [StepArgumentTransformation]
        public  IEnumerable<Validation> TransformForHasEnglish(Table table)
            => base.TransformForHas(table);

        [StepArgumentTransformation]
        public  Validation TransformValidationEnglish(string Constructor) 
            => base.TransformValidation(Constructor);

        [StepArgumentTransformation]
        public  object TransformEnglish(string id) => Interpeter.Get<object>(id);

        [Then(@"'(.*)' has the values")]
        public  void ThenTheCallHasTheValuesEnglish(object o, IEnumerable<Validation> validations)
            => base.ThenTheCallHasTheValues(o, validations);

        [Then(@"'(.*)' has the value '(.*)'")]
        public  void ThenTheCallHasTheValueEnglish(object o, Validation validation)
            => base.ThenTheCallHasTheValue(o, validation);

        [Then(@"'(.*)' contains the values?")]
        public  void ThenTheCallContainsTheValuesEnglish(object o, IEnumerable<IEnumerable<Validation>> validations)
            => base.ThenTheCallContainsTheValues(o, validations);

        [Then(@"'(.*)' contains the value '(.*)'")]
        public  void ThenTheCallContainsTheValueEnglish(object o, Validation validation)
            => base.ThenTheCallContainsTheValue(o, validation);
    }
}
