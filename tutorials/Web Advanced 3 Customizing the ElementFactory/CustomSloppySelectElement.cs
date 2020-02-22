using OpenQA.Selenium;
using PossumLabs.DSL.Web;
using System;
using System.Collections.Generic;
using System.Text;

namespace DSL.Documentation.Example
{
    /// <summary>
    /// this select is helped by a custom attribute (select-helper) on options
    /// this would only work if your application actually supported this specific
    /// custom attribute. 
    /// </summary>
    public class CustomSloppySelectElement : SloppySelectElement
    {
        public CustomSloppySelectElement(IWebElement element, IWebDriver driver) : base(element, driver)
        {
        }

        protected override System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> FindByContains(string id, string key)
            => base.WebDriver.FindElements(
                By.XPath($"//select[@id='{id}']/option[contains(" +
                $"translate(@value,'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ'), '{key}') or contains(" +
                $"translate(@select-helper,'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ'), '{key.Replace(" ", "")}') or contains(" +
                $"translate(text(),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ'), '{key}')]"));

        protected override System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> FindByExactMatch(string id, string key)
            => base.WebDriver.FindElements(
                By.XPath($"//select[@id='{id}']/option[" +
                $"translate(@value,'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ') ='{key}' or " +
                $"translate(@select-helper,'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ') ='{key.Replace(" ", "")}' or " +
                $"translate(text(),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ') = '{key}']"));
    }
}
