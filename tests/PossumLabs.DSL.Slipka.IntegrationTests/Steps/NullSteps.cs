using Reqnroll.BoDi;
using PossumLabs.DSL.Core.Variables;
using System;
using System.Collections.Generic;
using System.Text;
using Reqnroll;

namespace PossumLabs.DSL.Slipka.IntegrationTests
{
    public class Fake :IValueObject { }

    [Binding]
    public class NullSteps : RepositoryStepBase<Fake>
    {
        public NullSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
        }

        [BeforeScenario]
        public void Seed()
        {
            this.Add("null", null);
        }
    }
}
