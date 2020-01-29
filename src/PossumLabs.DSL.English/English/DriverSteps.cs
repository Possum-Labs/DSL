using BoDi;
using PossumLabs.DSL.Core;
using PossumLabs.DSL.Web.Selectors;
using TechTalk.SpecFlow;

namespace PossumLabs.DSL.English
{
    [Binding]
    public class DriverSteps: DriverStepsBase
    {
        public DriverSteps(IObjectContainer objectContainer) : base(objectContainer)
        { }

        //selectors
        [StepArgumentTransformation]
        public new ActiveElementSelector TransformActiveElementSelector(string Constructor)
            => base.TransformActiveElementSelector(Constructor);

        [StepArgumentTransformation]
        public new ContentSelector TransformContentSelector(string Constructor)
            => base.TransformContentSelector(Constructor);

        [StepArgumentTransformation]
        public new CheckableElementSelector TransformCheckableElementSelector(string Constructor)
            => base.TransformCheckableElementSelector(Constructor);

        [StepArgumentTransformation]
        public new ClickableElementSelector TransformClickableElementSelector(string Constructor)
            => base.TransformClickableElementSelector(Constructor);

        [StepArgumentTransformation]
        public new SelectableElementSelector TransformSelectableElementSelectorr(string Constructor)
            => base.TransformSelectableElementSelectorr(Constructor);

        [StepArgumentTransformation]
        public new SettableElementSelector TransformSettableElementSelector(string Constructor)
            => base.TransformSettableElementSelector(Constructor);


        //prefixes
        [StepArgumentTransformation]
        public new UnderSelectorPrefix TransformUnderSearcherPrefix(string Constructor)
            => base.TransformUnderSearcherPrefix(Constructor);

        [StepArgumentTransformation]
        public new RowSelectorPrefix TransformRowSearcherPrefix(string Constructor)
            => base.TransformRowSearcherPrefix(Constructor);

        [StepArgumentTransformation]
        public new WarningSelectorPrefix TransformWarningSearcherPrefix(string Constructor)
            => base.TransformWarningSearcherPrefix(Constructor);

        [StepArgumentTransformation]
        public new ErrorSelectorPrefix TransformErrorSearcherPrefix(string Constructor)
            => base.TransformErrorSearcherPrefix(Constructor);



        [AfterStep]
        public new void Cleanup()
            => base.Cleanup();

        [When(@"clicking the element '(.*)'")]
        public new void WhenClickingTheElement(ActiveElementSelector selector)
            => base.WhenClickingTheElement(selector);

        [When(@"using javascript driven clear on '(.*)'")]
        public new void WhenScriptClearing(ActiveElementSelector selector)
            => base.WhenScriptClearing(selector);

        [When(@"using javascript setting '(.*)' for element '(.*)'")]
        public new void WhenScriptSetting(ResolvedString text, ActiveElementSelector selector)
            => base.WhenScriptSetting(text, selector);

        [When(@"selecting the element '(.*)'")]
        public new void WhenSelectingTheElement(ActiveElementSelector selector)
            => base.WhenSelectingTheElement(selector);

        [When(@"entering '(.*)' into element '(.*)'")]
        public new void WhenEnteringForTheElement(ResolvedString text, SettableElementSelector selector)
            => base.WhenEnteringForTheElement(text, selector);

        [When(@"for row '(.*)' clicking the element '(.*)'")]
        public new void WhenClickingTheElementRow(RowSelectorPrefix row, ActiveElementSelector selector)
            => base.WhenClickingTheElementRow(row, selector);

        [When(@"for row '(.*)' selecting the element '(.*)'")]
        public new void WhenSelectingTheElementRow(RowSelectorPrefix row, ActiveElementSelector selector)
            => base.WhenSelectingTheElementRow(row, selector);

        [When(@"for row '(.*)' entering '(.*)' into element '(.*)'")]
        public new void WhenEnteringForTheElementRow(RowSelectorPrefix row, ResolvedString text, SettableElementSelector selector)
            => base.WhenEnteringForTheElementRow(row, text, selector);

        [When(@"under '(.*)' clicking the element '(.*)'")]
        public new void WhenClickingTheElementUnder(UnderSelectorPrefix under, ActiveElementSelector selector)
            => base.WhenClickingTheElementUnder(under, selector);

        [When(@"under '(.*)' selecting the element '(.*)'")]
        public new void WhenSelectingTheElementUnder(UnderSelectorPrefix under, ActiveElementSelector selector)
            => base.WhenSelectingTheElementUnder(under, selector);

        [When(@"under '(.*)' entering '(.*)' into element '(.*)'")]
        public new void WhenEnteringForTheElementUnder(UnderSelectorPrefix under, ResolvedString text, SettableElementSelector selector)
            => base.WhenEnteringForTheElementUnder(under, text, selector);

        [Given(@"navigated to '(.*)'")]
        public new void GivenNavigatedTo(string page)
            => base.GivenNavigatedTo(page);


        [When(@"selecting '(.*)' for element '(.*)'")]
        public new void WhenSelectingForElement(ResolvedString text, SelectableElementSelector selector)
            => base.WhenSelectingForElement(text, selector);

        [When(@"setting '(.*)' for element '(.*)'")]
        public new void WhenSettingTheElement(ResolvedString text, SettableElementSelector selector)
            => base.WhenSettingTheElement(text, selector);

        [When(@"checking element '(.*)'")]
        public new void WhenCheckingElement(CheckableElementSelector selector)
            => base.WhenCheckingElement(selector);

        [When(@"unchecking element '(.*)'")]
        public new void WhenUncheckingElement(CheckableElementSelector selector)
           => base.WhenUncheckingElement(selector);
    }
}
