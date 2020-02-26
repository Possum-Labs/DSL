using BoDi;
using PossumLabs.DSL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace DSL.Documentation.Example
{
    [Binding]
    public class HtmlInjectionSteps : RepositoryStepBase<HtmlInjection>
    {
        public HtmlInjectionSteps(IObjectContainer objectContainer) : base(objectContainer)
        {

        }
    }
}