using BoDi;
using LegacyTest.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace LegacyTest.Steps
{
    [Binding]
    public class DateSteps : RepositoryStepBase<Date>
    {
        public DateSteps(IObjectContainer objectContainer) : base(objectContainer)
        {

        }

        [BeforeScenario]
        public void SetUpDefaultDates()
        {
            Add("Yesterday", new Date(DateTime.Today.AddDays(-1)));
            Add("Today", new Date(DateTime.Today));
            Add("Tomorrow", new Date(DateTime.Today.AddDays(1)));
        }

        [Given(@"the Dates?")]
        public void GivenTheDates(Dictionary<string, Date> dates)
            => dates.Keys.ToList().ForEach(k => Add(k, dates[k]));
    }
}
