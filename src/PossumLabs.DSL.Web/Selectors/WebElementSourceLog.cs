using OpenQA.Selenium;
using PossumLabs.DSL.Core.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PossumLabs.DSL.Web.Selectors
{
    public class WebElementSourceLog
    {
        public WebElementSourceLog()
        {
            WebElementSources = new ConcurrentDictionary<IWebElement, WebElementSource>();
            Order = 0;
        }
        private int Order { get; set; }

        public class WebElementSource
        {
            public WebElementSource(string page, By by, int order)
            {
                Page = page;
                By= by;
                Order = order;
            }
            public string Page { get; }
            public By By { get; }
            public string SelectorType { get; set; }
            public string SelectorConstructor { get; set; }
            public int  Order { get; set; }
        }

        public void Add(IWebElement e, string page, By by)
        {
            var o = -1;

            lock (this)
                o = Order++;

            WebElementSources.TryAdd(e, new WebElementSource(page, by, o));
        }

        public WebElementSource Get(IWebElement e)
        {
            if (WebElementSources.TryGetValue(e, out var value))
                return value;
            return
                null;
        }

        private ConcurrentDictionary<IWebElement, WebElementSource> WebElementSources { get; }

        public void Log(ILog logger)
        {
            logger.Section(this.GetType().Name,  WebElementSources.Values
                .Where(x => x.By != null && x.SelectorType != null)
                .OrderBy(x => x.Order)
                .Select(x => new WebElementLogRecord(
                    x.Page,
                    x.SelectorType,
                    x.SelectorConstructor,
                    x.By.ToString()
                )).ToArray());
        }
    }

    public class WebElementLogRecord
    {
        public string Page { get; }
        public string SelectorType { get; }
        public string SelectorConstructor { get; }
        public string By { get; }

        public WebElementLogRecord(string page, string selectorType, string selectorConstructor, string by)
        {
            Page = page;
            SelectorType = selectorType;
            SelectorConstructor = selectorConstructor;
            By = by;
        }
    }
}
