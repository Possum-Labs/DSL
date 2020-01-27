using BoDi;
using PossumLabs.Specflow.Core;
using PossumLabs.Specflow.Core.Files;
using PossumLabs.Specflow.Core.Logging;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Variables
{
    [Binding]
    public class FrameworkInitializationSteps : StepBase
    {
        public FrameworkInitializationSteps(IObjectContainer objectContainer) : base(objectContainer) { }


        [StepArgumentTransformation]
        public object Transform(string id) => Interpeter.Get<object>(id);

        [StepArgumentTransformation]
        public ResolvedString TransformResolvedString(string id) => Interpeter.Get<string>(id);

        [BeforeScenario(Order = int.MinValue)]
        public void SetupInfrastructure()
        {
   
            var metadata = new ScenarioMetadata
            {
                FeatureName = FeatureContext.FeatureInfo.Title,
                ScenarioName = ScenarioContext.ScenarioInfo.Title
            };
            Register(metadata);

            var factory = new PossumLabs.Specflow.Core.Variables.ObjectFactory();
            Register(factory);
            Register(new PossumLabs.Specflow.Core.Variables.Interpeter(factory));
            var logger = new DefaultLogger(new DirectoryInfo(Environment.CurrentDirectory));
            Register((PossumLabs.Specflow.Core.Logging.ILog)logger);

            var templateManager = new PossumLabs.Specflow.Core.Variables.TemplateManager();
            templateManager.Initialize(Assembly.GetExecutingAssembly());
            Register(templateManager);
        }

        [BeforeScenario(Order = 1)]
        public void SetupExistingData()
        {
            new PossumLabs.Specflow.Core.Variables.ExistingDataManager(this.Interpeter).Initialize(Assembly.GetExecutingAssembly());
        }
    }
}