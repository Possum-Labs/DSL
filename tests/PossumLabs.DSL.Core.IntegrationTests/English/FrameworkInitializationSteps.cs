﻿using BoDi;
using PossumLabs.DSL.Core.Variables;
using TechTalk.SpecFlow;

namespace PossumLabs.DSL.Core.IntegrationTests
{
    [Binding]
    public class FrameworkInitializationSteps : FrameworkInitializationStepsBase
    {
        public FrameworkInitializationSteps(IObjectContainer objectContainer) : base(objectContainer) { }

        [StepArgumentTransformation]
        public Characteristics TransformEnglish(string id) => base.Transform(id);

        [BeforeScenario(Order = int.MinValue+1)]
        public void SetupInfrastructureEnglish()
            => base.SetupInfrastructure();
    }
}
