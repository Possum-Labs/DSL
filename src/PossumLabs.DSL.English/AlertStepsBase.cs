using BoDi;
using PossumLabs.DSL.Core.Validations;

namespace PossumLabs.DSL
{

    public abstract class AlertStepsBase : WebDriverStepsBase
    {
        public AlertStepsBase(IObjectContainer objectContainer) : base(objectContainer)
        { }


        protected virtual void WhenAcceptingTheAlert()
           => Executor.Execute(()
           => WebDriver.AcceptAlert());

   
        protected virtual void WhenDismissingTheAlert()
           => Executor.Execute(()
           => WebDriver.DismissAlert());


        protected virtual void ThenTheCallHasTheValue(Validation validation)
            => Executor.Execute(() => WebDriver.AlertText.Validate(validation));
    }
}
