using BoDi;
using PossumLabs.DSL;
using PossumLabs.DSL.Core.Validations;
using TechTalk.SpecFlow;

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
