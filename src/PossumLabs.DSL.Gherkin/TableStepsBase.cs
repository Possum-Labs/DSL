using Reqnroll.BoDi;
using System.Linq;
using Reqnroll;
using PossumLabs.DSL.Web;

namespace PossumLabs.DSL
{
    public abstract class TableStepsBase : WebDriverStepsBase
    {
        public TableStepsBase(IObjectContainer objectContainer) : base(objectContainer)
        {
        }

        protected virtual void WhenEnteringIntoTable(Table table)
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

        protected virtual void ThenTheTableHasValues(TableValidation table)
            => Executor.Execute(() =>
        {
            var tableElement = base.WebDriver.GetTables(table.Header);
            var e = table.Validate(tableElement);
            if (e != null)
                throw e;
        });
    }
}
