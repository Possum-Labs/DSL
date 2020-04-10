using OpenQA.Selenium;
using PossumLabs.DSL.Web;
using PossumLabs.DSL.Web.ApplicationElements;
using PossumLabs.DSL.Web.Selectors;
using System;
using System.Collections.Generic;
using System.Text;

namespace DSL.Documentation.Example
{
    /// <summary>
    /// this is where you can controll the logic of the elements, in this case we are creating a special
    /// select element.
    /// </summary>
    ///
    ///Please make sure to look at FrameworkInitializationSteps to see where we register this class
    public class CustomElementFactory : ElementFactory
    {
        public CustomElementFactory(ApplicationElementRegistry applicationElementRegistry)
            :base(applicationElementRegistry)
        {
        }

        /// <summary>
        /// here you specify when to use your specialized element vs. the default element. This one requires a select
        /// or an input that has the list attribute.
        /// </summary>
        override public Element Create(IWebDriver driver, IWebElement e)
        {
            var list = e.GetAttribute("list");
            var id = e.GetAttribute("id");
            var tag = e.TagName;
            var location = e.Location;
            if (e.TagName == "select" || (e.TagName == "input" && !string.IsNullOrEmpty(list)))
                return new CustomSloppySelectElement(e, driver);
            return base.Create(driver, e);
        }
    }

}
