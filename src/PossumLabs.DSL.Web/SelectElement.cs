using OpenQA.Selenium;
using PossumLabs.DSL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PossumLabs.DSL.Web
{
    public class SelectElement : Element
    {
        private Dictionary<string, IWebElement> Options { get; }

        public SelectElement(IWebElement element, IWebDriver driver) : base(element, driver)
        {
            if (element.TagName == "select")
            {
                OldStyleSelect = new OpenQA.Selenium.Support.UI.SelectElement(element);
                LazyAvailableOptions = new Lazy<IList<IWebElement>>(() => OldStyleSelect.Options);
                LazySelectedOptions = new Lazy<IList<IWebElement>>(() => OldStyleSelect.AllSelectedOptions);
            }
            else
            {
                var listId = element.GetAttribute("list");
                if (string.IsNullOrWhiteSpace(listId))
                    throw new Exception("The {element.TagName} is neither a select element nor an element with a list attribute");
                LazyAvailableOptions = new Lazy<IList<IWebElement>>(() => driver.FindElements(By.XPath($"//datalist[@id='{listId}']/option")));
                LazySelectedOptions = new Lazy<IList<IWebElement>>(() => new List<IWebElement>());
                var value = element.GetAttribute("value");
                if (!string.IsNullOrWhiteSpace(value))
                {
                    var selected = AvailableOptions.Where(o => o.GetAttribute("value") == value);
                    if (selected.None())
                        throw new Exception($"element with list:{listId} and value:{value} did not find any mathching options in the {AvailableOptions.Count()} options");
                    if (selected.Many())
                        throw new Exception($"element with list:{listId} and value:{value} found {selected.Count()} mathching options in the {AvailableOptions.Count()} options");
                    SelectedOptions.Add(selected.First());
                }
            }
        }

        protected OpenQA.Selenium.Support.UI.SelectElement OldStyleSelect { get; }
        private Lazy<IList<IWebElement>> LazyAvailableOptions { get; }
        private Lazy<IList<IWebElement>> LazySelectedOptions { get; }

        protected IList<IWebElement> AvailableOptions => LazyAvailableOptions.Value;
        protected IList<IWebElement> SelectedOptions => LazySelectedOptions.Value;

        public override void Enter(string text)
        {
            if (OldStyleSelect != null)
            {
                if (text == null) return;

                var id = OldStyleSelect.WrappedElement.GetAttribute("id");
                if (string.IsNullOrWhiteSpace(id))
                {
                    try
                    {
                        OldStyleSelect.SelectByText(text);
                        return;
                    }
                    catch { }

                    try
                    {
                        OldStyleSelect.SelectByValue(text);
                        return;
                    }
                    catch { }
                }

                var options = base.WebDriver.FindElements(
                By.XPath($"//select[@id='{id}']/option[" +
                $"@value ='{text}' or " +
                $"text() = '{text}']"));

                if (options.One())
                {
                    OldStyleSelect.SelectByValue(options.First().GetAttribute("value"));
                    return;
                }
                else if (options.Many())
                {
                    if (options.One(x => x.Text == text))
                        OldStyleSelect.SelectByValue(options.First(x => x.Text == text).GetAttribute("value"));
                    else
                        OldStyleSelect.SelectByValue(options.First().GetAttribute("value"));
                }
                else
                    throw new GherkinException("no matches"); //TODO: cleanup
            }
            else
            {
                var options = AvailableOptions.Where(o => string.Equals(o.GetAttribute("value"), text, ComparisonDefaults.StringComparison));
                if (options.One())
                    WebElement.SendKeys(options.First().GetAttribute("value"));
                else if (options.Many())
                    throw new GherkinException("too many matches"); //TODO: cleanup
                else
                    throw new GherkinException("no matches"); //TODO: cleanup
            }
        }

        public override List<string> Values => SelectedOptions
            .SelectMany(x=>new List<string>() { x.Text, x.GetAttribute("value") })
            .Where(s=>!string.IsNullOrWhiteSpace(s))
            .ToList();

        public override void Clear()
        {
            if (OldStyleSelect != null)
                OldStyleSelect.DeselectAll();
            else
                base.Clear();
        }
    }
}
