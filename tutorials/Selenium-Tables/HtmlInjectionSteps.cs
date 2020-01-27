using BoDi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Selenium_Tables
{
    [Binding]
    public class HtmlInjectionSteps : RepositoryStepBase<HtmlInjection>
    {
        public HtmlInjectionSteps(IObjectContainer objectContainer) : base(objectContainer)
        {

        }
    }
}