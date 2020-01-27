using BoDi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace PossumLabs.DSL.Web.Integration
{
    [Binding]
    public class HtmlInjectionSteps : RepositoryStepBase<HtmlInjection>
    {
        public HtmlInjectionSteps(IObjectContainer objectContainer) : base(objectContainer)
        {

        }
    }
}