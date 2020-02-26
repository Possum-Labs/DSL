﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:2.4.0.0
//      SpecFlow Generator Version:2.4.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace DSL.Documentation.Example
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.4.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute()]
    public partial class UnderSelectorFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private Microsoft.VisualStudio.TestTools.UnitTesting.TestContext _testContext;
        
#line 1 "UnderSelector.feature"
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
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner(null, 0);
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "UnderSelector", null, ProgrammingLanguage.CSharp, new string[] {
                        "SingleBrowser",
                        "injected-html"});
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
                        && (testRunner.FeatureContext.FeatureInfo.Title != "UnderSelector")))
            {
                global::LegacyTest.Features.UnderSelectorFeature.FeatureSetup(null);
            }
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCleanupAttribute()]
        public virtual void ScenarioTearDown()
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
        
        public virtual void Div(string description, string under, string target, string value, string html, string[] exampleTags)
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Div", null, exampleTags);
#line 4
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "Html"});
            table1.AddRow(new string[] {
                        string.Format("{0}", html)});
#line 5
 testRunner.Given("injecting browser content", ((string)(null)), table1, "Given ");
#line 8
 testRunner.When(string.Format("under \'{0}\' entering \'{1}\' into element \'{2}\'", under, value, target), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 9
 testRunner.Then(string.Format("under \'{0}\' the element \'{1}\' has the value \'{2}\'", under, target, value), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Div: 01 simple")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "UnderSelector")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("SingleBrowser")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("injected-html")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "01 simple")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:description", "01 simple")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:under", "under-target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:target", "input-target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:value", "Bob")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:html", "<div>under-target<label>input-target<input type=\"text\"></input></label></div>")]
        public virtual void Div_01Simple()
        {
#line 4
this.Div("01 simple", "under-target", "input-target", "Bob", "<div>under-target<label>input-target<input type=\"text\"></input></label></div>", ((string[])(null)));
#line hidden
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Div: 02 label")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "UnderSelector")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("SingleBrowser")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("injected-html")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "02 label")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:description", "02 label")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:under", "under-target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:target", "input-target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:value", "Bob")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:html", "<div><label>under-target</label><label>input-target<input type=\"text\"></input></l" +
            "abel></div>")]
        public virtual void Div_02Label()
        {
#line 4
this.Div("02 label", "under-target", "input-target", "Bob", "<div><label>under-target</label><label>input-target<input type=\"text\"></input></l" +
                    "abel></div>", ((string[])(null)));
#line hidden
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Div: 03 b")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "UnderSelector")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("SingleBrowser")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("injected-html")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "03 b")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:description", "03 b")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:under", "under-target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:target", "input-target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:value", "Bob")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:html", "<div><b>under-target</b><label>input-target<input type=\"text\"></input></label></d" +
            "iv>")]
        public virtual void Div_03B()
        {
#line 4
this.Div("03 b", "under-target", "input-target", "Bob", "<div><b>under-target</b><label>input-target<input type=\"text\"></input></label></d" +
                    "iv>", ((string[])(null)));
#line hidden
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Div: 04 h1")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "UnderSelector")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("SingleBrowser")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("injected-html")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "04 h1")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:description", "04 h1")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:under", "under-target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:target", "input-target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:value", "Bob")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:html", "<div><h1>under-target</h1><label>input-target<input type=\"text\"></input></label><" +
            "/div>")]
        public virtual void Div_04H1()
        {
#line 4
this.Div("04 h1", "under-target", "input-target", "Bob", "<div><h1>under-target</h1><label>input-target<input type=\"text\"></input></label><" +
                    "/div>", ((string[])(null)));
#line hidden
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Div: 05 h2")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "UnderSelector")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("SingleBrowser")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("injected-html")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "05 h2")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:description", "05 h2")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:under", "under-target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:target", "input-target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:value", "Bob")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:html", "<div><h2>under-target</h2><label>input-target<input type=\"text\"></input></label><" +
            "/div>")]
        public virtual void Div_05H2()
        {
#line 4
this.Div("05 h2", "under-target", "input-target", "Bob", "<div><h2>under-target</h2><label>input-target<input type=\"text\"></input></label><" +
                    "/div>", ((string[])(null)));
#line hidden
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Div: 06 h3")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "UnderSelector")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("SingleBrowser")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("injected-html")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "06 h3")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:description", "06 h3")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:under", "under-target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:target", "input-target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:value", "Bob")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:html", "<div><h3>under-target</h3><label>input-target<input type=\"text\"></input></label><" +
            "/div>")]
        public virtual void Div_06H3()
        {
#line 4
this.Div("06 h3", "under-target", "input-target", "Bob", "<div><h3>under-target</h3><label>input-target<input type=\"text\"></input></label><" +
                    "/div>", ((string[])(null)));
#line hidden
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Div: 07 h4")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "UnderSelector")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("SingleBrowser")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("injected-html")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "07 h4")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:description", "07 h4")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:under", "under-target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:target", "input-target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:value", "Bob")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:html", "<div><h4>under-target</h4><label>input-target<input type=\"text\"></input></label><" +
            "/div>")]
        public virtual void Div_07H4()
        {
#line 4
this.Div("07 h4", "under-target", "input-target", "Bob", "<div><h4>under-target</h4><label>input-target<input type=\"text\"></input></label><" +
                    "/div>", ((string[])(null)));
#line hidden
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Div: 08 h5")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "UnderSelector")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("SingleBrowser")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("injected-html")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "08 h5")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:description", "08 h5")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:under", "under-target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:target", "input-target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:value", "Bob")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:html", "<div><h5>under-target</h5><label>input-target<input type=\"text\"></input></label><" +
            "/div>")]
        public virtual void Div_08H5()
        {
#line 4
this.Div("08 h5", "under-target", "input-target", "Bob", "<div><h5>under-target</h5><label>input-target<input type=\"text\"></input></label><" +
                    "/div>", ((string[])(null)));
#line hidden
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Div: 09 h6")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "UnderSelector")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("SingleBrowser")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("injected-html")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "09 h6")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:description", "09 h6")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:under", "under-target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:target", "input-target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:value", "Bob")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:html", "<div><h6>under-target</h6><label>input-target<input type=\"text\"></input></label><" +
            "/div>")]
        public virtual void Div_09H6()
        {
#line 4
this.Div("09 h6", "under-target", "input-target", "Bob", "<div><h6>under-target</h6><label>input-target<input type=\"text\"></input></label><" +
                    "/div>", ((string[])(null)));
#line hidden
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Div: 10 input")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "UnderSelector")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("SingleBrowser")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("injected-html")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "10 input")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:description", "10 input")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:under", "under-target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:target", "input-target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:value", "Bob")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:html", "<div><input value=\"under-target\"></input><label>input-target<input type=\"text\"></" +
            "input></label></div>")]
        public virtual void Div_10Input()
        {
#line 4
this.Div("10 input", "under-target", "input-target", "Bob", "<div><input value=\"under-target\"></input><label>input-target<input type=\"text\"></" +
                    "input></label></div>", ((string[])(null)));
#line hidden
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Div: 11 span")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "UnderSelector")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("SingleBrowser")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("injected-html")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "11 span")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:description", "11 span")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:under", "under-target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:target", "input-target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:value", "Bob")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:html", "<div><span>under-target</span><label>input-target<input type=\"text\"></input></lab" +
            "el></div>")]
        public virtual void Div_11Span()
        {
#line 4
this.Div("11 span", "under-target", "input-target", "Bob", "<div><span>under-target</span><label>input-target<input type=\"text\"></input></lab" +
                    "el></div>", ((string[])(null)));
#line hidden
        }
        
        public virtual void DivWithRowRole(string description, string under, string target, string value, string html, string[] exampleTags)
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Div with row role", null, exampleTags);
#line 26
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "Html"});
            table2.AddRow(new string[] {
                        string.Format("{0}", html)});
