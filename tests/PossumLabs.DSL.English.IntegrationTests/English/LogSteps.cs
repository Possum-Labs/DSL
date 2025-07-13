using Reqnroll.BoDi;
using PossumLabs.DSL.Core.Validations;
using Reqnroll;

namespace PossumLabs.DSL.English
{
    [Binding]
    public class LogSteps: LogStepsBase
    {
        public LogSteps(IObjectContainer objectContainer) : base(objectContainer)
        { }


        [Then(@"the Browser Logs has the value '(.*)'")]
        public  void ThenTheBrowserLogsHasTheValueEnglish(Validation validation)
            => base.ThenTheBrowserLogsHasTheValue(validation);

    }
}
