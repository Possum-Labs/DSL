using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace PossumLabs.DSL.Web
{
    public class CheckboxElement : Element
    {
        public CheckboxElement(IWebElement element, IWebDriver driver) : base(element, driver)
        {

        }

        public override void Enter(string text)
        {
            if (WebElement.Selected)
            {
                if (IsProbablychecked(text))
                    noop();
                else
                    WebElement.Click();
            }
            else
            {
                if (IsProbablychecked(text))
                    WebElement.Click();
                else
                    noop();
            }
        }

        private bool IsProbablychecked(string text)
            =>
            string.Equals(text, "checked", StringComparison.InvariantCultureIgnoreCase) ||
            string.Equals(text, "check", StringComparison.InvariantCultureIgnoreCase) ||
            string.Equals(text, "true", StringComparison.InvariantCultureIgnoreCase);


        public override List<string> Values => new List<string>
        {
            WebElement.Selected.ToString(),
            WebElement.Selected?"checked":"unchecked"
        };

        //Do nothing, handy for if branches
        private void noop()
        {

        }
    }
}
