using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace PossumLabs.DSL.Core.Validations
{
    public class DataTableValidation : Validation
    {
        public DataTableValidation(List<Dictionary<string, Validation>> validations) : base((o) => AggregatePredicate(o, validations), "")
        {
            Header = validations.First().Keys.ToList();
        }

        public List<string> Header { get; }
        public List<Dictionary<string, Validation>> Rows { get; }

        private static string AggregatePredicate(object o, List<Dictionary<string, Validation>> validations)
        {
            if (!(o is DataTable))
                return "This validation can only work on DataTables";

            var table = o as DataTable;

            foreach (var rowValidation in validations)
            {
                var possibleRow = table.Rows.Cast<DataRow>()
                    .Where(r => rowValidation.First().Value.Predicate(r[rowValidation.First().Key]) == null);
                if (possibleRow.None())
                    throw new Exception("Row not found");
                if (possibleRow.Many())
                    throw new Exception("too many rows found");

                var row = possibleRow.First();

                foreach (var column in rowValidation.Skip(1))
                {
                    var e = row[column.Key];
                    var result = column.Value.Predicate(e);
                    if (result != null)
                        return result;
                }
            }

            return null;
        }
    }
}
