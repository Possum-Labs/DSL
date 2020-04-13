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
namespace PossumLabs.DSL.Web.IntegrationTests.Features
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.1.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute()]
    public partial class RichTextControlsFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private Microsoft.VisualStudio.TestTools.UnitTesting.TestContext _testContext;
        
        private string[] _featureTags = ((string[])(null));
        
#line 1 "RichTextControls.feature"
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
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en"), "Rich Text Controls", "all id\'s for the text area should be id=\"myeditor\"", ProgrammingLanguage.CSharp, ((string[])(null)));
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
                        && (testRunner.FeatureContext.FeatureInfo.Title != "Rich Text Controls")))
            {
                global::PossumLabs.DSL.Web.IntegrationTests.Features.RichTextControlsFeature.FeatureSetup(null);
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
        
        public virtual void EnteringTextInputsCKEditor4(string description, string target, string value, string result, string html, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("entering text inputs CKEditor 4", null, exampleTags);
#line 5
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
                TechTalk.SpecFlow.Table table35 = new TechTalk.SpecFlow.Table(new string[] {
                            "CKEditor4"});
                table35.AddRow(new string[] {
                            string.Format("{0}", html)});
#line 6
 testRunner.Given("injecting browser content", ((string)(null)), table35, "Given ");
#line hidden
#line 9
 testRunner.When(string.Format("entering \'{0}\' into element \'{1}\'", value, target), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 10
 testRunner.Then(string.Format("the element \'{0}\' has the value \'/{1}/\'", target, result), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("entering text inputs CKEditor 4: textarea for")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Rich Text Controls")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "textarea for")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:description", "textarea for")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:target", "target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:value", "Bob")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:result", "^<p>Bob<\\/p>")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:html", "<label for=\"myeditor\">target</label><textarea id=\"myeditor\"></textarea>")]
        public virtual void EnteringTextInputsCKEditor4_TextareaFor()
        {
#line 5
this.EnteringTextInputsCKEditor4("textarea for", "target", "Bob", "^<p>Bob<\\/p>", "<label for=\"myeditor\">target</label><textarea id=\"myeditor\"></textarea>", ((string[])(null)));
#line hidden
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("entering text inputs CKEditor 4: textarea following")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Rich Text Controls")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "textarea following")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:description", "textarea following")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:target", "target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:value", "Bob")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:result", "^<p>Bob<\\/p>")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:html", "<label>target</label><textarea id=\"myeditor\"></textarea>")]
        public virtual void EnteringTextInputsCKEditor4_TextareaFollowing()
        {
#line 5
this.EnteringTextInputsCKEditor4("textarea following", "target", "Bob", "^<p>Bob<\\/p>", "<label>target</label><textarea id=\"myeditor\"></textarea>", ((string[])(null)));
#line hidden
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("entering text inputs CKEditor 4: textarea nested")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Rich Text Controls")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "textarea nested")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:description", "textarea nested")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:target", "target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:value", "Bob")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:result", "^<p>Bob<\\/p>")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:html", "<label>target<textarea id=\"myeditor\"></textarea></label>")]
        public virtual void EnteringTextInputsCKEditor4_TextareaNested()
        {
#line 5
this.EnteringTextInputsCKEditor4("textarea nested", "target", "Bob", "^<p>Bob<\\/p>", "<label>target<textarea id=\"myeditor\"></textarea></label>", ((string[])(null)));
#line hidden
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("entering text inputs CKEditor 4: textarea aria-label")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Rich Text Controls")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "textarea aria-label")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:description", "textarea aria-label")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:target", "target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:value", "Bob")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:result", "^<p>Bob<\\/p>")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:html", "<textarea id=\"myeditor\" aria-label=\"target\"></textarea>")]
        public virtual void EnteringTextInputsCKEditor4_TextareaAria_Label()
        {
#line 5
this.EnteringTextInputsCKEditor4("textarea aria-label", "target", "Bob", "^<p>Bob<\\/p>", "<textarea id=\"myeditor\" aria-label=\"target\"></textarea>", ((string[])(null)));
#line hidden
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("entering text inputs CKEditor 4: textarea aria-labelledby")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Rich Text Controls")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "textarea aria-labelledby")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:description", "textarea aria-labelledby")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:target", "t1 t2")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:value", "Bob")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:result", "^<p>Bob<\\/p>")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:html", "<textarea id=\"myeditor\" aria-labelledby= \"l1 l2\"></textarea><label id=\"l1\">t1</la" +
            "bel><label id=\"l2\">t2</label>")]
        public virtual void EnteringTextInputsCKEditor4_TextareaAria_Labelledby()
        {
#line 5
this.EnteringTextInputsCKEditor4("textarea aria-labelledby", "t1 t2", "Bob", "^<p>Bob<\\/p>", "<textarea id=\"myeditor\" aria-labelledby= \"l1 l2\"></textarea><label id=\"l1\">t1</la" +
                    "bel><label id=\"l2\">t2</label>", ((string[])(null)));
#line hidden
        }
        
        public virtual void EnteringTextInputsCKEditor5(string description, string target, string value, string result, string html, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "ignore"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            string[] tagsOfScenario = @__tags;
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("entering text inputs CKEditor 5", "waiting on a framework change", @__tags);
#line 20
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
                TechTalk.SpecFlow.Table table36 = new TechTalk.SpecFlow.Table(new string[] {
                            "CKEditor5"});
                table36.AddRow(new string[] {
                            string.Format("{0}", html)});
#line 22
 testRunner.Given("injecting browser content", ((string)(null)), table36, "Given ");
#line hidden
#line 25
 testRunner.When(string.Format("entering \'{0}\' into element \'{1}\'", value, target), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 26
 testRunner.Then(string.Format("the element \'{0}\' has the value \'{1}\'", target, result), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("entering text inputs CKEditor 5: textarea for")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Rich Text Controls")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("ignore")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "textarea for")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:description", "textarea for")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:target", "target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:value", "Bob")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:result", "Bob")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:html", "<label for=\"myeditor\">target</label><textarea id=\"myeditor\"></textarea>")]
        public virtual void EnteringTextInputsCKEditor5_TextareaFor()
        {
#line 20
this.EnteringTextInputsCKEditor5("textarea for", "target", "Bob", "Bob", "<label for=\"myeditor\">target</label><textarea id=\"myeditor\"></textarea>", ((string[])(null)));
#line hidden
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("entering text inputs CKEditor 5: textarea following")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Rich Text Controls")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("ignore")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "textarea following")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:description", "textarea following")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:target", "target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:value", "Bob")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:result", "Bob")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:html", "<label>target</label><textarea id=\"myeditor\"></textarea>")]
        public virtual void EnteringTextInputsCKEditor5_TextareaFollowing()
        {
#line 20
this.EnteringTextInputsCKEditor5("textarea following", "target", "Bob", "Bob", "<label>target</label><textarea id=\"myeditor\"></textarea>", ((string[])(null)));
#line hidden
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("entering text inputs CKEditor 5: textarea nested")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Rich Text Controls")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("ignore")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "textarea nested")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:description", "textarea nested")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:target", "target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:value", "Bob")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:result", "Bob")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:html", "<label>target<textarea id=\"myeditor\"></textarea></label>")]
        public virtual void EnteringTextInputsCKEditor5_TextareaNested()
        {
#line 20
this.EnteringTextInputsCKEditor5("textarea nested", "target", "Bob", "Bob", "<label>target<textarea id=\"myeditor\"></textarea></label>", ((string[])(null)));
#line hidden
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("entering text inputs CKEditor 5: textarea aria-label")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Rich Text Controls")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("ignore")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "textarea aria-label")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:description", "textarea aria-label")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:target", "target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:value", "Bob")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:result", "Bob")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:html", "<textarea id=\"myeditor\" aria-label=\"target\"></textarea>")]
        public virtual void EnteringTextInputsCKEditor5_TextareaAria_Label()
        {
#line 20
this.EnteringTextInputsCKEditor5("textarea aria-label", "target", "Bob", "Bob", "<textarea id=\"myeditor\" aria-label=\"target\"></textarea>", ((string[])(null)));
#line hidden
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("entering text inputs CKEditor 5: textarea aria-labelledby")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Rich Text Controls")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("ignore")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "textarea aria-labelledby")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:description", "textarea aria-labelledby")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:target", "t1 t2")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:value", "Bob")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:result", "Bob")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:html", "<textarea id=\"myeditor\" aria-labelledby= \"l1 l2\"></textarea><label id=\"l1\">t1</la" +
            "bel><label id=\"l2\">t2</label>")]
        public virtual void EnteringTextInputsCKEditor5_TextareaAria_Labelledby()
        {
#line 20
this.EnteringTextInputsCKEditor5("textarea aria-labelledby", "t1 t2", "Bob", "Bob", "<textarea id=\"myeditor\" aria-labelledby= \"l1 l2\"></textarea><label id=\"l1\">t1</la" +
                    "bel><label id=\"l2\">t2</label>", ((string[])(null)));
#line hidden
        }
        
        public virtual void EnteringTextInputsTinyMCE4_5_5(string description, string target, string value, string result, string html, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("entering text inputs Tiny MCE 4.5.5", null, exampleTags);
#line 35
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
                TechTalk.SpecFlow.Table table37 = new TechTalk.SpecFlow.Table(new string[] {
                            "TinyMCE4"});
                table37.AddRow(new string[] {
                            string.Format("{0}", html)});
#line 36
 testRunner.Given("injecting browser content", ((string)(null)), table37, "Given ");
#line hidden
#line 39
 testRunner.When(string.Format("entering \'{0}\' into element \'{1}\'", value, target), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 40
 testRunner.Then(string.Format("the element \'{0}\' has the value \'/{1}/\'", target, result), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("entering text inputs Tiny MCE 4.5.5: textarea for")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Rich Text Controls")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "textarea for")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:description", "textarea for")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:target", "target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:value", "Bob")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:result", "^<p>Bob<\\/p>")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:html", "<label for=\"myeditor\">target</label><textarea id=\"myeditor\"></textarea>")]
        public virtual void EnteringTextInputsTinyMCE4_5_5_TextareaFor()
        {
#line 35
this.EnteringTextInputsTinyMCE4_5_5("textarea for", "target", "Bob", "^<p>Bob<\\/p>", "<label for=\"myeditor\">target</label><textarea id=\"myeditor\"></textarea>", ((string[])(null)));
#line hidden
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("entering text inputs Tiny MCE 4.5.5: textarea nested")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Rich Text Controls")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "textarea nested")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:description", "textarea nested")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:target", "target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:value", "Bob")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:result", "^<p>Bob<\\/p>")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:html", "<label>target<textarea id=\"myeditor\"></textarea></label>")]
        public virtual void EnteringTextInputsTinyMCE4_5_5_TextareaNested()
        {
#line 35
this.EnteringTextInputsTinyMCE4_5_5("textarea nested", "target", "Bob", "^<p>Bob<\\/p>", "<label>target<textarea id=\"myeditor\"></textarea></label>", ((string[])(null)));
#line hidden
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("entering text inputs Tiny MCE 4.5.5: textarea aria-label")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Rich Text Controls")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "textarea aria-label")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:description", "textarea aria-label")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:target", "target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:value", "Bob")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:result", "^<p>Bob<\\/p>")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:html", "<textarea id=\"myeditor\" aria-label=\"target\"></textarea>")]
        public virtual void EnteringTextInputsTinyMCE4_5_5_TextareaAria_Label()
        {
#line 35
this.EnteringTextInputsTinyMCE4_5_5("textarea aria-label", "target", "Bob", "^<p>Bob<\\/p>", "<textarea id=\"myeditor\" aria-label=\"target\"></textarea>", ((string[])(null)));
#line hidden
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("entering text inputs Tiny MCE 4.5.5: textarea aria-labelledby")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Rich Text Controls")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "textarea aria-labelledby")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:description", "textarea aria-labelledby")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:target", "t1 t2")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:value", "Bob")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:result", "^<p>Bob<\\/p>")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:html", "<textarea id=\"myeditor\" aria-labelledby= \"l1 l2\"></textarea><label id=\"l1\">t1</la" +
            "bel><label id=\"l2\">t2</label>")]
        public virtual void EnteringTextInputsTinyMCE4_5_5_TextareaAria_Labelledby()
        {
#line 35
this.EnteringTextInputsTinyMCE4_5_5("textarea aria-labelledby", "t1 t2", "Bob", "^<p>Bob<\\/p>", "<textarea id=\"myeditor\" aria-labelledby= \"l1 l2\"></textarea><label id=\"l1\">t1</la" +
                    "bel><label id=\"l2\">t2</label>", ((string[])(null)));
#line hidden
        }
        
        public virtual void EnteringTextInputsTinyMCE5(string description, string target, string value, string result, string html, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("entering text inputs Tiny MCE 5", null, exampleTags);
#line 49
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
                TechTalk.SpecFlow.Table table38 = new TechTalk.SpecFlow.Table(new string[] {
                            "TinyMCE5"});
                table38.AddRow(new string[] {
                            string.Format("{0}", html)});
#line 50
 testRunner.Given("injecting browser content", ((string)(null)), table38, "Given ");
#line hidden
#line 53
 testRunner.When(string.Format("entering \'{0}\' into element \'{1}\'", value, target), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 54
 testRunner.Then(string.Format("the element \'{0}\' has the value \'/{1}/\'", target, result), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("entering text inputs Tiny MCE 5: textarea for")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Rich Text Controls")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "textarea for")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:description", "textarea for")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:target", "target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:value", "Bob")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:result", "^<p>Bob<\\/p>")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:html", "<label for=\"myeditor\">target</label><textarea id=\"myeditor\"></textarea>")]
        public virtual void EnteringTextInputsTinyMCE5_TextareaFor()
        {
#line 49
this.EnteringTextInputsTinyMCE5("textarea for", "target", "Bob", "^<p>Bob<\\/p>", "<label for=\"myeditor\">target</label><textarea id=\"myeditor\"></textarea>", ((string[])(null)));
#line hidden
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("entering text inputs Tiny MCE 5: textarea following")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Rich Text Controls")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "textarea following")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:description", "textarea following")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:target", "target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:value", "Bob")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:result", "^<p>Bob<\\/p>")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:html", "<label>target</label><textarea id=\"myeditor\"></textarea>")]
        public virtual void EnteringTextInputsTinyMCE5_TextareaFollowing()
        {
#line 49
this.EnteringTextInputsTinyMCE5("textarea following", "target", "Bob", "^<p>Bob<\\/p>", "<label>target</label><textarea id=\"myeditor\"></textarea>", ((string[])(null)));
#line hidden
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("entering text inputs Tiny MCE 5: textarea nested")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Rich Text Controls")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "textarea nested")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:description", "textarea nested")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:target", "target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:value", "Bob")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:result", "^<p>Bob<\\/p>")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:html", "<label>target<textarea id=\"myeditor\"></textarea></label>")]
        public virtual void EnteringTextInputsTinyMCE5_TextareaNested()
        {
#line 49
this.EnteringTextInputsTinyMCE5("textarea nested", "target", "Bob", "^<p>Bob<\\/p>", "<label>target<textarea id=\"myeditor\"></textarea></label>", ((string[])(null)));
#line hidden
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("entering text inputs Tiny MCE 5: textarea aria-label")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Rich Text Controls")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "textarea aria-label")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:description", "textarea aria-label")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:target", "target")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:value", "Bob")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:result", "^<p>Bob<\\/p>")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:html", "<textarea id=\"myeditor\" aria-label=\"target\"></textarea>")]
        public virtual void EnteringTextInputsTinyMCE5_TextareaAria_Label()
        {
#line 49
this.EnteringTextInputsTinyMCE5("textarea aria-label", "target", "Bob", "^<p>Bob<\\/p>", "<textarea id=\"myeditor\" aria-label=\"target\"></textarea>", ((string[])(null)));
#line hidden
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("entering text inputs Tiny MCE 5: textarea aria-labelledby")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Rich Text Controls")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "textarea aria-labelledby")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:description", "textarea aria-labelledby")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:target", "t1 t2")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:value", "Bob")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:result", "^<p>Bob<\\/p>")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:html", "<textarea id=\"myeditor\" aria-labelledby= \"l1 l2\"></textarea><label id=\"l1\">t1</la" +
            "bel><label id=\"l2\">t2</label>")]
        public virtual void EnteringTextInputsTinyMCE5_TextareaAria_Labelledby()
        {
#line 49
this.EnteringTextInputsTinyMCE5("textarea aria-labelledby", "t1 t2", "Bob", "^<p>Bob<\\/p>", "<textarea id=\"myeditor\" aria-labelledby= \"l1 l2\"></textarea><label id=\"l1\">t1</la" +
                    "bel><label id=\"l2\">t2</label>", ((string[])(null)));
#line hidden
        }
    }
}
#pragma warning restore
#endregion