﻿using BoDi;
using DSL.Documentation.Example;
using Microsoft.Extensions.Configuration;
using PossumLabs.DSL.Core;
using PossumLabs.DSL.Core.Configuration;
using PossumLabs.DSL.Core.Files;
using PossumLabs.DSL.Core.Logging;
using PossumLabs.DSL.Core.Variables;
using PossumLabs.DSL.DataGeneration;
using PossumLabs.DSL.Web.Configuration;
using PossumLabs.DSL.Web.Diagnostic;
using PossumLabs.DSL.Web.Selectors;
using System;
using System.IO;
using System.Reflection;
using TechTalk.SpecFlow;

namespace PossumLabs.DSL.English
{
    [Binding]
    public class FrameworkInitializationSteps : FrameworkInitializationStepsBase
    {
        public FrameworkInitializationSteps(IObjectContainer objectContainer) : base(objectContainer) { }

        protected override void SetupInfrastructure()
        {
            IConfiguration config = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .AddEnvironmentVariables()
              .Build();

            NetworkWatcher = new NetworkWatcher();

            var configFactory = new ConfigurationFactory(config);

            ObjectContainer.RegisterInstanceAs(configFactory.Create<MovieLoggerConfig>());
            ObjectContainer.RegisterInstanceAs(configFactory.Create<ImageLoggingConfig>());
            WebElementSourceLog = new WebElementSourceLog();

            ImageLogging = new ImageLogging(ObjectContainer.Resolve<ImageLoggingConfig>());
            Register(new FileManager(new DatetimeManager(() => DateTime.Now)));
            FileManager.Initialize(FeatureContext.FeatureInfo.Title, ScenarioContext.ScenarioInfo.Title, null /*Specflow limitation*/);
            MovieLogger = new MovieLogger(FileManager, ObjectContainer.Resolve<MovieLoggerConfig>(), Metadata);

            ObjectContainer.RegisterInstanceAs(ImageLogging);
            ObjectContainer.RegisterInstanceAs(MovieLogger);

            Logger = new DefaultLogger(new DirectoryInfo(Environment.CurrentDirectory), new YamlLogFormatter());
            Register((PossumLabs.DSL.Core.Logging.ILog)Logger);

            //here is our override
            Register<ElementFactory>(new CustomElementFactory());

            Register<XpathProvider>(new XpathProvider());
            Register<SelectorFactory>(new SelectorFactory(ElementFactory, XpathProvider).UseBootstrap());
            Register(new PossumLabs.DSL.Web.WebDriverManager(
                this.Interpeter,
                this.ObjectFactory,
                new SeleniumGridConfiguration()));

            var dataGeneratorRepository = new DataGeneratorRepository(Interpeter, ObjectFactory);
            Register<DataGenerator>(dataGeneratorRepository.BuildGenerator());


            var templateManager = new PossumLabs.DSL.Core.Variables.TemplateManager();
            templateManager.Initialize(Assembly.GetExecutingAssembly());
            Register(templateManager);

            new PossumLabs.DSL.Core.Variables.ExistingDataManager(this.Interpeter, this.TemplateManager)
                .Initialize(this.GetType().Assembly);

            Log.Message($"Feature: {FeatureContext.FeatureInfo.Title} Scenario: {ScenarioContext.ScenarioInfo.Title} \n" +
                $"Tags: {FeatureContext.FeatureInfo.Tags.LogFormat()} {ScenarioContext.ScenarioInfo.Tags.LogFormat()}");

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            WebDriverManager.Initialize(BuildDriver);
            WebDriverManager.WebDriverFactory = WebdriverFactory;
        }

        [StepArgumentTransformation]
        public Characteristics TransformEnglish(string id) => base.Transform(id);

        [AfterStep]
        public  void LogStepEnglish()
            => base.LogStep();

        [BeforeStep]
        public  void NetworkWatcherHookEnglish()
            => base.NetworkWatcherHook();

        [AfterScenario(Order = 1)]
        public  void WebDriverStepBasedLoggingEnglish()
            => base.WebDriverStepBasedLogging();

        [AfterScenario(Order = int.MinValue + 1)]
        public  void ErrorScreenLoggingEnglish()
            => base.ErrorScreenLogging();

        [AfterScenario(Order = int.MinValue)]
        public  void CheckForAlertsEnglish()
            => base.CheckForAlerts();

        [BeforeScenario(Order = int.MinValue+1)]
        public  virtual void SetupInfrastructureEnglish()
            => base.SetupInfrastructure();

        [BeforeScenario(Order = 1)]
        public virtual void LoadTemplatesEnglish()
        => LoadTemplates();
        

        [BeforeScenario(Order = 2)]
        public virtual void LoadExistingDataEnglish()
            => LoadExistingData();

        [BeforeScenario("Movie-Logger")]
        public  void EnableMovieLoggerEnglish()
            => base.EnableMovieLogger();

        [AfterScenario]
        public  void CreateMovieEnglish()
            => base.CreateMovie();
    }
}