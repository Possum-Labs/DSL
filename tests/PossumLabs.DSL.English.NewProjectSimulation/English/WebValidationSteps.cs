using BoDi;
using PossumLabs.DSL;
using PossumLabs.DSL.Web;
using PossumLabs.DSL.Web.Selectors;
using TechTalk.SpecFlow;

namespace PossumLabs.DSL.English
{
    [Binding]
    public class WebValidationSteps : WebValidationStepsBase
    {
        public WebValidationSteps(IObjectContainer objectContainer) : base(objectContainer)
        { }

        [StepArgumentTransformation]
        public  WebValidation TransformWebValidationEnglish(string Constructor) 
            => base.TransformWebValidation(Constructor);

        [StepArgumentTransformation]
        public  TableValidation TransformForHasEnglish(Table table) 
            => base.TransformForHas(table);

        [Then(@"the element '(.*)' has the value '(.*)'")]
        public  void ThenTheElementHasTheValueEnglish(ActiveElementSelector selector, WebValidation validation)
            => base.ThenTheElementHasTheValue(selector, validation);

        [Then(@"under '(.*)' the element '(.*)' has the value '(.*)'")]
        public  void ThenUnderTheElementHasTheValueEnglish(UnderSelectorPrefix prefix, ActiveElementSelector selector, WebValidation validation)
            => base.ThenUnderTheElementHasTheValue(prefix, selector, validation);

        [Then(@"for row '(.*)' the element '(.*)' has the value '(.*)'")]
        public  void ThenForRowTheElementHasTheValueEnglish(RowSelectorPrefix prefix, ActiveElementSelector selector, WebValidation validation)
            => base.ThenForRowTheElementHasTheValue(prefix, selector, validation);

        [Then(@"the page contains the element '(.*)'")]
        public  void ThenThePageContainsEnglish(ActiveElementSelector selector)
            => base.ThenThePageContains(selector);

        [Then(@"the element '(.*)' is '(.*)'")]
        public  void ThenTheElementIsEnglish(ActiveElementSelector selector, WebValidation validation)
            => base.ThenTheElementIs(selector,validation);
    }
}
