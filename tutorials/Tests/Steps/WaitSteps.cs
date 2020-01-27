using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace LegacyTest.Steps
{
    [Binding]
    public sealed class WaitSteps
    {
        [Given(@"wait (.*) ms")]
        [When(@"wait (.*) ms")]
        [Then(@"wait (.*) ms")]
        public void ThenWaitMs(int duration)
        {
            System.Threading.Thread.Sleep(duration);
        }

    }
}
