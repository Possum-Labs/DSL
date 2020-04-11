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
using PossumLabs.DSL.Core.Variables;

namespace DSL.Documentation.Example
{
    public abstract class FrameworkInitializationStepsBase : StepsBase
    {
        public FrameworkInitializationStepsBase(IObjectContainer objectContainer) : base(objectContainer) { }

        private DefaultLogger Logger { get; set; }


        protected virtual Characteristics Transform(string id) => id;

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


        protected void LoadTemplates()
        {
            ObjectContainer.Resolve<ITemplateManager>()
                .Initialize(this.GetType().Assembly);
        }
        protected void LoadExistingData()
        {
            ObjectContainer.Resolve<IExistingDataManager>()
                .Initialize(this.GetType().Assembly);
        }
    }
}
