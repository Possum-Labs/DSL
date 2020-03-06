using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace DSL.Documentation.Example
{
    [Binding]
    public class NoopStep
    {
        [Given("noop")]
        public void Noop()
        { }
    }
}
