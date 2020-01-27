using BoDi;
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
        public new WebValidation TransformWebValidation(string Constructor) 
            => base.TransformWebValidation(Constructor);

        [StepArgumentTransformation]
        public new TableValidation TransformForHas(Table table) 
            => base.TransformForHas(table);

        [Then(@"the element '(.*)' has the value '(.*)'")]
        public new void ThenTheElementHasTheValue(ActiveElementSelector selector, WebValidation validation)
            => base.ThenTheElementHasTheValue(selector, validation);

        [Then(@"under '(.*)' the element '(.*)' has the value '(.*)'")]
        public new void ThenUnderTheElementHasTheValue(UnderSelectorPrefix prefix, ActiveElementSelector selector, WebValidation validation)
            => base.ThenUnderTheElementHasTheValue(prefix, selector, validation);

        [Then(@"for row '(.*)' the element '(.*)' has the value '(.*)'")]
        public new void ThenForRowTheElementHasTheValue(RowSelectorPrefix prefix, ActiveElementSelector selector, WebValidation validation)
            => base.ThenForRowTheElementHasTheValue(prefix, selector, validation);

        [Then(@"the page contains the element '(.*)'")]
        public new void ThenThePageContains(ActiveElementSelector selector)
            => base.ThenThePageContains(selector);

        [Then(@"the element '(.*)' is '(.*)'")]
        public new void ThenTheElementIs(ActiveElementSelector selector, WebValidation validation)
            => base.ThenTheElementIs(selector,validation);
    }
}
