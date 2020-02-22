using BoDi;
using Microsoft.Extensions.Configuration;
using PossumLabs.DSL.Core.Configuration;
using PossumLabs.DSL.Core.Files;
using PossumLabs.DSL.Core.Logging;
using PossumLabs.DSL.DataGeneration;
using PossumLabs.DSL.Web.Diagnostic;
using PossumLabs.DSL.Web.Selectors;
using System.IO;
using TechTalk.SpecFlow;

namespace DSL.Documentation.Example
{
    [Binding]
    public class FrameworkInitializationSteps : PossumLabs.DSL.English.FrameworkInitializationSteps
    {
        public FrameworkInitializationSteps(IObjectContainer objectContainer) : base(objectContainer) { }

        [BeforeScenario(Order = int.MinValue + 1)]
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
            Register<ElementFactory>(new ElementFactory());
            //This is where the override is placed
            Register<XpathProvider>(new CustomXpathProvider());
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
    }
}
