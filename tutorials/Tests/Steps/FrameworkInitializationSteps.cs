using BoDi;
using LegacyTest.Framework;
using LegacyTest.ValueObjects;
using Microsoft.Extensions.Configuration;
using PossumLabs.DSL.Core;
using PossumLabs.DSL.Core.Files;
using PossumLabs.DSL.Core.Logging;
using PossumLabs.DSL.Web.Diagnostic;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace DSL.Documentation.Example
{
    [Binding]
    public class FrameworkInitializationSteps : StepBase
    {
        public FrameworkInitializationSteps(IObjectContainer objectContainer) : base(objectContainer) { }

        private ScreenshotProcessor ScreenshotProcessor { get; set; }
        private ImageLogging ImageLogging { get; set; }
        private MovieLogger MovieLogger { get; set; }

        [StepArgumentTransformation]
        public object Transform(string id) => Interpeter.Get<object>(id);

        [BeforeScenario(Order = int.MinValue + 1)]
        public void Setup()
        {
            ScreenshotProcessor = ObjectContainer.Resolve<ScreenshotProcessor>();
        }

        [BeforeScenario(Order = int.MinValue)]
        public void SetupInfrastructure()
        {
   
            var metadata = new ScenarioMetadata
            {
                FeatureName = FeatureContext.FeatureInfo.Title,
                ScenarioName = ScenarioContext.ScenarioInfo.Title
            };
            Register(metadata);

            var factory = new PossumLabs.DSL.Core.Variables.ObjectFactory();
            Register(factory);
            Register(new PossumLabs.DSL.Core.Variables.Interpeter(factory));
            var logger = new DefaultLogger(new DirectoryInfo(Environment.CurrentDirectory));
            Register((PossumLabs.DSL.Core.Logging.ILog)logger);
            Register(new PossumLabs.DSL.Core.Exceptions.ActionExecutor(logger));

            Register(new FileManager(new DatetimeManager() { Now = () => DateTime.Now }));
            FileManager.Initialize(FeatureContext.FeatureInfo.Title, ScenarioContext.ScenarioInfo.Title, null /*Specflow limitation*/);
            var templateManager = new PossumLabs.DSL.Core.Variables.TemplateManager();
            templateManager.Initialize(Assembly.GetExecutingAssembly());
            Register(templateManager);

            Log.Message($"feature: {FeatureContext.FeatureInfo.Title} scenario: {ScenarioContext.ScenarioInfo.Title} \n" +
                $"Tags: {FeatureContext.FeatureInfo.Tags.LogFormat()} {ScenarioContext.ScenarioInfo.Tags.LogFormat()}");
        }
    }
}