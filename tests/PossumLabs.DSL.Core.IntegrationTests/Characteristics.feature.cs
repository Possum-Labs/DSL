﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by Reqnroll (https://www.reqnroll.net/).
//      Reqnroll Version:2.0.0.0
//      Reqnroll Generator Version:2.0.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
using Reqnroll;
namespace PossumLabs.DSL.Core.IntegrationTests
{
    
    
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Reqnroll", "2.0.0.0")]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute()]
    public partial class CharacteristicsFeature
    {
        
        private global::Reqnroll.ITestRunner testRunner;
        
        private Microsoft.VisualStudio.TestTools.UnitTesting.TestContext _testContext;
        
        private static string[] featureTags = ((string[])(null));
        
        private static global::Reqnroll.FeatureInfo featureInfo = new global::Reqnroll.FeatureInfo(new global::System.Globalization.CultureInfo("en-US"), "", "Characteristics", null, global::Reqnroll.ProgrammingLanguage.CSharp, featureTags);
        
#line 1 "Characteristics.feature"
#line hidden
        
        public virtual Microsoft.VisualStudio.TestTools.UnitTesting.TestContext TestContext
        {
            get
            {
                return this._testContext;
            }
            set
            {
                this._testContext = value;
            }
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassInitializeAttribute()]
        public static async global::System.Threading.Tasks.Task FeatureSetupAsync(Microsoft.VisualStudio.TestTools.UnitTesting.TestContext testContext)
        {
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassCleanupAttribute(Microsoft.VisualStudio.TestTools.UnitTesting.ClassCleanupBehavior.EndOfClass)]
        public static async global::System.Threading.Tasks.Task FeatureTearDownAsync()
        {
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestInitializeAttribute()]
        public async global::System.Threading.Tasks.Task TestInitializeAsync()
        {
            testRunner = global::Reqnroll.TestRunnerManager.GetTestRunnerForAssembly(featureHint: featureInfo);
            try
            {
                if (((testRunner.FeatureContext != null) 
                            && (testRunner.FeatureContext.FeatureInfo.Equals(featureInfo) == false)))
                {
                    await testRunner.OnFeatureEndAsync();
                }
            }
            finally
            {
                if (((testRunner.FeatureContext != null) 
                            && testRunner.FeatureContext.BeforeFeatureHookFailed))
                {
                    throw new global::Reqnroll.ReqnrollException("Scenario skipped because of previous before feature hook error");
                }
                if ((testRunner.FeatureContext == null))
                {
                    await testRunner.OnFeatureStartAsync(featureInfo);
                }
            }
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCleanupAttribute()]
        public async global::System.Threading.Tasks.Task TestTearDownAsync()
        {
            if ((testRunner == null))
            {
                return;
            }
            try
            {
                await testRunner.OnScenarioEndAsync();
            }
            finally
            {
                global::Reqnroll.TestRunnerManager.ReleaseTestRunner(testRunner);
                testRunner = null;
            }
        }
        
        public void ScenarioInitialize(global::Reqnroll.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Microsoft.VisualStudio.TestTools.UnitTesting.TestContext>(_testContext);
        }
        
        public async global::System.Threading.Tasks.Task ScenarioStartAsync()
        {
            await testRunner.OnScenarioStartAsync();
        }
        
        public async global::System.Threading.Tasks.Task ScenarioCleanupAsync()
        {
            await testRunner.CollectScenarioErrorsAsync();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("no template")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Characteristics")]
        public async global::System.Threading.Tasks.Task NoTemplate()
        {
            string[] tagsOfScenario = ((string[])(null));
            global::System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new global::System.Collections.Specialized.OrderedDictionary();
            global::Reqnroll.ScenarioInfo scenarioInfo = new global::Reqnroll.ScenarioInfo("no template", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 3
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((global::Reqnroll.TagHelper.ContainsIgnoreTag(scenarioInfo.CombinedTags) || global::Reqnroll.TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                await this.ScenarioStartAsync();
                global::Reqnroll.Table table1 = new global::Reqnroll.Table(new string[] {
                            "var"});
                table1.AddRow(new string[] {
                            "TO"});
#line 4
 await testRunner.GivenAsync("the Test Object", ((string)(null)), table1, "Given ");
#line hidden
#line 7
 await testRunner.ThenAsync("\'TO.IsSpecial\' has the value \'False\'", ((string)(null)), ((global::Reqnroll.Table)(null)), "Then ");
#line hidden
#line 8
 await testRunner.AndAsync("\'TO.Created\' has the value \'True\'", ((string)(null)), ((global::Reqnroll.Table)(null)), "And ");
#line hidden
            }
            await this.ScenarioCleanupAsync();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("just template")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Characteristics")]
        public async global::System.Threading.Tasks.Task JustTemplate()
        {
            string[] tagsOfScenario = ((string[])(null));
            global::System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new global::System.Collections.Specialized.OrderedDictionary();
            global::Reqnroll.ScenarioInfo scenarioInfo = new global::Reqnroll.ScenarioInfo("just template", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 10
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((global::Reqnroll.TagHelper.ContainsIgnoreTag(scenarioInfo.CombinedTags) || global::Reqnroll.TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                await this.ScenarioStartAsync();
                global::Reqnroll.Table table2 = new global::Reqnroll.Table(new string[] {
                            "var"});
                table2.AddRow(new string[] {
                            "TO"});
#line 11
 await testRunner.GivenAsync("the Test Object that is \'special\'", ((string)(null)), table2, "Given ");
#line hidden
#line 14
 await testRunner.ThenAsync("\'TO.TemplateName\' has the value \'default\'", ((string)(null)), ((global::Reqnroll.Table)(null)), "Then ");
#line hidden
#line 15
 await testRunner.AndAsync("\'TO.Created\' has the value \'True\'", ((string)(null)), ((global::Reqnroll.Table)(null)), "And ");
#line hidden
#line 16
 await testRunner.AndAsync("\'TO.IsSpecial\' has the value \'True\'", ((string)(null)), ((global::Reqnroll.Table)(null)), "And ");
#line hidden
            }
            await this.ScenarioCleanupAsync();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("pecific template")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Characteristics")]
        public async global::System.Threading.Tasks.Task PecificTemplate()
        {
            string[] tagsOfScenario = ((string[])(null));
            global::System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new global::System.Collections.Specialized.OrderedDictionary();
            global::Reqnroll.ScenarioInfo scenarioInfo = new global::Reqnroll.ScenarioInfo("pecific template", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 18
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((global::Reqnroll.TagHelper.ContainsIgnoreTag(scenarioInfo.CombinedTags) || global::Reqnroll.TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                await this.ScenarioStartAsync();
                global::Reqnroll.Table table3 = new global::Reqnroll.Table(new string[] {
                            "var"});
                table3.AddRow(new string[] {
                            "TO"});
#line 19
 await testRunner.GivenAsync("the Test Object of type \'templateA\' that is \'special\'", ((string)(null)), table3, "Given ");
#line hidden
#line 22
 await testRunner.ThenAsync("\'TO.TemplateName\' has the value \'A\'", ((string)(null)), ((global::Reqnroll.Table)(null)), "Then ");
#line hidden
#line 23
 await testRunner.AndAsync("\'TO.Created\' has the value \'True\'", ((string)(null)), ((global::Reqnroll.Table)(null)), "And ");
#line hidden
#line 24
 await testRunner.AndAsync("\'TO.IsSpecial\' has the value \'True\'", ((string)(null)), ((global::Reqnroll.Table)(null)), "And ");
#line hidden
            }
            await this.ScenarioCleanupAsync();
        }
    }
}
#pragma warning restore
#endregion
