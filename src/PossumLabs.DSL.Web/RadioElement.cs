using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using OpenQA.Selenium;

namespace PossumLabs.DSL.Web
{
    public class RadioElement : Element
    {
        private Dictionary<string, IWebElement> Options { get; }

        public RadioElement(IEnumerable<IWebElement> elements, IWebDriver driver): base(elements.First(), driver)
        {
            Options = new Dictionary<string, IWebElement>();
            foreach(var e in elements)
            {
                //TODO: unsafe
                Options.AddOrUpdate(e.GetAttribute("value"), e);
                if (!string.IsNullOrWhiteSpace(e.GetAttribute("aria-labelledby")))
                {
                    var lables = e.GetAttribute("aria-labelledby").Split(' ').Select(id => driver.FindElement(By.Id(id)));
                    var text = lables.Select(l => l.Text).OrderBy(s => s).Aggregate((x, y) => x + " " + y);
                    Options.AddUnlessPresent(text, e);
                    continue;
                }
                if (!string.IsNullOrWhiteSpace(e.GetAttribute("aria-label")))
                {
                    Options.AddUnlessPresent(e.GetAttribute("aria-label"), e);
                    continue;
                }
                var forLabels = driver.FindElements(By.XPath($"//label[@for='{e.GetAttribute("id")}']"));
                if(forLabels.Any())
                {
                    var lables = forLabels;
                    var text = lables.Select(l => l.Text).OrderBy(s => s).Aggregate((x, y) => x + " " + y);
                    Options.AddUnlessPresent(text, e);
                    continue;
                }
                if (!string.IsNullOrWhiteSpace(e.Text))
                {
                    Options.AddUnlessPresent(e.Text, e);
                    continue;
                }
                var parrent = e.FindElement(By.XPath(".."));
                if(parrent.TagName == "label")
                {
                    Options.AddUnlessPresent(parrent.Text, e);
                    continue;
                }
            }
        }

        public override void Enter(string text)
        {
            if (Options.ContainsKey(text))
                Options[text].Click();
            //TODO: wonky order stuff
        }

        public override List<string> Values => new List<string>(Options.Keys.Where(k=> Options[k].GetAttribute("checked")=="true"));
        }
}
