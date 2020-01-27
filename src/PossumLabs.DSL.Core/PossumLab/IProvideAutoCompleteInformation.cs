using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Core.PossumLab
{
    public interface IProvideAutoCompleteInformation
    {
        IEnumerable<AutocompleteInformationProvider> Providers { get; }
    }

    public class AutocompleteInformationProvider
    {
        public string Type { get; set; }
        public Func<AutocompleteArguments, List<string>> Provider { get; set; }
    }

    public class AutocompleteArguments
    {
        public string AutoCompleteType { get; set; }
        public string Hint { get; set; }
        public string SubType { get; set; }
    }
}
