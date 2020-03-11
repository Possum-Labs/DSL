using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using PossumLabs.DSL.Core;
using PossumLabs.DSL.Core.Exceptions;
using PossumLabs.DSL.Core.Logging;
using PossumLabs.DSL.Core.Variables;
using PossumLabs.DSL.Web.Configuration;
using PossumLabs.DSL.Web.Selectors;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PossumLabs.DSL.Web
{
    public class WebDriver : IEntity, IWebDriverWrapper
    {
        public WebDriver(
            IWebDriver seleniumDriver, 
            Func<Uri> rootUrl, 
            SeleniumGridConfiguration configuration, 
            RetryExecutor retryExecutor, 
            SelectorFactory selectorFactory,
            ElementFactory elementFactory,
            XpathProvider xpathProvider,
            MovieLogger movieLogger,
            WebElementSourceLog webElementSourceLog,
            IEnumerable<SelectorPrefix> prefixes = null)
        {
            SeleniumDriver = seleniumDriver;
            SuccessfulSearchers = new List<Searcher>();
            RootUrl = rootUrl;
            SeleniumGridConfiguration = configuration;
            RetryExecutor = retryExecutor;
            SelectorFactory = selectorFactory;
            MovieLogger = movieLogger;
            Prefixes = prefixes?.ToList() ?? new List<SelectorPrefix>() { new EmptySelectorPrefix() };

            Children = new List<WebDriver>();
            Screenshots = new List<byte[]>();
            ElementFactory = elementFactory;
            XpathProvider = xpathProvider;
            WebElementSourceLog = webElementSourceLog;
        }

        private ElementFactory ElementFactory { get; }
        private XpathProvider XpathProvider { get; }
        private Func<Uri> RootUrl { get; set; }
        public IWebDriver SeleniumDriver { get; }
        public IJavaScriptExecutor ScriptExecutor
        { get
            {
                var scriptExecutor = SeleniumDriver as IJavaScriptExecutor;
                if (scriptExecutor == null)
                    throw new Exception("this webdriver does not support script executon.");

                    return scriptExecutor;
            }
        }
        private SelectorFactory SelectorFactory { get; }
        private List<Searcher> SuccessfulSearchers { get; }
        private MovieLogger MovieLogger { get; }
        public WebElementSourceLog WebElementSourceLog { get; set; }

        public TableElement GetTables(IEnumerable<string> headers, StringComparison comparison = StringComparison.CurrentCulture, int? index = null )
        => RetryExecutor.RetryFor(() =>
        {
            
            var loggingWebdriver = new LoggingWebDriver(SeleniumDriver, MovieLogger, WebElementSourceLog);
            try
            {
                var possilbeTables = GetTables(headers.Count()).ToList();
                Func<string, string, bool> comparer = (s1, s2) => s1.Equals(s2, comparison);

                var tableElements = possilbeTables.Where(t => headers.Where(h => !string.IsNullOrEmpty(h)).Except(t.Header.Keys, 
                    new Core.EqualityComparer<string>(comparer)).None());

                if (tableElements.One())
                    return tableElements.First();
                //Exact match overrides in case of multiples.
                if (tableElements.Where(x => headers.Count() == x.MaxColumnIndex).One())
                    return tableElements.Where(x => headers.Count() == x.MaxColumnIndex).First();
                if (tableElements.Many())
                {
                    if (index == null)
                        throw new Exception($"multiple talbes matched the definition of {headers.LogFormat()}, table headers were {tableElements.LogFormat(t => $"Table: {t.Header.Keys.LogFormat()}")};");
                    if (index >= tableElements.Count())
                        throw new Exception($"only found {tableElements.Count()} tables, index of {index} is out of range. (zero based)");
                    return tableElements.ToArray()[index.Value];
                }
                //iframes ? 
                var iframes = SeleniumDriver.FindElements(By.XPath("//iframe"));
                foreach (var iframe in iframes)
                {
                    try
                    {
                        loggingWebdriver.Log($"Trying iframe:{iframe}");
                        SeleniumDriver.SwitchTo().Frame(iframe);
                        possilbeTables = GetTables(headers.Count() - 1).ToList();

                        tableElements = possilbeTables.Where(t => headers.Where(h => !string.IsNullOrEmpty(h)).Except(t.Header.Keys,
                            new Core.EqualityComparer<string>(comparer)).None());

                        if (tableElements.One())
                            return tableElements.First();
                        if (tableElements.Many())
                        {
                            if(index == null)
                                throw new Exception($"multiple talbes matched the definition of {headers.LogFormat()}, table headers were {tableElements.LogFormat(t => $"Table: {t.Header.Keys.LogFormat()}")};");
                            if (index >= tableElements.Count())
                                throw new Exception($"only found {tableElements.Count()} tables, index of {index} is out of range. (zero based)");
                            return tableElements.ToArray()[index.Value];
                        }
                    }
                    catch
                    { }
                }
                SeleniumDriver.SwitchTo().DefaultContent();
                throw new Exception($"table was not found");
            }
            finally
            {
                if (loggingWebdriver.Screenshots.Any())
                    Screenshots = loggingWebdriver.Screenshots.Select(x => x.AsByteArray).ToList();

            }
        }, TimeSpan.FromMilliseconds(SeleniumGridConfiguration.RetryMs));

        public IEnumerable<TableElement> GetTables(int minimumWidth)
        {
            var xpath = $"//table[" +
                $"tr[th[{minimumWidth}] or td[{minimumWidth}]] or " +
                $"tbody/tr[th[{minimumWidth}] or td[{minimumWidth}]] or " +
                $"thead/tr[th[{minimumWidth}] or td[{minimumWidth}]]" +
                $"]";
            //var xpath = $"//tr[*[self::td or self::th][{minimumWidth}] and (.|parent::tbody)[1]/parent::table]/ancestor::table[1]";
            var tables = SeleniumDriver.
                FindElements(By.XPath(xpath))
                .Select(t => new TableElement(t, SeleniumDriver, ElementFactory, XpathProvider)).ToList();
            var Ordinal = 1;
            foreach (var table in tables)
            {
                table.Ordinal = Ordinal++;
                table.Xpath = xpath;
            }
            tables.AsParallel().ForAll(table => table.Setup());
            return tables.Where(t=>t.IsValid).ToList();
        }

        private SeleniumGridConfiguration SeleniumGridConfiguration { get; }
        private RetryExecutor RetryExecutor { get; }
        private List<SelectorPrefix> Prefixes { get; }
        private List<byte[]> Screenshots { get; set; }
        private List<WebDriver> Children { get; set; }

        //TODO: check this form
        // I suspect this is an Async call, and there is no way to await :/
        public void NavigateTo(string url)
        {
            if (Uri.TryCreate(url, UriKind.Absolute, out var absolute))
                SeleniumDriver.Navigate().GoToUrl(url);
            else
                SeleniumDriver.Navigate().GoToUrl(RootUrl().AbsoluteUri + url);

            //safety, it is not it's job to catch this error
            try
            {
                Thread.Sleep(1000);
                WebDriverWait wait = new WebDriverWait(SeleniumDriver, new TimeSpan(0, 0, 5));
                wait.Until(x => x.FindElement(By.XPath("//body")));
            }
            catch { }
        }

        public void Close()
            => SeleniumDriver.Close();

        public void LeaveFrames()
            => SeleniumDriver.SwitchTo().DefaultContent();

        public Actions BuildAction()
            => new Actions(SeleniumDriver);

        public void LoadPage(string html)
            => SeleniumDriver.Navigate().GoToUrl("data:text/html;charset=utf-8," + html);

        public void SwitchToWindow(string window)
            => SeleniumDriver.SwitchTo().Window(window);

        public Element Select(Selector selector, TimeSpan? retryDuration = null, int? index = null)
            => RetryExecutor.RetryFor(() =>
             {
                 var loggingWebdriver = new LoggingWebDriver(SeleniumDriver, MovieLogger, WebElementSourceLog);
                 try
                 {
                     var element = FindElement(selector, loggingWebdriver, index);
                     if (element != null)
                         return element;
                     //iframes ? 
                     var iframes = SeleniumDriver.FindElements(By.XPath("//iframe"));
                     foreach (var iframe in iframes)
                     {
                         try
                         {
                             loggingWebdriver.Log($"Trying iframe:{iframe}");
                             SeleniumDriver.SwitchTo().Frame(iframe);
                             element = FindElement(selector, loggingWebdriver, index);
                             if (element != null)
                                 return element;
                         }
                         catch
                         { }
                     }
                     SeleniumDriver.SwitchTo().DefaultContent();
                     throw new Exception($"element was not found; tried:\n{loggingWebdriver.GetLogs()}, maybe try one of these identifiers {GetIdentifiers().Take(10).LogFormat()}");
                 }
                 finally
                 {
                     if (loggingWebdriver.Screenshots.Any())
                         Screenshots = loggingWebdriver.Screenshots.Select(x => x.AsByteArray).ToList();

                 }
             }, retryDuration ?? TimeSpan.FromMilliseconds(SeleniumGridConfiguration.RetryMs));

        public IEnumerable<Element> SelectMany(Selector selector)
            => RetryExecutor.RetryFor(() =>
            {
                var loggingWebdriver = new LoggingWebDriver(SeleniumDriver, MovieLogger, WebElementSourceLog);
                try
                {
                    var elements = FindElements(selector, loggingWebdriver);
                    if (elements != null)
                        return elements;
                    //iframes ? 
                    var iframes = SeleniumDriver.FindElements(By.XPath("//iframe"));
                    foreach (var iframe in iframes)
                    {
                        try
                        {
                            loggingWebdriver.Log($"Trying iframe:{iframe}");
                            SeleniumDriver.SwitchTo().Frame(iframe);
                            elements = FindElements(selector, loggingWebdriver);
                            if (elements != null)
                                return elements;
                        }
                        catch
                        { }
                    }
                    SeleniumDriver.SwitchTo().DefaultContent();
                    return new List<Element>();
                }
                finally
                {
                    if (loggingWebdriver.Screenshots.Any())
                        Screenshots = loggingWebdriver.Screenshots.Select(x => x.AsByteArray).ToList();

                }
            }, TimeSpan.FromMilliseconds(SeleniumGridConfiguration.RetryMs));

        public void DismissAlert()
            => SeleniumDriver.SwitchTo().Alert().Dismiss();

        public void AcceptAlert()
            => SeleniumDriver.SwitchTo().Alert().Accept();

        private class Wrapper
        {
            public IEnumerable<Element> Elements;
            public Element Element;
            public Searcher Searcher;
            public Exception Exception;
        }

        private Element FindElement(Selector selector, LoggingWebDriver loggingWebdriver, int? index = null)
        {
            var wrappers = selector.PrioritizedSearchers.Select(s => new Wrapper { Searcher = s }).ToList();
            var loopResults = Parallel.ForEach(wrappers, 
                //new ParallelOptions { MaxDegreeOfParallelism = 4 }, 
                (wrapper, loopState) =>
            {
                var searcher = wrapper.Searcher;

                var results = searcher.SearchIn(loggingWebdriver, Prefixes);
                if (loopState.ShouldExitCurrentIteration)
                    return;
                else if (results.One())
                {
                    SuccessfulSearchers.Add(searcher);
                    loopState.Break();
                    wrapper.Element = results.First();
                    return;
                }
                else if (results.Many() && index.HasValue)
                {
                    SuccessfulSearchers.Add(searcher);
                    loopState.Break();
                    var a = results.ToArray();
                    if (a.Count() <= index.Value)
                        wrapper.Exception = new Exception($"Not enough items found, found {a.Count()} and desired index {index}");
                    else
                        wrapper.Element = a[index.Value];
                    return;
                }
                else if (results.Many())
                {
                    //lets make sure none are hidden
                    var filterHidden = results
                        .Select(e => new { e, o = loggingWebdriver.GetElementFromPoint(e.Location.X + 1, e.Location.Y + 1) })
                        .Where(p => p.e.Tag == p.o?.TagName && p.e.Location == p.o?.Location);
                    if (filterHidden.One())
                    {
                        SuccessfulSearchers.Add(searcher);
                        loopState.Break();
                        wrapper.Element = results.First();
                        return;
                    }
                    //check if they are logical duplicates.
                    if (results.GroupBy(e => e.Id).One())
                    {
                        SuccessfulSearchers.Add(searcher);
                        loopState.Break();
                        wrapper.Element = results.First();
                        return;
                    }
                    //scroll up ?
                    //WebDriver.ExecuteScript("window.scrollTo(0,1)");
                    var items = results.Select(e => $"{e.Tag}@{e.Location.X},{e.Location.Y}").LogFormat();
                    loopState.Break();
                    wrapper.Exception = new Exception($"Multiple results were found using {searcher.LogFormat()}");
                    return;
                }
            });
            var r = loopResults.IsCompleted;
            var wrapperIndex = 0;
            foreach (var w in wrappers)
            {
                if (w.Element != null)
                {
                    AugmentWebElementSources(selector, w.Element.WebElement);
                    return w.Element;
                }
                if (w.Exception != null)
                    throw new AggregateException($"Error throw on xpath {wrapperIndex}", w.Exception);
                wrapperIndex++;
            }
            return null;
        }

        private void AugmentWebElementSources(Selector selector, IWebElement e)
        {
            var s = WebElementSourceLog.Get(e);
            if (s == null)
                return;
            s.SelectorType = selector.Type;
            s.SelectorConstructor = selector.Constructor;
        }

        private IEnumerable<Element> FindElements(Selector selector, LoggingWebDriver loggingWebdriver)
        {
            var wrappers = selector.PrioritizedSearchers.Select(s => new Wrapper { Searcher = s }).ToList();
            var loopResults = Parallel.ForEach(wrappers, (wrapper, loopState) =>
            {
                var searcher = wrapper.Searcher;
                var results = searcher.SearchIn(loggingWebdriver, Prefixes);

                SuccessfulSearchers.Add(searcher);
                loopState.Break();
                wrapper.Elements = results;
                return;
            });
            var r = loopResults.IsCompleted;
            var index = 0;
            foreach (var w in wrappers)
            {
                if (w.Elements != null)
                {
                    if (w.Elements.Any())
                    {
                        foreach (var e in w.Elements)
                        {
                            AugmentWebElementSources(selector, e.WebElement);
                        }
                    }
                    return w.Elements;
                }
                if (w.Exception != null)
                    throw new AggregateException($"Error throw on xpath {index}", w.Exception);
                index++;
            }
            return null;
        }

        virtual public List<string> GetIdentifiers()
        {
            var options = new List<Tuple<By, Func<IWebElement, string>, List<string>>>()
            {
                new Tuple<By, Func<IWebElement,string>, List<string>>(
                    By.XPath("//label"), (e)=>e.Text,  new List<string>()),
                new Tuple<By, Func<IWebElement,string>, List<string>>(
                    By.XPath("//*[self::button or self::a or (self::input and @type='button')]"), (e)=>e.Text, new List<string>()),
                new Tuple<By, Func<IWebElement,string>, List<string>>(
                    By.XPath("//*[@alt]"), (e)=>e.GetAttribute("alt"),  new List<string>()),
                new Tuple<By, Func<IWebElement,string>, List<string>>(
                    By.XPath("//*[@name]"), (e)=>e.GetAttribute("name"),  new List<string>()),
                new Tuple<By, Func<IWebElement,string>, List<string>>(
                    By.XPath("//*[@aria-label]"), (e)=>e.GetAttribute("aria-label"),  new List<string>()),
                new Tuple<By, Func<IWebElement,string>, List<string>>(
                    By.XPath("//*[@title]"), (e)=>e.GetAttribute("title"),  new List<string>()),
            };

            Parallel.ForEach(options, (option, loopState) =>
            {
                var elements = SeleniumDriver.FindElements(option.Item1);
                foreach (var e in elements)
                {
                    try
                    {
                        option.Item3.Add(option.Item2(e));
                    }
                    catch
                    { }
                }
            });

            return options.SelectMany(o => o.Item3).Distinct().OrderBy(s => s.ToLower()).ToList();
        }

        public void ExecuteScript(string script)
            => ((IJavaScriptExecutor)SeleniumDriver).ExecuteScript(script);

        public WebDriver Under(UnderSelectorPrefix under)
            => Prefix(under);

        public WebDriver ForRow(RowSelectorPrefix row)
            => Prefix(row);

        public WebDriver ForError()
            => Prefix(SelectorFactory.CreatePrefix<ErrorSelectorPrefix>());

        public WebDriver ForWarning()
            => Prefix(SelectorFactory.CreatePrefix<WarningSelectorPrefix>());

        public WebDriver Prefix(SelectorPrefix prefix)
        {
            var p = new ValidatedPrefix();
            var l = Prefixes.Concat(prefix);

            var possibles = l.CrossMultiply(); 
            RetryExecutor.RetryFor(() =>
               {
                   var iframes = SeleniumDriver.FindElements(By.XPath("//iframe"));
                   var valid = possibles.AsParallel().AsOrdered().Where(xpath => SeleniumDriver.FindElements(By.XPath(xpath)).Any()).ToList();
                   if (valid.Any())
                       p.Init("filtered", valid);
                   else if (iframes.Any())
                   {
                       foreach (var iframe in iframes)
                       {
                           try
                           {
                               SeleniumDriver.SwitchTo().Frame(iframe);
                               valid = possibles.AsParallel().AsOrdered().Where(xpath => SeleniumDriver.FindElements(By.XPath(xpath)).Any()).ToList();
                               if (valid.Any())
                                   p.Init("filtered", valid);
                           }
                           catch
                           { }
                       }
                       SeleniumDriver.SwitchTo().DefaultContent();
                   }
                   if(!p.IsInitialized)
                       throw new Exception($"Was unable to find any that matched prefix, tried:{possibles.LogFormat()}");
               }, TimeSpan.FromMilliseconds(SeleniumGridConfiguration.RetryMs));

            var wdm = new WebDriver(
                SeleniumDriver,
                RootUrl,
                SeleniumGridConfiguration,
                RetryExecutor,
                SelectorFactory,
                ElementFactory,
                XpathProvider,
                MovieLogger,
                WebElementSourceLog,
                new List<SelectorPrefix> { p }
                );

            Children.Add(wdm);
            return wdm;
        }

        public IEnumerable<byte[]> GetScreenshots()
        {
            foreach (var c in Children)
                foreach (var s in c.GetScreenshots())
                    yield return s;
            foreach (var s in Screenshots)
                yield return s;
            byte[] data = null;
            try
            {
                data = ((ITakesScreenshot)SeleniumDriver).GetScreenshot().AsByteArray;
            }
            catch (OpenQA.Selenium.UnhandledAlertException) { }
            catch (OpenQA.Selenium.NoSuchWindowException) { }
            if (data != null)
                yield return data;
        }

        public void ResetScreenshots()
        {
            Children = new List<WebDriver>();
            Screenshots = new List<byte[]>();
        }

        public string LogFormat() => Url;

        public string PageSource { get => SeleniumDriver.PageSource; }
        public string Url { get => SeleniumDriver.Url; }
        public IEnumerable<string> Windows { get => SeleniumDriver.WindowHandles; }
        public string AlertText
        {
            get
            {
                try
                {
                    return SeleniumDriver.SwitchTo().Alert().Text;

                }
                catch
                {
                    return null;
                }
            }
        }

        public Size Size
        {
            get => SeleniumDriver.Manage().Window.Size;
            set => SeleniumDriver.Manage().Window.Size = value;
        }
        public bool HasAlert
        {
            get {
                try {
                    SeleniumDriver.SwitchTo().Alert();
                    return true;
                }
                catch {
                    return false;
                }
            }
        }

        IWebDriver IWebDriverWrapper.IWebDriver => SeleniumDriver;

        public bool Disposed { get; set; }

        public string BrowserLogs
        {
            get
            {
                var sb = new StringBuilder();
                var logs = SeleniumDriver.Manage().Logs;
                var test = logs.GetLog(LogType.Browser);

                foreach (var t in logs.AvailableLogTypes)
                    foreach (var l in logs.GetLog(t))
                        sb.AppendLine($"{l.Message}");

                var formatted = logs.AvailableLogTypes.SelectMany(t => logs.GetLog(t).Select(x => new { type = t, log = x }))
                    .OrderBy(x => x.log.Timestamp)
                    .Select(x => $"{x.type}:{x.log.Timestamp}:{x.log.Message}")
                    .Aggregate((x, y) => x + "\n" + y);
                return formatted;
            }
        }

    }
}

