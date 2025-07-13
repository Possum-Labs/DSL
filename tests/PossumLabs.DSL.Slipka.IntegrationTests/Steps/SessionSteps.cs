using PossumLabs.DSL.Slipka.IntegrationTests;
using PossumLabs.DSL.Core.Files;
using PossumLabs.DSL.Slipka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reqnroll;
using Reqnroll.BoDi;

namespace PossumLabs.DSL.Slipka.IntegrationTests
{
    [Binding]
    public class SessionSteps : RepositoryStepBase<Session>
    {
        public SessionSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
        }
    }
}
