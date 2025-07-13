using Reqnroll.BoDi;
using PossumLabs.DSL.Core;
using PossumLabs.DSL.Slipka;
using PossumLabs.DSL.Slipka.ValueObjects;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Reqnroll;

namespace PossumLabs.DSL.Slipka.IntegrationTests
{
    [Binding]
    public class HeaderSteps : RepositoryStepBase<Header>
    {
        public HeaderSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
        }
    }
}
