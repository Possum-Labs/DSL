using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using PossumLabs.DSL.Core;
using PossumLabs.DSL.Core.Threading;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PossumLabs.DSL.Web.Selectors
{
    public class SelectorFactory
    {
        public SelectorFactory(ElementFactory elementFactory, XpathProvider xpathProvider)
        {
            ElementFactory = elementFactory;
            XpathProvider = xpathProvider;

            Selectors = new Dictionary<string, List<Func<string, IEnumerable<SelectorPrefix>, IWebDriver, IEnumerable<Element>>>>
            {
                {
                    SelectorNames.Settable,
                    new List<Func<string, IEnumerable<SelectorPrefix>, IWebDriver, IEnumerable<Element>>>
                    {
                        ByForAttribute,
                        ByNestedInLabel(XpathProvider.SettableElements),
                        ByNested(XpathProvider.SettableElements),
                        ByText(XpathProvider.SettableElements),
                        ByLabelledBy,
                        RadioByName,
                        ByFollowingMarker(XpathProvider.SettableElements),
                        ByCellBelow(XpathProvider.SettableElements),
                        ByLabelAncestor(XpathProvider.ActiveElements),
                    }
                },
                {
                    SelectorNames.Clickable,
                    new List<Func<string, IEnumerable<SelectorPrefix>, IWebDriver, IEnumerable<Element>>>
                    {
                        ByForAttribute,
                        ByNestedInLabel(XpathProvider.ClickableElements),
                        ByNested(XpathProvider.ClickableElements),
                        ByText(XpathProvider.ClickableElements),
                        ByTitle,
                        RadioByName,
                        SpecialButtons,
                        ByFollowingMarker(XpathProvider.ClickableElements),
                        ByCellBelow(XpathProvider.ClickableElements),
                        ByTypeAncestor(XpathProvider.ActiveElements),
                    }
                },
                {
                    SelectorNames.Active,
                    new List<Func<string, IEnumerable<SelectorPrefix>, IWebDriver, IEnumerable<Element>>>
                    {
                        ByForAttribute,
                        ByNestedInLabel(XpathProvider.ActiveElements),
                        ByNested(XpathProvider.ActiveElements),
                        ByText(XpathProvider.ActiveElements),
                        ByTitle,
                        ByLabelledBy,
                        RadioByName,
                        SpecialButtons,
                        ByFollowingMarker(XpathProvider.ActiveElements),
                        ByCellBelow(XpathProvider.ActiveElements),
                        ByLabelAncestor(XpathProvider.ActiveElements),
                        ByTypeAncestor(XpathProvider.ActiveElements),
                    }
                },
                {
                    SelectorNames.Content,
                    new List<Func<string, IEnumerable<SelectorPrefix>, IWebDriver, IEnumerable<Element>>>
                    {
                        ByContentSelf,
                        ByContentSelfForRow,
                        ByContent
                    }
                },
                {
                    SelectorNames.Selectable  ,
                    new List<Func<string, IEnumerable<SelectorPrefix>, IWebDriver, IEnumerable<Element>>>
                    {
                        ByForAttribute,
                        ByNestedInLabel(XpathProvider. SelectableElements  ),
                        ByNested(XpathProvider. SelectableElements  ),
                        ByText(XpathProvider. SelectableElements  ),
                        ByLabelledBy,
                        ByFollowingMarker(XpathProvider. SelectableElements  ),
                        ByCellBelow(XpathProvider. SelectableElements  ),
                    }
                },
                {
                    SelectorNames.Checkable  ,
                    new List<Func<string, IEnumerable<SelectorPrefix>, IWebDriver, IEnumerable<Element>>>
                    {
                        ByForAttribute,
                        ByNestedInLabel(XpathProvider.CheckableElements  ),
                        ByNested(XpathProvider.CheckableElements  ),
                        ByText(XpathProvider.CheckableElements  ),
                        ByLabelledBy,
                        ByFollowingMarker(XpathProvider.CheckableElements  ),
                        ByCellBelow(XpathProvider.CheckableElements  ),
                        ByLabelAncestor(XpathProvider.CheckableElements),
                    }
                },
            };

            Prefixes = new Dictionary<string, List<Func<string, IEnumerable<string>>>>
            {
                {
                    PrefixNames.Row,
                    new List<Func<string, IEnumerable<string>>>
                    {
                        TableRow,
                        DivRoleRow,
                    }
                },
                {
                    PrefixNames.Under,
                    new List<Func<string, IEnumerable<string>>>
                    {
                        ParrentDiv,
                        ParrentDivWithRowRole,
                        FollowingRow,
                        Legend
                    }
                },
                { PrefixNames.Warning, new List<Func<string, IEnumerable<string>>> { } },
                { PrefixNames.Error, new List<Func<string, IEnumerable<string>>> { } }
            };

            
        }

        protected ElementFactory ElementFactory { get; }
        protected XpathProvider XpathProvider { get; }

        protected static readonly Core.EqualityComparer<IWebElement> Comparer =
            new Core.EqualityComparer<IWebElement>((x, y) => 
            (x.Location == y.Location  && x.TagName == y.TagName));

        virtual protected bool Filter(IWebElement e)
        => e is RemoteWebElement && ((RemoteWebElement)e).Displayed && ((RemoteWebElement)e).Enabled;
            
        public T CreateSelector<T>(string constructor) where T : Selector, new()
        {
            var t = new T();
            if (Parser.IsId.IsMatch(constructor))
                t.Init(Parser.IsId.Match(constructor).Groups[1].Value,
                    new List<Func<string, IEnumerable<SelectorPrefix>, IWebDriver, IEnumerable<Element>>> { ById });
            else if (Parser.IsElement.IsMatch(constructor))
                t.Init(Parser.IsElement.Match(constructor).Groups[1].Value,
                    new List<Func<string, IEnumerable<SelectorPrefix>, IWebDriver, IEnumerable<Element>>> { ByTag });
            else if (Parser.IsClass.IsMatch(constructor))
                t.Init(Parser.IsClass.Match(constructor).Groups[1].Value,
                    new List<Func<string, IEnumerable<SelectorPrefix>, IWebDriver, IEnumerable<Element>>> { ByClass });
            else if (Parser.IsXPath.IsMatch(constructor))
                t.Init(Parser.IsXPath.Match(constructor).Groups[1].Value,
                    new List<Func<string, IEnumerable<SelectorPrefix>, IWebDriver, IEnumerable<Element>>> { ByXpath });
            else if (Selectors.ContainsKey(t.Type) && Selectors[t.Type].Any())
                t.Init(constructor, Selectors[t.Type]);
            else
                throw new GherkinException($"the selector type of '{t.Type}' is not supported.");
            return t;
        }

        public T CreatePrefix<T>(string constructor = "") where T : SelectorPrefix, new()
        {
            var t = new T();
            if (Parser.IsId.IsMatch(constructor))
                t.Init(constructor, $"//*[@id='{Parser.IsId.Match(constructor).Groups[1].Value}']");
            else if (Parser.IsElement.IsMatch(constructor))
                t.Init(constructor, $"//{Parser.IsElement.Match(constructor).Groups[1].Value}");
            else if (Parser.IsClass.IsMatch(constructor))
                t.Init(constructor, $"//*[contains(concat(' ', normalize-space(@class), ' '), ' {Parser.IsClass.Match(constructor).Groups[1].Value} ')]");
            else if (Parser.IsXPath.IsMatch(constructor))
                t.Init(constructor, Parser.IsClass.Match(constructor).Groups[1].Value);
            else if (Prefixes.ContainsKey(t.Type) && Prefixes[t.Type].Any())
                t.Init(constructor, Prefixes[t.Type]);
            else
                throw new GherkinException($"the prefix type of '{t.Type}' is not supported.");
            return t;
        }

        protected IEnumerable<Element> UnfilteredPermutate(IEnumerable<SelectorPrefix> prefixes, IWebDriver driver, Func<string, string> xpathMaker)
        => prefixes.CrossMultiply().ParallelFirstOrException(prefix =>
                      driver
                          .FindElements(By.XPath(xpathMaker(prefix)))
                          .Distinct(Comparer)
                          .Select(e => ElementFactory.Create(driver, e)),
                    result => result.Any()
                   )?.Item ?? new Element[] { };


        protected IEnumerable<Element> Permutate(IEnumerable<SelectorPrefix> prefixes, IWebDriver driver, Func<string, string> xpathMaker, string name = null)
        => prefixes.CrossMultiply().ParallelFirstOrException(prefix =>
                  driver.FindElements(By.XPath(xpathMaker(prefix)))
                    .Where(Filter)
                    .Distinct(Comparer)
                    .Select(e => ElementFactory.Create(driver, e)).ToArray(),
                  result => result.Any()
                  )?.Item ?? new Element[] { };
        
           

        public Dictionary<string, List<Func<string, IEnumerable<SelectorPrefix>, IWebDriver, IEnumerable<Element>>>> Selectors { get; }
        public Dictionary<string, List<Func<string, IEnumerable<string>>>> Prefixes { get; }

        #region Selectors
        //https://w3c.github.io/using-aria/

        //<label for="female">target</label>
        //label[@for and text()='{target}']
        virtual protected Func<string, IEnumerable<SelectorPrefix>, IWebDriver, IEnumerable<Element>> ByForAttribute =>
            (target, prefixes, driver) =>
                prefixes.CrossMultiply().ParallelFirstOrException(prefix =>
                {
                    var elements = driver.FindElements(By.XPath($"{prefix}//label[@for and {XpathProvider.TextMatch(target)}]"));
                    if (elements.One())
                        return elements.SelectMany(e => driver.FindElements(By.Id(e.GetAttribute("for"))))
                        .Select(e => ElementFactory.Create(driver, e));
                    if (elements.Many())
                    {
                        var visible = elements.Where(e => e.Displayed);
                        if (visible.One())
                            return visible.SelectMany(e => driver.FindElements(By.Id(e.GetAttribute("for"))))
                                .Select(e => ElementFactory.Create(driver, e));
                    }
                    return new Element[] { };
                },
               result => result.Any()
                )?.Item ?? new Element[] { };

        //label[text()[normalize-space(.)='Bob']]/*[self::input]
        //<label>target<input type = "text" ></ label >
        //label[text()='{target}']/*[self::input or self::textarea or self::select]
        virtual protected Func<string, IEnumerable<SelectorPrefix>, IWebDriver, IEnumerable<Element>> ByNestedInLabel(string elementType) =>
            (target, prefixes, driver) => Permutate(prefixes, driver, (prefix) => 
                $"{prefix}//*[(self::label or self::div) and {XpathProvider.TextMatch(target)}]/*[{elementType}]");


        //<label><span><strong>target</strong></span><input type = "text" ></ label >
        //*[text()='{target}']/ancestor::label//*[self::input or self::textarea or self::select]
        virtual protected Func<string, IEnumerable<SelectorPrefix>, IWebDriver, IEnumerable<Element>> ByLabelAncestor(string elementType) =>
            (target, prefixes, driver) => Permutate(prefixes, driver, (prefix) =>
                $"{prefix}//*[{XpathProvider.MarkerElements} and {XpathProvider.TextMatch(target)}]/ancestor::label//*[{elementType}]");

        //<button><span>target</span></button>  
        //*[text()='{target}']/ancestor::*[]
        virtual protected Func<string, IEnumerable<SelectorPrefix>, IWebDriver, IEnumerable<Element>> ByTypeAncestor(string elementType) =>
            (target, prefixes, driver) => Permutate(prefixes, driver, (prefix) =>
                $"{prefix}//*[{XpathProvider.MarkerElements} and {XpathProvider.TextMatch(target)}]/parent::*[{elementType}]");


        virtual protected Func<string, IEnumerable<SelectorPrefix>, IWebDriver, IEnumerable<Element>> SpecialButtons =>
            (target, prefixes, driver) => Permutate(prefixes, driver, (prefix) =>
                $"{prefix}//*[(self::input or self::button) and @type={target.XpathEncode()} and (@type='submit' or @type='reset')]");

        //<input aria-label="target">
        //*[(self::a or self::button or @role='button' or @role='link' or @role='menuitem' or self::input or self::textarea or self::select) and @aria-label='{target}']
        virtual protected Func<string, IEnumerable<SelectorPrefix>, IWebDriver, IEnumerable<Element>> ByNested(string elementType) =>
            (target, prefixes, driver) => Permutate(prefixes, driver, (prefix) => 
                $"{prefix}//*[{elementType} and (" +
                    $"{XpathProvider.TextMatch(target)} or " +
                    $"label[{XpathProvider.TextMatch(target)}] or " +
                    $"((@type='button' or @type='submit' or @type='reset') and @value={target.XpathEncode()}) or " +
                    $"@name={target.XpathEncode()} or " +
                    $"@aria-label={target.XpathEncode()} or " +
                    $"(@type='radio' and @value={target.XpathEncode()})" +
                $")]");

        //<a href = "https://www.w3schools.com/html/" >target</a>
        //*[(self::a or self::button or @role='button' or @role='link' or @role='menuitem') and text()='{target}']
        virtual protected Func<string, IEnumerable<SelectorPrefix>, IWebDriver, IEnumerable<Element>> ByText(string elementType) =>
            (target, prefixes, driver) => Permutate(prefixes, driver, (prefix) => 
                $"{prefix}//*[{elementType} and {XpathProvider.TextMatch(target)}]");

        //<a href = "https://www.w3schools.com/html/" title="target">Visit our HTML Tutorial</a>
        //a[@title='{target}']
        virtual protected Func<string, IEnumerable<SelectorPrefix>, IWebDriver, IEnumerable<Element>> ByTitle =>
            (target, prefixes, driver) => Permutate(prefixes, driver, (prefix) =>
                $"{prefix}//a[@title={target.XpathEncode()}]");

        //<input type="radio" id="i1" name="target"
        virtual protected Func<string, IEnumerable<SelectorPrefix>, IWebDriver, IEnumerable<Element>> RadioByName =>
            (target, prefixes, driver) =>
                prefixes.CrossMultiply().ParallelFirstOrException(prefix =>
                {
                    var elements = driver.FindElements(By.XPath($"{prefix}//input[@type='radio' and @name={target.XpathEncode()}]"));
                        if (elements.Any())
                            return new Element[] { new RadioElement(elements, driver) };
                    return new Element[] { };
                },
               result => result.Any()
                )?.Item ?? new Element[] { };

        //<input aria-labelledby= "l1 l2 l3"/>
        //*[(self::a or self::button or @role='button' or @role='link' or @role='menuitem' or self::input or self::textarea or self::select) and  @aria-labelledby]
        virtual protected Func<string, IEnumerable<SelectorPrefix>, IWebDriver, IEnumerable<Element>> ByLabelledBy =>
            (target, prefixes, driver) =>
                prefixes.CrossMultiply().ParallelFirstOrException(prefix =>
                {
                    var elements = driver.FindElements(By.XPath($"{prefix}//*[{XpathProvider.ActiveElements} and  @aria-labelledby]"));
                        if (elements.Any())
                        {
                            return elements.Where(e =>
                            {
                                var ids = e.GetAttribute("aria-labelledby").Split(' ').Select(s => s.Trim()).Where(s => !String.IsNullOrEmpty(s));
                                var labels = ids.SelectMany(id => driver.FindElements(By.Id(id))).Select(l => l.Text);
                                var t = target;
                                foreach (var l in labels.Where(s=>!string.IsNullOrWhiteSpace(s)))
                                    t = t.Replace(l, string.Empty);
                                return string.IsNullOrWhiteSpace(t);
                            }).Select(e => ElementFactory.Create(driver, e));
                        }
                    return new Element[] { };
                },
               result => result.Any()
                )?.Item ?? new Element[] { };

        // //tr[*[self::td][*[( self::span ) and text()[normalize-space(.)='Add Clinic']]]]/following-sibling::tr[1]/td[1+count(//*[self::td][*[( self::span ) and text()[normalize-space(.)='Add Clinic']]]/preceding-sibling::*[self::td])]
        virtual protected Func<string, IEnumerable<SelectorPrefix>, IWebDriver, IEnumerable<Element>> ByCellBelow(string elementType) =>
            (target, prefixes, driver) => Permutate(prefixes, driver, (prefix) => 
                $"{prefix}//tr[*[self::td or self::th][*[{XpathProvider.MarkerElements} and {XpathProvider.TextMatch(target)}]]]/following-sibling::tr[1]/td[1+count(//*[self::td or self::th][*[{XpathProvider.MarkerElements} and {XpathProvider.TextMatch(target)}]]/preceding-sibling::*[self::td or self::th])]/*[{elementType}]");

        //<a href = "https://www.w3schools.com/html/" title="target">Visit our HTML Tutorial</a>
        //a[@title='{target}']
        virtual protected Func<string, IEnumerable<SelectorPrefix>, IWebDriver, IEnumerable<Element>> ByFollowingMarker(string elementType) =>
                (target, prefixes, driver) => Permutate(prefixes, driver, (prefix) => 
                    $"{prefix}//*[{XpathProvider.MarkerElements} and {XpathProvider.TextMatch(target)}]/following-sibling::*[not(self::br or self::hr)][1][{elementType}]");

        #endregion

        #region content
        virtual protected Func<string, IEnumerable<SelectorPrefix>, IWebDriver, IEnumerable<Element>> ByContentSelf =>
            (target, prefixes, driver) => Permutate(prefixes, driver, (prefix) =>
                string.IsNullOrWhiteSpace(prefix) ?
                    "//*[1=2]" : //junk, valid xpath that never returns anything. used for prefixes.
                    $"{prefix}[{XpathProvider.ContentElements} and {XpathProvider.TextMatch(target)}]");

        virtual protected Func<string, IEnumerable<SelectorPrefix>, IWebDriver, IEnumerable<Element>> ByContentSelfForRow =>
            (target, prefixes, driver) => Permutate(prefixes, driver, (prefix) =>
                string.IsNullOrWhiteSpace(prefix) ?
                    "//*[1=2]" : //junk, valid xpath that never returns anything. used for prefixes.
                    $"{prefix}/*[({XpathProvider.ContentElements} or self::td) and {XpathProvider.TextMatch(target)}]");


        virtual protected Func<string, IEnumerable<SelectorPrefix>, IWebDriver, IEnumerable<Element>> ByContent =>
            (target, prefixes, driver) => Permutate(prefixes, driver, (prefix) => 
                $"{prefix}//*[{XpathProvider.ContentElements} and {XpathProvider.TextMatch(target)}]");

        #endregion

        #region id & class & tag selectors
        //https://stackoverflow.com/questions/1604471/how-can-i-find-an-element-by-css-class-with-xpath
        virtual protected Func<string, IEnumerable<SelectorPrefix>, IWebDriver, IEnumerable<Element>> ByClass =>
            (target, prefixes, driver) => UnfilteredPermutate(prefixes, driver, (prefix) =>
                $"{prefix}//*[contains(concat(' ', normalize-space(@class), ' '), ' {target} ')]");


        virtual protected Func<string, IEnumerable<SelectorPrefix>, IWebDriver, IEnumerable<Element>> ByTag =>
            (target, prefixes, driver) => UnfilteredPermutate(prefixes, driver, (prefix) =>
                $"{prefix}//{target}");

        virtual protected Func<string, IEnumerable<SelectorPrefix>, IWebDriver, IEnumerable<Element>> ById =>
            (target, prefixes, driver) => UnfilteredPermutate(prefixes, driver, (prefix) =>
                $"{prefix}//*[@id = {target.XpathEncode()}]");

        virtual protected Func<string, IEnumerable<SelectorPrefix>, IWebDriver, IEnumerable<Element>> ByXpath =>
            (target, prefixes, driver) => UnfilteredPermutate(prefixes, driver, (prefix) =>
                $"{prefix}{target}");

        #endregion

        #region Prefixes
        virtual protected Func<string, IEnumerable<string>> TableRow =>
            (target) => new List<string>(){
                $"//tr[td[{XpathProvider.TextMatch(target)}]]",
                $"//tr[td/*[{XpathProvider.MarkerElements} and {XpathProvider.TextMatch(target)}]]",
                $"//tr[td/*[@value = {target.XpathEncode()}]]",
                $"//tr[td/select/option[@selected='selected' and {XpathProvider.TextMatch(target)}]]"
            };

        virtual protected Func<string, IEnumerable<string>> DivRoleRow =>
            (target) => new List<string>() {
                $"//*[{XpathProvider.MarkerElements} and {XpathProvider.TextMatch(target)}]/ancestor::div[@role='row'][1]",
                $"//*[@value = {target.XpathEncode()}]/ancestor::div[@role='row'][1]",
                $"//select[option[@selected='selected' and {XpathProvider.TextMatch(target)}]]/ancestor::div[@role='row'][1]",
                $"//div[{XpathProvider.TextMatch(target)}]/ancestor-or-self::div[@role='row'][1]"
            };

        virtual protected Func<string, IEnumerable<string>> ParrentDiv =>
            (target) => new List<string>() { $"//div[" +
                    $"{XpathProvider.TextMatch(target)} or " +
                    $"*[{XpathProvider.MarkerElements} and {XpathProvider.TextMatch(target)}] or " +
                    $"*[{XpathProvider.ActiveElements} and @value = {target.XpathEncode()}] or " +
                    $"@name={target.XpathEncode()} or " +
                    $"@aria-label={target.XpathEncode()}" +
                $"]" };

        virtual protected Func<string, IEnumerable<string>> ParrentDivWithRowRole =>
            (target) => new List<string>() {
                $"//*[{XpathProvider.MarkerElements} and {XpathProvider.TextMatch(target)}]/ancestor::div[@role='row'][1]",
                $"//*[@value = {target.XpathEncode()}]/ancestor::div[@role='row'][1]",
                $"//select[option[@selected='selected' and {XpathProvider.TextMatch(target)}]]/ancestor::div[@role='row'][1]"
            };
        virtual protected Func<string, IEnumerable<string>> Legend =>
            (target) => new List<string>() { $"//*[legend[{XpathProvider.TextMatch(target)}]]" };
        
        virtual protected Func<string, IEnumerable<string>> FollowingRow =>
            (target) => TableRow(target).Select(x => $"{x}/following-sibling::tr[1]").ToList();
        #endregion
    }
}
