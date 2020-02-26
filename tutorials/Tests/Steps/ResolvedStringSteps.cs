using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoDi;
using LegacyTest.Framework;
using LegacyTest.ValueObjects;
using PossumLabs.DSL.Core;
using TechTalk.SpecFlow;

namespace DSL.Documentation.Example
{
    [Binding]
    public class ResolvedStringSteps : StepBase
    {
        public ResolvedStringSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
        }

        [StepArgumentTransformation]
        public ResolvedString Transform(string id) => Interpeter.Get<string>(id);
    }
}
