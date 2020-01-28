using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;

namespace PossumLabs.DSL.Web.Selectors
{
    public abstract class Selector
    {
        public void Init(string constructor, List<Func<string, IEnumerable<SelectorPrefix>, IWebDriver, IEnumerable<Element>>> sequencedByOrder)
        {
            Constructor = constructor;
            SequencedByOrder = sequencedByOrder;
        }

        public string Constructor { get; private set; }
        private By By { get; set; }
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
                            () => Constructor,
                            (driver, prefixes) => By(Constructor, prefixes, driver)));
                }

            }
        }

        public virtual string Type => SelectorNames.Unknown;
    }
}

