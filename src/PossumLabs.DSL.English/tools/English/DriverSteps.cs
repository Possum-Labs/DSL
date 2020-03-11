using BoDi;
using PossumLabs.DSL;
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
        public  ActiveElementSelector TransformActiveElementSelectorEnglish(string Constructor)
            => base.TransformActiveElementSelector(Constructor);

        [StepArgumentTransformation]
        public  ContentSelector TransformContentSelectorEnglish(string Constructor)
            => base.TransformContentSelector(Constructor);

        [StepArgumentTransformation]
        public  CheckableElementSelector TransformCheckableElementSelectorEnglish(string Constructor)
            => base.TransformCheckableElementSelector(Constructor);

        [StepArgumentTransformation]
        public  ClickableElementSelector TransformClickableElementSelectorEnglish(string Constructor)
            => base.TransformClickableElementSelector(Constructor);

        [StepArgumentTransformation]
        public  SelectableElementSelector TransformSelectableElementSelectorrEnglish(string Constructor)
            => base.TransformSelectableElementSelectorr(Constructor);

        [StepArgumentTransformation]
        public  SettableElementSelector TransformSettableElementSelectorEnglish(string Constructor)
            => base.TransformSettableElementSelector(Constructor);


        //prefixes
        [StepArgumentTransformation]
        public  UnderSelectorPrefix TransformUnderSearcherPrefixEnglish(string Constructor)
            => base.TransformUnderSearcherPrefix(Constructor);

        [StepArgumentTransformation]
        public  RowSelectorPrefix TransformRowSearcherPrefixEnglish(string Constructor)
            => base.TransformRowSearcherPrefix(Constructor);

        [StepArgumentTransformation]
        public  WarningSelectorPrefix TransformWarningSearcherPrefixEnglish(string Constructor)
            => base.TransformWarningSearcherPrefix(Constructor);

        [StepArgumentTransformation]
        public  ErrorSelectorPrefix TransformErrorSearcherPrefixEnglish(string Constructor)
            => base.TransformErrorSearcherPrefix(Constructor);



        [AfterStep]
        public  void CleanupEnglish()
            => base.Cleanup();

        [When(@"clicking the element '(.*)'")]
        public  void WhenClickingTheElementEnglish(ActiveElementSelector selector)
            => base.WhenClickingTheElement(selector);

        [When(@"using javascript driven clear on '(.*)'")]
        public  void WhenScriptClearingEnglish(ActiveElementSelector selector)
            => base.WhenScriptClearing(selector);

        [When(@"using javascript setting '(.*)' for element '(.*)'")]
        public  void WhenScriptSettingEnglish(ResolvedString text, ActiveElementSelector selector)
            => base.WhenScriptSetting(text, selector);

        [When(@"selecting the element '(.*)'")]
        public  void WhenSelectingTheElementEnglish(ActiveElementSelector selector)
            => base.WhenSelectingTheElement(selector);

        [When(@"entering '(.*)' into element '(.*)'")]
        public  void WhenEnteringForTheElementEnglish(ResolvedString text, SettableElementSelector selector)
            => base.WhenEnteringForTheElement(text, selector);

        [When(@"for row '(.*)' clicking the element '(.*)'")]
        public  void WhenClickingTheElementRowEnglish(RowSelectorPrefix row, ActiveElementSelector selector)
            => base.WhenClickingTheElementRow(row, selector);

        [When(@"for row '(.*)' selecting the element '(.*)'")]
        public  void WhenSelectingTheElementRowEnglish(RowSelectorPrefix row, ActiveElementSelector selector)
            => base.WhenSelectingTheElementRow(row, selector);

        [When(@"for row '(.*)' entering '(.*)' into element '(.*)'")]
        public  void WhenEnteringForTheElementRowEnglish(RowSelectorPrefix row, ResolvedString text, SettableElementSelector selector)
            => base.WhenEnteringForTheElementRow(row, text, selector);

        [When(@"under '(.*)' clicking the element '(.*)'")]
        public  void WhenClickingTheElementUnderEnglish(UnderSelectorPrefix under, ActiveElementSelector selector)
            => base.WhenClickingTheElementUnder(under, selector);

        [When(@"under '(.*)' selecting the element '(.*)'")]
        public  void WhenSelectingTheElementUnderEnglish(UnderSelectorPrefix under, ActiveElementSelector selector)
            => base.WhenSelectingTheElementUnder(under, selector);

        [When(@"under '(.*)' entering '(.*)' into element '(.*)'")]
        public  void WhenEnteringForTheElementUnderEnglish(UnderSelectorPrefix under, ResolvedString text, SettableElementSelector selector)
            => base.WhenEnteringForTheElementUnder(under, text, selector);

        [Given(@"navigated to '(.*)'")]
        public  void GivenNavigatedToEnglish(string page)
            => base.GivenNavigatedTo(page);


        [When(@"selecting '(.*)' for element '(.*)'")]
        public  void WhenSelectingForElementEnglish(ResolvedString text, SelectableElementSelector selector)
            => base.WhenSelectingForElement(text, selector);

        [When(@"setting '(.*)' for element '(.*)'")]
        public  void WhenSettingTheElementEnglish(ResolvedString text, SettableElementSelector selector)
            => base.WhenSettingTheElement(text, selector);

        [When(@"checking element '(.*)'")]
        public  void WhenCheckingElementEnglish(CheckableElementSelector selector)
            => base.WhenCheckingElement(selector);

        [When(@"unchecking element '(.*)'")]
        public  void WhenUncheckingElementEnglish(CheckableElementSelector selector)
           => base.WhenUncheckingElement(selector);
    }
}
