using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Web.Selectors
{
    public static class BootstrapExtensions
    {
        public static SelectorFactory UseBootstrap(this SelectorFactory factory)
        {
            factory.Prefixes[PrefixNames.Warning].Add(Warning);
            factory.Prefixes[PrefixNames.Error].Add(Alert);
            return factory;
        }

        private static Func<string, IEnumerable<string>> Alert =>
            (target) => new List<string>() { $"//*[contains(@class, 'danger')]" };

        private static Func<string, IEnumerable<string>> Warning =>
            (target) => new List<string>() { $"//*[contains(@class, 'warning')]" };

    }
}
