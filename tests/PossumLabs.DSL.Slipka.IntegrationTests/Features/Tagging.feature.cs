// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:3.1.0.0
//      SpecFlow Generator Version:3.1.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace PossumLabs.DSL.Slipka.IntegrationTests.Features
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.1.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute()]
    public partial class TaggingCallsFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private Microsoft.VisualStudio.TestTools.UnitTesting.TestContext _testContext;
        
        private string[] _featureTags = new string[] {
                "Slipka"};
        
#line 1 "Tagging.feature"
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
        public static void FeatureSetup(Microsoft.VisualStudio.TestTools.UnitTesting.TestContext testContext)
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en"), "Tagging Calls", null, ProgrammingLanguage.CSharp, new string[] {
                        "Slipka"});
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassCleanupAttribute()]
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestInitializeAttribute()]
        public virtual void TestInitialize()
        {
            if (((testRunner.FeatureContext != null) 
                        && (testRunner.FeatureContext.FeatureInfo.Title != "Tagging Calls")))
            {
                global::PossumLabs.DSL.Slipka.IntegrationTests.Features.TaggingCallsFeature.FeatureSetup(null);
            }
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCleanupAttribute()]
        public virtual void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Microsoft.VisualStudio.TestTools.UnitTesting.TestContext>(_testContext);
        }
        
        public virtual void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void FeatureBackground()
        {
#line 4
#line hidden
            TechTalk.SpecFlow.Table table103 = new TechTalk.SpecFlow.Table(new string[] {
                        "var",
                        "Destination"});
            table103.AddRow(new string[] {
                        "P1",
                        "http://PossumLabs.com"});
#line 5
 testRunner.Given("the Slipka Proxy", ((string)(null)), table103, "Given ");
#line hidden
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Tagging happy path")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Tagging Calls")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("Slipka")]
        public virtual void TaggingHappyPath()
        {
            string[] tagsOfScenario = ((string[])(null));
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Tagging happy path", null, ((string[])(null)));
#line 9
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 4
this.FeatureBackground();
#line hidden
                TechTalk.SpecFlow.Table table104 = new TechTalk.SpecFlow.Table(new string[] {
                            "Uri",
                            "Response Content",
                            "StatusCode",
                            "Method"});
                table104.AddRow(new string[] {
                            "test",
                            "Hello World",
                            "200",
                            "GET"});
                table104.AddRow(new string[] {
                            "other",
                            "Hello World",
                            "200",
                            "GET"});
#line 10
 testRunner.Given("the Proxy \'P1\' injects the calls", ((string)(null)), table104, "Given ");
#line hidden
                TechTalk.SpecFlow.Table table105 = new TechTalk.SpecFlow.Table(new string[] {
                            "Uri",
                            "Tags"});
                table105.AddRow(new string[] {
                            "test",
                            "[\'Test\']"});
#line 14
 testRunner.Given("the Proxy \'P1\' tags the calls", ((string)(null)), table105, "Given ");
#line hidden
                TechTalk.SpecFlow.Table table106 = new TechTalk.SpecFlow.Table(new string[] {
                            "var",
                            "Host",
                            "Path",
                            "Method"});
                table106.AddRow(new string[] {
                            "C1",
                            "P1.ProxyUri",
                            "test",
                            "GET"});
                table106.AddRow(new string[] {
                            "C2",
                            "P1.ProxyUri",
                            "other",
                            "GET"});
#line 17
 testRunner.And("the Call", ((string)(null)), table106, "And ");
#line hidden
#line 21
 testRunner.When("the Call \'C1\' is executed", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 22
 testRunner.And("the Call \'C2\' is executed", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 23
 testRunner.And("wait 1000 ms", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 24
 testRunner.Then("retrieving the tagged calls from Proxy \'P1\' with tag \'Test\' as \'RC\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
                TechTalk.SpecFlow.Table table107 = new TechTalk.SpecFlow.Table(new string[] {
                            "Count"});
                table107.AddRow(new string[] {
                            "1"});
#line 25
 testRunner.And("\'RC\' has the values", ((string)(null)), table107, "And ");
#line hidden
                TechTalk.SpecFlow.Table table108 = new TechTalk.SpecFlow.Table(new string[] {
                            "Path",
                            "StatusCode",
                            "Tags"});
                table108.AddRow(new string[] {
                            "/test",
                            "200",
                            "[\'Test\']"});
#line 28
 testRunner.And("\'RC[0]\' has the values", ((string)(null)), table108, "And ");
#line hidden
#line 31
 testRunner.And("retrieving the Session from Proxy \'P1\' as \'S1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
                TechTalk.SpecFlow.Table table109 = new TechTalk.SpecFlow.Table(new string[] {
                            "Tags"});
                table109.AddRow(new string[] {
                            "[\'Test\']"});
#line 32
 testRunner.And("\'S1\' has the values", ((string)(null)), table109, "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Tagging slow calls")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Tagging Calls")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("Slipka")]
        public virtual void TaggingSlowCalls()
        {
            string[] tagsOfScenario = ((string[])(null));
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Tagging slow calls", null, ((string[])(null)));
#line 37
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 4
this.FeatureBackground();
#line hidden
                TechTalk.SpecFlow.Table table110 = new TechTalk.SpecFlow.Table(new string[] {
                            "Uri",
                            "Response Content",
                            "StatusCode",
                            "Method",
                            "Duration"});
                table110.AddRow(new string[] {
                            "fast",
                            "Hello World",
                            "200",
                            "GET",
                            "1"});
                table110.AddRow(new string[] {
                            "slow",
                            "Hello World",
                            "200",
                            "GET",
                            "4000"});
#line 38
 testRunner.Given("the Proxy \'P1\' injects the calls", ((string)(null)), table110, "Given ");
#line hidden
                TechTalk.SpecFlow.Table table111 = new TechTalk.SpecFlow.Table(new string[] {
                            "var",
                            "Destination"});
                table111.AddRow(new string[] {
                            "P2",
                            "P1.ProxyUri"});
#line 42
 testRunner.Given("the Slipka Proxy", ((string)(null)), table111, "Given ");
#line hidden
                TechTalk.SpecFlow.Table table112 = new TechTalk.SpecFlow.Table(new string[] {
                            "Tags",
                            "Method",
                            "Duration"});
                table112.AddRow(new string[] {
                            "[\'Test\']",
                            "GET",
                            "3000"});
#line 45
 testRunner.Given("the Proxy \'P2\' tags the calls", ((string)(null)), table112, "Given ");
#line hidden
                TechTalk.SpecFlow.Table table113 = new TechTalk.SpecFlow.Table(new string[] {
                            "var",
                            "Host",
                            "Path",
                            "Method"});
                table113.AddRow(new string[] {
                            "C1",
                            "P2.ProxyUri",
                            "fast",
                            "GET"});
                table113.AddRow(new string[] {
                            "C2",
                            "P2.ProxyUri",
                            "slow",
                            "GET"});
#line 48
 testRunner.And("the Call", ((string)(null)), table113, "And ");
#line hidden
#line 52
 testRunner.When("the Call \'C1\' is executed", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 53
 testRunner.And("the Call \'C2\' is executed", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 54
 testRunner.And("wait 1000 ms", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 55
 testRunner.Then("retrieving the tagged calls from Proxy \'P2\' with tag \'Test\' as \'RC\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
                TechTalk.SpecFlow.Table table114 = new TechTalk.SpecFlow.Table(new string[] {
                            "Count"});
                table114.AddRow(new string[] {
                            "1"});
#line 56
 testRunner.And("\'RC\' has the values", ((string)(null)), table114, "And ");
#line hidden
                TechTalk.SpecFlow.Table table115 = new TechTalk.SpecFlow.Table(new string[] {
                            "Path",
                            "StatusCode",
                            "Tags"});
                table115.AddRow(new string[] {
                            "/slow",
                            "200",
                            "[\'Test\']"});
#line 59
 testRunner.And("\'RC[0]\' has the values", ((string)(null)), table115, "And ");
#line hidden
#line 62
 testRunner.And("retrieving the Session from Proxy \'P2\' as \'S2\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
                TechTalk.SpecFlow.Table table116 = new TechTalk.SpecFlow.Table(new string[] {
                            "Tags"});
                table116.AddRow(new string[] {
                            "[\'Test\']"});
#line 63
 testRunner.And("\'S2\' has the values", ((string)(null)), table116, "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
