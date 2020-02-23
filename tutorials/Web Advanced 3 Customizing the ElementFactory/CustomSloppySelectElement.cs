using OpenQA.Selenium;
using PossumLabs.DSL.Web;
using System;
using System.Collections.Generic;
using System.Text;

namespace DSL.Documentation.Example
{
    /// <summary>
    /// this select is helped by a custom attribute (custom-option-identifier) on options
    /// this would only work if your application actually supported this specific
    /// custom attribute. 
    /// </summary>
    public class CustomSloppySelectElement : SloppySelectElement
    {
        public CustomSloppySelectElement(IWebElement element, IWebDriver driver) : base(element, driver)
        {
        }

        /// <summary>
        /// Soemtimes select options are not friendly for DSL langauge, eitehr due tot ranslation or beacuase they
        /// are icons or images
        /// supports html like 
        /// <select>
        ///    <option value="1" custom-option-identifier="default"><img src="default.jpg"></option>
        ///    <option value="2" custom-option-identifier="special"><img src="specail.jpg"></option>
        /// </select>
        /// We can now add an extra "translate(@custom-option-identifier,'...', '...') etc."
        /// to help us set the selector to the right version.
        /// 
        /// the "translate(...,'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')"
        /// helps compare as if case insensitive by uppercasing the a-z characters, the 
        /// sloppy select provider will uppercase the "key"
        /// </summary>
        protected override System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> FindByContains(string id, string key)
            => base.WebDriver.FindElements(
                By.XPath($"//select[@id='{id}']/option[contains(" +
                $"translate(@value,'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ'), '{key}') or contains(" +
                $"translate(@custom-option-identifier,'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ'), '{key}') or contains(" +
                $"translate(text(),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ'), '{key}')]"));

        protected override System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> FindByExactMatch(string id, string key)
            => base.WebDriver.FindElements(
                By.XPath($"//select[@id='{id}']/option[" +
                $"translate(@value,'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ') ='{key}' or " +
                $"translate(@custom-option-identifier,'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ') ='{key}' or " +
                $"translate(text(),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ') = '{key}']"));
    }
}
