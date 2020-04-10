using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using PossumLabs.DSL.Core;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Remote;
using System.Drawing;
using System.Linq;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Interactions;
using PossumLabs.DSL.Core.Logging;
using System.Diagnostics;
using PossumLabs.DSL.Web.Selectors;
using static PossumLabs.DSL.Web.Selectors.WebElementSourceLog;

namespace PossumLabs.DSL.Web
{
#pragma warning disable CS0618 // Type or member is obsolete, 3rd party reference
    public class LoggingWebDriver : IWebDriver, ITakesScreenshot, IActionExecutor, IJavaScriptExecutor
#pragma warning restore CS0618 // Type or member is obsolete
    {
        public LoggingWebDriver(IWebDriver driver, 
            IMovieLogger movieLogger, 
            IWebElementSourceLog webElementSourceLog)
        {
            SeleniumDriver = driver;
            Messages = new List<string>();
            Screenshots = new List<Screenshot>();
            MovieLogger = movieLogger;
            WebElementSourceLog = webElementSourceLog;
        }

        private List<string> Messages { get; }
        private IMovieLogger MovieLogger { get; }
        public List<Screenshot> Screenshots { get; }
        public IWebElementSourceLog WebElementSourceLog { get; set; }

        public string Url { get => SeleniumDriver.Url; set => SeleniumDriver.Url = value; }

        public string Title => SeleniumDriver.Title;

        public string PageSource => SeleniumDriver.PageSource;

        public string CurrentWindowHandle => SeleniumDriver.CurrentWindowHandle;

        public ReadOnlyCollection<string> WindowHandles => SeleniumDriver.WindowHandles;

        private IActionExecutor ActionExecutor => (IActionExecutor)SeleniumDriver;
        public bool IsActionExecutor => ActionExecutor.IsActionExecutor;

        private IWebDriver SeleniumDriver;
        public string GetLogs() => Messages.Where(x => !string.IsNullOrEmpty(x)).LogFormat();

        public void Close() => SeleniumDriver.Close();

        public void Quit() => SeleniumDriver.Quit();

        public IOptions Manage() => SeleniumDriver.Manage();

        public INavigation Navigate() => SeleniumDriver.Navigate();

        public ITargetLocator SwitchTo() => SeleniumDriver.SwitchTo();

        public IWebElement FindElement(By by)
        {
            Messages.Add(by.ToString());
            var element = SeleniumDriver.FindElement(by);
            if (element != null)
            {
                if (by.ToString().StartsWith("By.XPath: "))
                    VisualLog(by);
                var url = SeleniumDriver.Url;
                WebElementSourceLog.Add(element, url, by);
            }
            return element;
        }

        private IJavaScriptExecutor ScriptExecutor
        {
            get
            {
                var scriptExecutor = SeleniumDriver as IJavaScriptExecutor;
                if (scriptExecutor == null)
                    throw new Exception("this webdriver does not support script executon.");

                return scriptExecutor;
            }
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            lock (Messages)
            {
                Messages.Add(by.ToString());
            }
            var elements = SeleniumDriver.FindElements(by);
            if (elements != null && elements.Any())
            {
                var url = SeleniumDriver.Url;
                if (by.IsXpath())
                    VisualLog(by);
                foreach (var e in elements)
                    WebElementSourceLog.Add(e, url, by);
            }
            return elements;
        }

        private void VisualLog(By by)
        {
            if (MovieLogger.IsEnabled)
            {
                var xpath = by.Xpath();
                var img = Preview(xpath);
                MovieLogger.AddScreenShot(img.AsByteArray);
                Screenshots.Add(img);
            }
        }

        public void Log(string message)
        {
            lock (Messages)
            {
                Messages.Add(message);
            }
        }

        public void Dispose() => SeleniumDriver.Dispose();

        public Screenshot GetScreenshot()
            => ((ITakesScreenshot)SeleniumDriver).GetScreenshot();

        //HACK: wrong place for this code
        private Screenshot Preview(string xpath)
        {
            var id = "data-possum-labs-"+Guid.NewGuid().ToString();
            //add style
            String s_Script = @"
var css = new function()
{
    function addStyleSheet()
    {
        let head = document.head;
        let style = document.createElement('style');

        head.appendChild(style);
    }

    this.insert = function(rule)
    {
        if(document.styleSheets.length == 0) { 
            addStyleSheet(); }
        if (!document.styleSheets[document.styleSheets.length - 1].hasOwnProperty('rules')) { 
            addStyleSheet(); }

        let sheet = document.styleSheets[document.styleSheets.length - 1];
        let rules = sheet.rules;

        sheet.insertRule(rule, rules.length);
    }
}

css.insert( '*['+arguments[0]+'] { outline: DarkOrange dashed 2px; }');
";
            ScriptExecutor.ExecuteScript(s_Script, id);

            //mark items

            s_Script = @"
var nodesSnapshot = document.evaluate(arguments[1], document, null, XPathResult.ORDERED_NODE_SNAPSHOT_TYPE, null);

for (var i=0 ; i<nodesSnapshot.snapshotLength; i++ )
{
  nodesSnapshot.snapshotItem(i).setAttribute(arguments[0], i);
}
";
            ScriptExecutor.ExecuteScript(s_Script, id, xpath);
            var screenshot = GetScreenshot();
            //remove style
            s_Script = @"
let sheet = document.styleSheets[document.styleSheets.length - 1];
if (sheet.hasOwnProperty('rules')) { 
    let index = sheet.rules.length-1;
    sheet.deleteRule(index); 
}
";
            ScriptExecutor.ExecuteScript(s_Script, id);
            //unmark
            s_Script = @"
var nodesSnapshot = document.evaluate('//*[@'+arguments[0]+']', document, null, XPathResult.ORDERED_NODE_SNAPSHOT_TYPE, null);

for (var i=0 ; i<nodesSnapshot.snapshotLength; i++ )
{
  nodesSnapshot.snapshotItem(i).removeAttribute(arguments[0]);
}
";
            ScriptExecutor.ExecuteScript(s_Script, id);
            return screenshot;
        }
            
            
            
        /// <summary>
        /// Get the element at the viewport coordinates X, Y
        /// </summary>
        public RemoteWebElement GetElementFromPoint(int X, int Y)
        {
            while (true)
            {
                String s_Script = "return document.elementFromPoint(arguments[0], arguments[1]);";

                RemoteWebElement i_Elem = ScriptExecutor.ExecuteScript(s_Script, X, Y) as RemoteWebElement;
                if (i_Elem == null)
                    return null;

                if (i_Elem.TagName != "frame" && i_Elem.TagName != "iframe")
                    return i_Elem;

                Point p_Pos = GetElementPosition(i_Elem);
                X -= p_Pos.X;
                Y -= p_Pos.Y;

                SeleniumDriver.SwitchTo().Frame(i_Elem);
            }
        }

        //HACK: nested IFrames
        /// <summary>
        /// Get the position of the top/left corner of the Element in the document.
        /// NOTE: RemoteWebElement.Location is always measured from the top of the document and ignores the scroll position.
        /// </summary>
        public Point GetElementPosition(RemoteWebElement i_Elem)
        {
            String s_Script = "var X, Y; "
                            + "if (window.pageYOffset) " // supported by most browsers 
                            + "{ "
                            + "  X = window.pageXOffset; "
                            + "  Y = window.pageYOffset; "
                            + "} "
                            + "else " // Internet Explorer 6, 7, 8
                            + "{ "
                            + "  var  Elem = document.documentElement; "         // <html> node (IE with DOCTYPE)
                            + "  if (!Elem.clientHeight) Elem = document.body; " // <body> node (IE in quirks mode)
                            + "  X = Elem.scrollLeft; "
                            + "  Y = Elem.scrollTop; "
                            + "} "
                            + "return new Array(X, Y);";

            IList<Object> i_Coord = (IList<Object>)ScriptExecutor.ExecuteScript(s_Script);

            int s32_ScrollX = Convert.ToInt32(i_Coord[0]);
            int s32_ScrollY = Convert.ToInt32(i_Coord[1]);

            return new Point(i_Elem.Location.X - s32_ScrollX,
                             i_Elem.Location.Y - s32_ScrollY);

        }

        public void PerformActions(IList<ActionSequence> actionSequenceList)
        => ActionExecutor.PerformActions(actionSequenceList);

        public void ResetInputState()
        => ActionExecutor.ResetInputState();

        public void ScriptClear(IWebElement e)
            => ScriptExecutor.ScriptClear(e);
        public void ScriptSet(IWebElement e, string val)
            => ScriptExecutor.ScriptSet(e, val);

        object IJavaScriptExecutor.ExecuteScript(string script, params object[] args)
            => ScriptExecutor.ExecuteScript(script, args);

        object IJavaScriptExecutor.ExecuteAsyncScript(string script, params object[] args)
            => ScriptExecutor.ExecuteAsyncScript(script, args);

       
    }
}
