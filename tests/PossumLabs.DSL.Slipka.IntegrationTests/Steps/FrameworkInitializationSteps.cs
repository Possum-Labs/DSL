using Reqnroll.BoDi;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PossumLabs.DSL.Core;
using PossumLabs.DSL.Core.Configuration;
using PossumLabs.DSL.Core.Exceptions;
using PossumLabs.DSL.Core.Files;
using PossumLabs.DSL.Core.Logging;
using PossumLabs.DSL.Core.Variables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Reqnroll;

namespace PossumLabs.DSL.Slipka.IntegrationTests
{
    [Binding]
    public class FrameworkInitializationSteps : StepBase
    {
        public FrameworkInitializationSteps(IObjectContainer objectContainer) : base(objectContainer) { }

        protected DefaultLogger Logger { get; set; }

        protected virtual void IoCInitialialization()
        {
            ObjectContainer.RegisterTypeAs<ConfigurationFactory, IConfigurationFactory>();
            ObjectContainer.RegisterTypeAs<Interpeter, IInterpeter>();
            ObjectContainer.RegisterTypeAs<ActionExecutor, PossumLabs.DSL.Core.Exceptions.IActionExecutor>();
            ObjectContainer.RegisterTypeAs<ObjectFactory, IObjectFactory>();
            ObjectContainer.RegisterTypeAs<TemplateManager, ITemplateManager>();
            ObjectContainer.RegisterTypeAs<ImageLogging, IImageLogging>();
            ObjectContainer.RegisterTypeAs<FileManager, IFileManager>();
            ObjectContainer.RegisterTypeAs<MovieLogger, IMovieLogger>();
            ObjectContainer.RegisterTypeAs<YamlLogFormatter, ILogFormatter>();
            ObjectContainer.RegisterTypeAs<DefaultLogger, ILog>();
            ObjectContainer.RegisterTypeAs<ExistingDataManager, IExistingDataManager>();
            ObjectContainer.RegisterTypeAs<RetryExecutor, IRetryExecutor>();
        }

        [BeforeScenario(Order = int.MinValue + 1)]
        protected virtual void SetupInfrastructure()
        {
            IConfiguration config = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .AddEnvironmentVariables()
              .Build();
            ObjectContainer.RegisterInstanceAs(config);
            ObjectContainer.RegisterInstanceAs(new DatetimeManager(() => DateTime.Now));
            ObjectContainer.RegisterInstanceAs(new DirectoryInfo(Environment.CurrentDirectory));
            ObjectContainer.RegisterInstanceAs(new ScenarioMetadata(() => ScenarioContext.TestError != null));

            IoCInitialialization();

            var configFactory = ObjectContainer.Resolve<IConfigurationFactory>();
            ObjectContainer.RegisterInstanceAs(configFactory.Create<MovieLoggerConfig>());
            ObjectContainer.RegisterInstanceAs(configFactory.Create<ImageLoggingConfig>());
            ObjectContainer.Resolve<IFileManager>()
                .Initialize(FeatureContext.FeatureInfo.Title,
                ScenarioContext.ScenarioInfo.Title,
                null /*Specflow limitation*/);

            Log.Message($"Feature: {FeatureContext.FeatureInfo.Title} Scenario: {ScenarioContext.ScenarioInfo.Title} \n" +
                $"Tags: {FeatureContext.FeatureInfo.Tags.LogFormat()} {ScenarioContext.ScenarioInfo.Tags.LogFormat()}");

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        [BeforeScenario(Order = int.MinValue + 12)]
        protected void LoadTemplates()
        {
            var templateManager = new PossumLabs.DSL.Core.Variables.TemplateManager();
            templateManager.Initialize(this.GetType().Assembly);
            Register(templateManager);
        }

        [BeforeScenario(Order = int.MinValue + 13)]
        protected void LoadExistingData()
        {
            new PossumLabs.DSL.Core.Variables.ExistingDataManager(this.Interpeter, this.TemplateManager)
                .Initialize(this.GetType().Assembly);

        }

    }
}
