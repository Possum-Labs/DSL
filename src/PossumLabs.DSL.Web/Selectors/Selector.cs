using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;

namespace PossumLabs.DSL.Web.Selectors
{
    public abstract class Selector
    {
        public void Init(string label, List<Func<string, IEnumerable<SelectorPrefix>, IWebDriver, IEnumerable<Element>>> sequencedByOrder)
        {
            Label = label;
            SequencedByOrder = sequencedByOrder;
        }

        public string Constructor { get; private set; }
        private By By { get; set; }
        private string Label { get; set; }
        private List<Func<string, IEnumerable<SelectorPrefix>, IWebDriver, IEnumerable<Element>>> SequencedByOrder { get; set; }

        internal IEnumerable<Searcher> PrioritizedSearchers
        {
            get
            {
                if (By != null)
                    return new[] {
                        new Searcher(
                            () => Constructor, 
                            (driver, prefixes) => driver.FindElements(By).Select(element => new Element(element, driver)))
                    };
                else
                {
                    return SequencedByOrder
                        .Select(By => new Searcher(
                            () => Label,
                            (driver, prefixes) => By(Label, prefixes, driver)));
                }

            }
        }

        public virtual string Type => SelectorNames.Unknown;
    }
}