#line 27
 testRunner.Given("injecting browser content", ((string)(null)), table2, "Given ");
#line 30
 testRunner.When(string.Format("under \'{0}\' entering \'{1}\' into element \'{2}\'", under, value, target), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 31
 testRunner.Then(string.Format("under \'{0}\' the element \'{1}\' has the value \'{2}\'", under, target, value), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Div with row role: 01 simple")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "UnderSelector")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("SingleBrowser")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("injected-html")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "01 simple")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:description", "01 simple")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:under", "under-target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:target", "input-target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:value", "Bob")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:html", "<div role=\"row\"><div>under-target<label>input-target<input type=\"text\"></input></" +
            "label></div></div>")]
        public virtual void DivWithRowRole_01Simple()
        {
#line 26
this.DivWithRowRole("01 simple", "under-target", "input-target", "Bob", "<div role=\"row\"><div>under-target<label>input-target<input type=\"text\"></input></" +
                    "label></div></div>", ((string[])(null)));
#line hidden
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Div with row role: 02 label")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "UnderSelector")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("SingleBrowser")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("injected-html")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "02 label")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:description", "02 label")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:under", "under-target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:target", "input-target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:value", "Bob")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:html", "<div role=\"row\"><div><label>under-target</label><label>input-target<input type=\"t" +
            "ext\"></input></label></div></div>")]
        public virtual void DivWithRowRole_02Label()
        {
#line 26
this.DivWithRowRole("02 label", "under-target", "input-target", "Bob", "<div role=\"row\"><div><label>under-target</label><label>input-target<input type=\"t" +
                    "ext\"></input></label></div></div>", ((string[])(null)));
#line hidden
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Div with row role: 03 b")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "UnderSelector")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("SingleBrowser")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("injected-html")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "03 b")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:description", "03 b")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:under", "under-target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:target", "input-target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:value", "Bob")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:html", "<div role=\"row\"><div><b>under-target</b><label>input-target<input type=\"text\"></i" +
            "nput></label></div></div>")]
        public virtual void DivWithRowRole_03B()
        {
#line 26
this.DivWithRowRole("03 b", "under-target", "input-target", "Bob", "<div role=\"row\"><div><b>under-target</b><label>input-target<input type=\"text\"></i" +
                    "nput></label></div></div>", ((string[])(null)));
#line hidden
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Div with row role: 04 h1")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "UnderSelector")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("SingleBrowser")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("injected-html")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "04 h1")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:description", "04 h1")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:under", "under-target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:target", "input-target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:value", "Bob")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:html", "<div role=\"row\"><div><h1>under-target</h1><label>input-target<input type=\"text\"><" +
            "/input></label></div></div>")]
        public virtual void DivWithRowRole_04H1()
        {
#line 26
this.DivWithRowRole("04 h1", "under-target", "input-target", "Bob", "<div role=\"row\"><div><h1>under-target</h1><label>input-target<input type=\"text\"><" +
                    "/input></label></div></div>", ((string[])(null)));
#line hidden
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Div with row role: 05 h2")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "UnderSelector")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("SingleBrowser")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("injected-html")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "05 h2")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:description", "05 h2")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:under", "under-target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:target", "input-target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:value", "Bob")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:html", "<div role=\"row\"><div><h2>under-target</h2><label>input-target<input type=\"text\"><" +
            "/input></label></div></div>")]
        public virtual void DivWithRowRole_05H2()
        {
#line 26
this.DivWithRowRole("05 h2", "under-target", "input-target", "Bob", "<div role=\"row\"><div><h2>under-target</h2><label>input-target<input type=\"text\"><" +
                    "/input></label></div></div>", ((string[])(null)));
#line hidden
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Div with row role: 06 h3")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "UnderSelector")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("SingleBrowser")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("injected-html")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "06 h3")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:description", "06 h3")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:under", "under-target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:target", "input-target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:value", "Bob")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:html", "<div role=\"row\"><div><h3>under-target</h3><label>input-target<input type=\"text\"><" +
            "/input></label></div></div>")]
        public virtual void DivWithRowRole_06H3()
        {
#line 26
this.DivWithRowRole("06 h3", "under-target", "input-target", "Bob", "<div role=\"row\"><div><h3>under-target</h3><label>input-target<input type=\"text\"><" +
                    "/input></label></div></div>", ((string[])(null)));
#line hidden
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Div with row role: 07 h4")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "UnderSelector")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("SingleBrowser")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("injected-html")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "07 h4")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:description", "07 h4")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:under", "under-target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:target", "input-target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:value", "Bob")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:html", "<div role=\"row\"><div><h4>under-target</h4><label>input-target<input type=\"text\"><" +
            "/input></label></div></div>")]
        public virtual void DivWithRowRole_07H4()
        {
#line 26
this.DivWithRowRole("07 h4", "under-target", "input-target", "Bob", "<div role=\"row\"><div><h4>under-target</h4><label>input-target<input type=\"text\"><" +
                    "/input></label></div></div>", ((string[])(null)));
#line hidden
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Div with row role: 08 h5")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "UnderSelector")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("SingleBrowser")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("injected-html")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "08 h5")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:description", "08 h5")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:under", "under-target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:target", "input-target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:value", "Bob")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:html", "<div role=\"row\"><div><h5>under-target</h5><label>input-target<input type=\"text\"><" +
            "/input></label></div></div>")]
        public virtual void DivWithRowRole_08H5()
        {
#line 26
this.DivWithRowRole("08 h5", "under-target", "input-target", "Bob", "<div role=\"row\"><div><h5>under-target</h5><label>input-target<input type=\"text\"><" +
                    "/input></label></div></div>", ((string[])(null)));
#line hidden
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Div with row role: 09 h6")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "UnderSelector")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("SingleBrowser")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("injected-html")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "09 h6")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:description", "09 h6")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:under", "under-target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:target", "input-target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:value", "Bob")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:html", "<div role=\"row\"><div><h6>under-target</h6><label>input-target<input type=\"text\"><" +
            "/input></label></div></div>")]
        public virtual void DivWithRowRole_09H6()
        {
#line 26
this.DivWithRowRole("09 h6", "under-target", "input-target", "Bob", "<div role=\"row\"><div><h6>under-target</h6><label>input-target<input type=\"text\"><" +
                    "/input></label></div></div>", ((string[])(null)));
#line hidden
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Div with row role: 10 input")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "UnderSelector")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("SingleBrowser")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("injected-html")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "10 input")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:description", "10 input")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:under", "under-target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:target", "input-target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:value", "Bob")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:html", "<div role=\"row\"><div><input value=\"under-target\"></input><label>input-target<inpu" +
            "t type=\"text\"></input></label></div></div>")]
        public virtual void DivWithRowRole_10Input()
        {
#line 26
this.DivWithRowRole("10 input", "under-target", "input-target", "Bob", "<div role=\"row\"><div><input value=\"under-target\"></input><label>input-target<inpu" +
                    "t type=\"text\"></input></label></div></div>", ((string[])(null)));
#line hidden
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Div with row role: 11 span")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "UnderSelector")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("SingleBrowser")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("injected-html")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "11 span")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:description", "11 span")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:under", "under-target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:target", "input-target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:value", "Bob")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:html", "<div role=\"row\"><div><span>under-target</span><label>input-target<input type=\"tex" +
            "t\"></input></label></div></div>")]
        public virtual void DivWithRowRole_11Span()
        {
#line 26
this.DivWithRowRole("11 span", "under-target", "input-target", "Bob", "<div role=\"row\"><div><span>under-target</span><label>input-target<input type=\"tex" +
                    "t\"></input></label></div></div>", ((string[])(null)));
#line hidden
        }
    }
}
#pragma warning restore
#endregion
