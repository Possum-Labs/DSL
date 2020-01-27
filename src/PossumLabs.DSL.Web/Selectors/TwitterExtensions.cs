using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Web.Selectors
{
    public static class TwitterExtensions
    {
        //TODO:Bas Refactor the Permutate :(
        //public static SelectorFactory UseTwitterJs(this SelectorFactory factory)
        //{
        //    factory.Selectors[SelectorNames.Active].Add(ByTTSelectable);
        //    return factory;
        //}

        //private static Func<string, IEnumerable<SelectorPrefix>, IWebDriver, IEnumerable<Element>> ByTTSelectable =>
        //    (target, prefixes, driver) => Permutate(prefixes, driver, (prefix) =>
        //        $"{prefix}//div[contains(@class,'tt-selectable')]/*[{TextMatch(target)}]");

    }
}
