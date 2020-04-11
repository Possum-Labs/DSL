using BoDi;
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

        protected override void IoCInitialialization()
        {
            base.IoCInitialialization();
            ObjectContainer.RegisterTypeAs<CustomElementFactory,IElementFactory>();
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

        [BeforeScenario(Order = int.MinValue + 1)]
        public  virtual void SetupInfrastructureEnglish()
            => base.SetupInfrastructure();

        [BeforeScenario(Order = int.MinValue + 11)]
        public virtual void LoadTemplatesEnglish()
        => LoadTemplates();
        

        [BeforeScenario(Order = int.MinValue + 12)]
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
