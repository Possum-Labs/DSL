using OpenQA.Selenium;
using PossumLabs.DSL.Web;
using PossumLabs.DSL.Web.Selectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSL.Documentation.Example
{
    public class SpecializedElementFactory: ElementFactory
    {
        override public Element Create(IWebDriver driver, IWebElement e)
        {
            if (e.TagName == "select" || (e.TagName == "input" && !string.IsNullOrEmpty(e.GetAttribute("list"))))
                return new SloppySelectElement(e, driver);
            return base.Create(driver, e);
        }
    }
}
