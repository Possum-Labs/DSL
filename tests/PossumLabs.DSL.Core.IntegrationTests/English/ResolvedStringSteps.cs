﻿using BoDi;
using PossumLabs.DSL.Core;
using TechTalk.SpecFlow;

namespace PossumLabs.DSL.Core.IntegrationTests
{
    [Binding]
    public class ResolvedStringSteps : ResolvedStringStepsBase
    {
        public ResolvedStringSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
        }

        [StepArgumentTransformation]
        public ResolvedString TransformEnglish(string id) 
            => base.Transform(id);
    }
}
