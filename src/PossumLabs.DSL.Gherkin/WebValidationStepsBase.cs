using BoDi;
using FluentAssertions;
using PossumLabs.DSL.Core.Validations;
using PossumLabs.DSL.Web;
using PossumLabs.DSL.Web.Selectors;
using System.Linq;
using TechTalk.SpecFlow;

namespace PossumLabs.DSL
{
    public abstract class WebValidationStepsBase : WebDriverStepsBase
    {
        public WebValidationStepsBase(IObjectContainer objectContainer) : base(objectContainer)
        { }

        protected virtual WebValidation TransformWebValidation(string Constructor) 
            => WebValidationFactory.Create(Constructor);

        protected virtual TableValidation TransformForHas(Table table) 
            => WebValidationFactory.Create(table.Rows.Select(r=>table.Header.ToDictionary(h=>h, h=> WebValidationFactory.Create(r[h]))).ToList());

        protected virtual void ThenTheElementHasTheValue(ActiveElementSelector selector, WebValidation validation)
            => WebDriver.Select(selector).Validate(validation);

        protected virtual void ThenUnderTheElementHasTheValue(UnderSelectorPrefix prefix, ActiveElementSelector selector, WebValidation validation)
            => WebDriver.Under(prefix).Select(selector).Validate(validation);


        protected virtual void ThenForRowTheElementHasTheValue(RowSelectorPrefix prefix, ActiveElementSelector selector, WebValidation validation)
            => WebDriver.ForRow(prefix).Select(selector).Validate(validation);

        protected virtual void ThenThePageContains(ActiveElementSelector selector)
            => WebDriver.Select(selector).Should().NotBeNull();

        protected virtual void ThenTheElementIs(ActiveElementSelector selector, WebValidation validation)
            => WebDriver.Select(selector).Validate(validation);
    }
}
