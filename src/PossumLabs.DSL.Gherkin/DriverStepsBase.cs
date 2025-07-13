using Reqnroll.BoDi;
using PossumLabs.DSL.Core;
using PossumLabs.DSL.Web;
using PossumLabs.DSL.Web.Selectors;

namespace PossumLabs.DSL
{

    public abstract class DriverStepsBase: WebDriverStepsBase
    {
        public DriverStepsBase(IObjectContainer objectContainer) : base(objectContainer)
        { }

        //selectors
        protected virtual ActiveElementSelector TransformActiveElementSelector(string Constructor)
            => SelectorFactory.CreateSelector<ActiveElementSelector>(Interpeter.Get<string>(Constructor));

        protected virtual ContentSelector TransformContentSelector(string Constructor)
            => SelectorFactory.CreateSelector<ContentSelector>(Interpeter.Get<string>(Constructor));

        protected virtual CheckableElementSelector TransformCheckableElementSelector(string Constructor)
            => SelectorFactory.CreateSelector<CheckableElementSelector>(Interpeter.Get<string>(Constructor));

        protected virtual ClickableElementSelector TransformClickableElementSelector(string Constructor)
            => SelectorFactory.CreateSelector<ClickableElementSelector>(Interpeter.Get<string>(Constructor));

        protected virtual SelectableElementSelector TransformSelectableElementSelectorr(string Constructor)
            => SelectorFactory.CreateSelector<SelectableElementSelector>(Interpeter.Get<string>(Constructor));

        protected virtual SettableElementSelector TransformSettableElementSelector(string Constructor)
            => SelectorFactory.CreateSelector<SettableElementSelector>(Interpeter.Get<string>(Constructor));


        //prefixes
        protected virtual UnderSelectorPrefix TransformUnderSearcherPrefix(string Constructor)
            => SelectorFactory.CreatePrefix<UnderSelectorPrefix>(Interpeter.Get<string>(Constructor));

        protected virtual RowSelectorPrefix TransformRowSearcherPrefix(string Constructor)
            => SelectorFactory.CreatePrefix<RowSelectorPrefix>(Interpeter.Get<string>(Constructor));

        protected virtual WarningSelectorPrefix TransformWarningSearcherPrefix(string Constructor)
            => SelectorFactory.CreatePrefix<WarningSelectorPrefix>(Interpeter.Get<string>(Constructor));

        protected virtual ErrorSelectorPrefix TransformErrorSearcherPrefix(string Constructor)
            => SelectorFactory.CreatePrefix<ErrorSelectorPrefix>(Interpeter.Get<string>(Constructor));

        protected virtual void Cleanup()
        {
            if (WebDriverManager.IsInitialized && WebDriver != null && !WebDriver.HasAlert)
                WebDriver.LeaveFrames();
        }

        public void WhenClickingTheElement(string selector)
            => WhenClickingTheElement(SelectorFactory.CreateSelector<ActiveElementSelector>(selector));

        protected virtual void WhenClickingTheElement(ActiveElementSelector selector)
            => Executor.Execute(()
            => WebDriver.Select(selector).Click());

        public void WhenScriptClearing(string selector)
           => WhenScriptClearing(SelectorFactory.CreateSelector<ActiveElementSelector>(selector));

        protected virtual void WhenScriptClearing(ActiveElementSelector selector)
            => Executor.Execute(()
            => WebDriver.Select(selector).ScriptClear());

        public void WhenScriptSetting(string text, string selector)
           => WhenScriptSetting(text, SelectorFactory.CreateSelector<ActiveElementSelector>(selector));

        protected virtual void WhenScriptSetting(ResolvedString text, ActiveElementSelector selector)
            => Executor.Execute(()
            => WebDriver.Select(selector).ScriptSet(text));

        public void WhenSelectingTheElement(string selector)
           => WhenSelectingTheElement(SelectorFactory.CreateSelector<ActiveElementSelector>(selector));

        protected virtual void WhenSelectingTheElement(ActiveElementSelector selector)
            => Executor.Execute(()
            => WebDriver.Select(selector).Select());

        public void WhenEnteringForTheElement(string text, string selector)
           => WhenEnteringForTheElement(text, SelectorFactory.CreateSelector<SettableElementSelector>(selector));

        protected virtual void WhenEnteringForTheElement(ResolvedString text, SettableElementSelector selector)
            => Executor.Execute(()
            => WebDriver.Select(selector).Enter(text));

