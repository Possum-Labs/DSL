﻿using BoDi;
using TechTalk.SpecFlow;

namespace PossumLabs.DSL.English
{
    [Binding]
    public class FrameworkInitializationSteps : FrameworkInitializationStepsBase
    {
        public FrameworkInitializationSteps(IObjectContainer objectContainer) : base(objectContainer) { }

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
