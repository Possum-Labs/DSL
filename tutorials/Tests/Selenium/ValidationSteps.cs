using BoDi;
using FluentAssertions;
using PossumLabs.Specflow.Core.Validations;
using PossumLabs.Specflow.Selenium;
using PossumLabs.Specflow.Selenium.Selectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Shim.Selenium
{
    [Binding]
    public class ValidationSteps : WebDriverStepBase
    {
        public ValidationSteps(IObjectContainer objectContainer) : base(objectContainer)
        { }

        [StepArgumentTransformation]
        public WebValidation TransformWebValidation(string Constructor) 
            => WebValidationFactory.Create(Constructor);

        [StepArgumentTransformation]
        public TableValidation TransformForHas(Table table) 
            => WebValidationFactory.Create(table.Rows.Select(r=>table.Header.ToDictionary(h=>h, h=> WebValidationFactory.Create(r[h]))).ToList());

        [Then(@"the element '(.*)' has the value '(.*)'")]
        public void ThenTheElementHasTheValue(ActiveElementSelector selector, WebValidation validation)
            => WebDriver.Select(selector).Validate(validation);

        [Then(@"under '(.*)' the element '(.*)' has the value '(.*)'")]
        public void ThenUnderTheElementHasTheValue(UnderSelectorPrefix prefix, ActiveElementSelector selector, WebValidation validation)
            => WebDriver.Under(prefix).Select(selector).Validate(validation);

        [Then(@"for row '(.*)' the element '(.*)' has the value '(.*)'")]
        public void ThenForRowTheElementHasTheValue(RowSelectorPrefix prefix, ActiveElementSelector selector, WebValidation validation)
            => WebDriver.ForRow(prefix).Select(selector).Validate(validation);

        [Then(@"the page contains the element '(.*)'")]
        public void ThenThePageContains(ActiveElementSelector selector)
            => WebDriver.Select(selector).Should().NotBeNull();

        [Then(@"the element '(.*)' is '(.*)'")]
        public void ThenTheElementIs(ActiveElementSelector selector, WebValidation validation)
            => WebDriver.Select(selector).Validate(validation);
    }
}
