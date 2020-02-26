using BoDi;
using PossumLabs.DSL.Core;
using PossumLabs.DSL.Slipka.ValueObjects;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace PossumLabs.DSL.Slipka.IntegrationTests
{
    [Binding]
    public class CallCollectionSteps : RepositoryStepBase<CallCollection>
    {
        public CallCollectionSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
        }
    }
}
