using BoDi;
using PossumLabs.DSL.Core;
using PossumLabs.DSL.Slipka;
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
    public class SlipkaInjectionSteps : RepositoryStepBase<SlipkaInjection>
    {
        public SlipkaInjectionSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
        }
    }
}
