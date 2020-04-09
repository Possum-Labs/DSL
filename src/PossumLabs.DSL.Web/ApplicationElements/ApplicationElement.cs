using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PossumLabs.DSL.Web.ApplicationElements
{
    public class ApplicationElement : Element
    {
        public ApplicationElement(
            IWebElement element, 
            IWebDriver driver,
            string replacedElementId,
            IApplicationCommandSet applicationCommandSet
            ) : base(element, driver)
        {
            ReplacedElementId = replacedElementId;
            Commands = applicationCommandSet;
        }

        public string ReplacedElementId { get; }
        public IWebElement Application { get; }

        private IApplicationCommandSet Commands { get; }

        public override void Clear()
            => Commands.Clear(ReplacedElementId, WebDriver);

        public override void Enter(string text)
            => Commands.Set(ReplacedElementId, WebDriver, text);

        public override string Value 
            => Commands.Get(ReplacedElementId, WebDriver);

        public override List<string> Values 
            => new List<string> { Value };
    }
}
