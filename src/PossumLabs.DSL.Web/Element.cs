using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PossumLabs.DSL.Core.Validations;
using System.Drawing;
using PossumLabs.DSL.Core;
using OpenQA.Selenium.Interactions;
using System.Text.RegularExpressions;
using System.Threading;

namespace PossumLabs.DSL.Web
{
    public class Element
    {
        public IWebElement WebElement { get; }
        protected IWebDriver WebDriver { get; }

        public Element(IWebElement element, IWebDriver driver)
        {
            WebElement = element;
            WebDriver = driver;
        }

        public string Tag => WebElement.TagName;
        public Point Location => WebElement.Location;
        public IEnumerable<string> Classes => WebElement.GetAttribute("class").Split(' ').Select(x => x.Trim());
        public string Id => WebElement.GetAttribute("id");
        public virtual List<string> Values => new List<string>() { WebElement.GetAttribute("value"), WebElement.Text };
        public virtual string Value => WebElement.GetAttribute("value") ?? WebElement.Text;

        public string GetAttribute(string name) => WebElement.GetAttribute(name);

        public bool Displayed => WebElement.Displayed;

        public System.Drawing.Size Size { get => WebElement.Size; }
        public bool IsEnabled { get => WebElement.Enabled; }

        public void Select()
            => WebElement.Click();

        //https://www.grazitti.com/resources/articles/automating-different-input-fields-using-selenium-webdriver/

  
        public virtual void Enter(string text)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                var tag = WebElement.TagName;
                var type = WebElement.GetAttribute("type");
                // optimisting setting
                if (type != "file")
                {
                    WebElement.Click();
                    Thread.Sleep(100);
                }
                WebElement.SendKeys(text);
                if (WebElement.GetAttribute("type") == "file" || Equivalent(WebElement.GetAttribute("value"), text))
                    return;
            }

            // clear and set
            try
            {
                WebElement.Clear();
            }
            catch { }
            if (string.IsNullOrWhiteSpace(text))
            {
                if (!Equivalent(WebElement.GetAttribute("value"), text))
                {
                    ScriptClear();
                    Thread.Sleep(100);
                    if (!Equivalent(WebElement.GetAttribute("value"), text))
                        throw new Exception($"failed clear element, was left with '{WebElement.GetAttribute("value")}'");
                }
                return;
            }
            try
            {
                //TODO: v2 Check Boxes
                WebElement.Click();
                Thread.Sleep(100);
                WebElement.SendKeys(text);
                if (WebElement.GetAttribute("type") == "file" || Equivalent(WebElement.GetAttribute("value"), text))
                    return;
            }
            catch (WebDriverException e)
            {
                try
                {
                    if (WebElement.GetAttribute("readonly") == "true")
                        throw new InvalidOperationException("element is readonly");
                }
                catch(InvalidOperationException ex)
                {
                    throw ex;
                }
                catch
                {
                    throw e;
                }
            }
            try
            {
                WebElement.Clear();
            }
            catch { }
            WebElement.Click(); //Works with date time elements require formatting
            Thread.Sleep(100);
            foreach (var key in text)
            {
                Thread.Sleep(10);
                WebElement.SendKeys(key.ToString());
            }
            if (!Equivalent(WebElement.GetAttribute("value"), text) && WebElement.GetAttribute("type") != "file")
            {
                Thread.Sleep(1000);
                if (!Equivalent(WebElement.GetAttribute("value"), text))
                    ScriptSet(text);

                Thread.Sleep(100);
                if (!Equivalent(WebElement.GetAttribute("value"), text))
                    throw new Exception($"failed setting element, desired '{text}' got '{WebElement.GetAttribute("value")}'");
            }
        }

        private IJavaScriptExecutor ScriptExecutor
        {
            get
            {
                var scriptExecutor = WebDriver as IJavaScriptExecutor;
                if (scriptExecutor == null)
                    throw new Exception("this webdriver does not support script executon.");

                return scriptExecutor;
            }
        }

        public void ScriptClear()
            => ScriptExecutor.ScriptClear(WebElement);

        public void ScriptSet( string val)
             => ScriptExecutor.ScriptSet(WebElement, val);

        protected bool Equivalent(string actual, string desired)
        {
            if (string.IsNullOrWhiteSpace(actual) && string.IsNullOrWhiteSpace(desired))
                return true;
            Regex rgx = new Regex("[^a-zA-Z0-9]");
            if (string.IsNullOrWhiteSpace(rgx.Replace(actual, "")) && string.IsNullOrWhiteSpace(desired))
                return true;
            return rgx.Replace(actual, "").ToUpper() == rgx.Replace(desired,"").ToUpper();
        }

        public void Click()
            => WebElement.Click();


        public string GetCssValue(string prop)
            => WebElement.GetCssValue(prop);

        public void DoubleClick()
        {
            var builder = new Actions(WebDriver);
            var action = builder.DoubleClick(WebElement);
            action.Build().Perform();
        }

        public virtual void Clear()
        => WebElement.Clear();

    }
}