        public void WhenClickingTheElementRow(string row, string selector)
           => WhenClickingTheElementRow(
               SelectorFactory.CreatePrefix<RowSelectorPrefix>(row),
               SelectorFactory.CreateSelector<ActiveElementSelector>(selector));

        protected virtual void WhenClickingTheElementRow(RowSelectorPrefix row, ActiveElementSelector selector)
            => Executor.Execute(()
            => WebDriver.ForRow(row).Select(selector).Click());

        public void WhenSelectingTheElementRow(string row, string selector)
           => WhenSelectingTheElementRow(
               SelectorFactory.CreatePrefix<RowSelectorPrefix>(row),
               SelectorFactory.CreateSelector<ActiveElementSelector>(selector));
        protected virtual void WhenSelectingTheElementRow(RowSelectorPrefix row, ActiveElementSelector selector)
            => Executor.Execute(()
            => WebDriver.ForRow(row).Select(selector).Select());

        public void WhenEnteringForTheElementRow(string row, string text, string selector)
           => WhenEnteringForTheElementRow(
               SelectorFactory.CreatePrefix<RowSelectorPrefix>(row),
               text,
               SelectorFactory.CreateSelector<SettableElementSelector>(selector));

        protected virtual void WhenEnteringForTheElementRow(RowSelectorPrefix row, ResolvedString text, SettableElementSelector selector)
            => Executor.Execute(()
            => WebDriver.ForRow(row).Select(selector).Enter(text));

        public void WhenClickingTheElementUnder(string under, string selector)
           => WhenClickingTheElementUnder(
               SelectorFactory.CreatePrefix<UnderSelectorPrefix>(under),
               SelectorFactory.CreateSelector<ActiveElementSelector>(selector));

        protected virtual void WhenClickingTheElementUnder(UnderSelectorPrefix under, ActiveElementSelector selector)
            => Executor.Execute(()
            => WebDriver.Under(under).Select(selector).Click());

        public void WhenSelectingTheElementUnder(string under, string selector)
           => WhenSelectingTheElementUnder(
               SelectorFactory.CreatePrefix<UnderSelectorPrefix>(under),
               SelectorFactory.CreateSelector<ActiveElementSelector>(selector));

        protected virtual void WhenSelectingTheElementUnder(UnderSelectorPrefix under, ActiveElementSelector selector)
            => Executor.Execute(()
            => WebDriver.Under(under).Select(selector).Select());

        public void WhenEnteringForTheElementUnder(string under, string text, string selector)
           => WhenEnteringForTheElementUnder(
               SelectorFactory.CreatePrefix<UnderSelectorPrefix>(under),
               text,
               SelectorFactory.CreateSelector<SettableElementSelector>(selector));

        protected virtual void WhenEnteringForTheElementUnder(UnderSelectorPrefix under, ResolvedString text, SettableElementSelector selector)
            => Executor.Execute(()
            => WebDriver.Under(under).Select(selector).Enter(text));

        protected virtual void GivenNavigatedTo(string page)
            => Executor.Execute(()
            => WebDriver.NavigateTo(page));

        public void WhenSelectingForElement(string text, string selector)
           => WhenSelectingForElement(
               text,
               SelectorFactory.CreateSelector<SelectableElementSelector>(selector));

        protected virtual void WhenSelectingForElement(ResolvedString text, SelectableElementSelector selector)
            => Executor.Execute(()
            => WebDriver.Select(selector).Enter(text));

        public void WhenSettingTheElement(string text, string selector)
           => WhenSettingTheElement(
               text,
               SelectorFactory.CreateSelector<SettableElementSelector>(selector));

        protected virtual void WhenSettingTheElement(ResolvedString text, SettableElementSelector selector)
            => Executor.Execute(()
            => WebDriver.Select(selector).Enter(text));

        public void WhenCheckingElement(string selector)
           => WhenCheckingElement(SelectorFactory.CreateSelector<CheckableElementSelector>(selector));

        protected virtual void WhenCheckingElement(CheckableElementSelector selector)
            => Executor.Execute(()
            => WebDriver.Select(selector).Enter("checked"));

        public void WhenUncheckingElement(string selector)
           => WhenUncheckingElement(SelectorFactory.CreateSelector<CheckableElementSelector>(selector));

        protected virtual void WhenUncheckingElement(CheckableElementSelector selector)
           => Executor.Execute(()
           => WebDriver.Select(selector).Enter("unchecked"));
    }
}
