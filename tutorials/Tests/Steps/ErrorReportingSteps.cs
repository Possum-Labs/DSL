//using PossumLabs.DSL.Core;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using TechTalk.SpecFlow;

//namespace DSL.Documentation.Example
//{
//    [Binding]
//    public class ErrorReportingSteps : StepBase
//    {
//        public ErrorReportingSteps(ScenarioContext scenarioContext, FeatureContext featureContext) : base(scenarioContext, featureContext)
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
