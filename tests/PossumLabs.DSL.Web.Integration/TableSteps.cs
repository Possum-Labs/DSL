using BoDi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using PossumLabs.DSL.Core;
using PossumLabs.DSL.Web;

namespace PossumLabs.DSL.Web.Integration
{
    [Binding]
    public class TableSteps : WebDriverStepBase
    {
        public TableSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
        }

        [When(@"entering into Table")]
        public void WhenEnteringIntoTable(Table table)
        {
            var tableElement = FindTable(table);
 
            foreach (var row in table.Rows)
            {
                var rowId = tableElement.GetRowId(Interpeter.Get<string>(row[0]));

                for(int c = 1; c < table.Header.Count; c++)
                {
                    var e = tableElement.GetActiveElement(rowId, table.Header.ToList()[c]);
                    e.Enter(base.Interpeter.Get<string>(row[c]));
                }
            }
        }

        private TableElement FindTable(Table table)
            => base.WebDriver.GetTables(table.Header);


        //TODO:pick one
        [Then(@"the table contains")]
        [Then(@"the Table has values")]
        public void ThenTheTableHasValues(TableValidation table)
            => Executor.Execute(() =>
        {
            var tableElement = base.WebDriver.GetTables(table.Header);
            var e = table.Validate(tableElement);
            if (e != null)
                throw e;
        });
    }
}
