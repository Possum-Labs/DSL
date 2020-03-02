using BoDi;
using Microsoft.Extensions.Configuration;
using PossumLabs.DSL.Core;
using PossumLabs.DSL.Core.Configuration;
using PossumLabs.DSL.Core.Exceptions;
using PossumLabs.DSL.Core.Files;
using PossumLabs.DSL.Core.Logging;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using TechTalk.SpecFlow;
using System.Linq;

namespace DSL.Documentation.Example
{
    public abstract class FrameworkInitializationStepsBase : StepsBase
    {
        public FrameworkInitializationStepsBase(IObjectContainer objectContainer) : base(objectContainer) { }

        private DefaultLogger Logger { get; set; }

        
        protected virtual void SetupInfrastructure()
        {
            IConfiguration config = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .AddEnvironmentVariables()
              .Build();

            var configFactory = new ConfigurationFactory(config);

            Logger = new DefaultLogger(new DirectoryInfo(Environment.CurrentDirectory), new YamlLogFormatter());
            Register((PossumLabs.DSL.Core.Logging.ILog)Logger);

            var templateManager = new PossumLabs.DSL.Core.Variables.TemplateManager();
            templateManager.Initialize(Assembly.GetExecutingAssembly());
            Register(templateManager);

            Log.Message($"Feature: {FeatureContext.FeatureInfo.Title} Scenario: {ScenarioContext.ScenarioInfo.Title} \n" +
                $"Tags: {FeatureContext.FeatureInfo.Tags.LogFormat()} {ScenarioContext.ScenarioInfo.Tags.LogFormat()}");

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        protected void LoadExistingData()
        {
            new PossumLabs.DSL.Core.Variables.ExistingDataManager(this.Interpeter, this.TemplateManager)
                .Initialize(this.GetType().Assembly);
        }
    }
}
