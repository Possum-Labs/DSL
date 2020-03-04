using BoDi;
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

        //TODO:pick one
        [Then(@"the table contains")]
        [Then(@"the Table has values")]
        public  void ThenTheTableHasValuesEnglish(TableValidation table)
            => base.ThenTheTableHasValues(table);
    }
}
