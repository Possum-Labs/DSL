using BoDi;
using PossumLabs.DSL;
using TechTalk.SpecFlow;
using PossumLabs.DSL.Web;

namespace PossumLabs.DSL.English
{
    [Binding]
    public class TableSteps : TableStepsBase
    {
        public TableSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
        }

        [When(@"entering into Table")]
        public  void WhenEnteringIntoTableEnglish(Table table)
            => base.WhenEnteringIntoTable(table);

        [Then(@"the Table has the values")]
        public  void ThenTheTableHasValuesEnglish(TableValidation table)
            => base.ThenTheTableHasValues(table);
    }
}
