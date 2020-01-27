using OpenQA.Selenium;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Web.Selectors
{
    public class ElementFactory
    {
        virtual public Element Create(IWebDriver driver, IWebElement e)
        {
            if (e.TagName == "select" || (e.TagName == "input" && !string.IsNullOrEmpty(e.GetAttribute("list"))))
                return new SelectElement(e, driver);
            if (e.TagName == "input" && e.GetAttribute("type") == "radio")
            {
                var elements = driver.FindElements(By.XPath($"//input[@type='radio' and @name='{e.GetAttribute("name")}']")).ToList();
                var value = e.GetAttribute("value");
                var first = elements.Where(x => x.GetAttribute("value") == value);
                return new RadioElement(first.Concat(elements.Except(first)), driver);
            }
            if (e.TagName == "input" && e.GetAttribute("type") == "checkbox")
            {
                return new CheckboxElement(e, driver);
            }
            return new Element(e, driver);
        }
    }
}
