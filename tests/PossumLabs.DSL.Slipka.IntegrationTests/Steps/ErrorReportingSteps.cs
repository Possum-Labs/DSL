//using PossumLabs.DSL.Core;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using TechTalk.SpecFlow;

//namespace PossumLabs.DSL.Slipka.IntegrationTests
//{
//    [Binding]
//    public class ErrorReportingSteps : StepBase
//    {
//        public ErrorReportingSteps(IObjectContainer objectContainer) : base(objectContainer)
//        {
//        }

//        [AfterScenario]
//        void UpdateErrorMessages()
//        {
//            if (ScenarioContext.TestError is GherkinException)
//            {
//                ScenarioContex.ScenarioExecutionStatus == ScenarioExecutionStatus.BindingError;
//                ScenarioContext.stat
//            }
//    }
//}
