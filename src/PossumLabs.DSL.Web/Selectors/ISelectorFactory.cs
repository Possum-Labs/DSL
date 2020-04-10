using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace PossumLabs.DSL.Web.Selectors
{
    public interface ISelectorFactory
    {
        Dictionary<string, List<Func<string, IEnumerable<string>>>> Prefixes { get; }
        Dictionary<string, List<Func<string, IEnumerable<SelectorPrefix>, IWebDriver, IEnumerable<Element>>>> Selectors { get; }

        T CreatePrefix<T>(string constructor = "") where T : SelectorPrefix, new();
        T CreateSelector<T>(string constructor) where T : Selector, new();
    }
}