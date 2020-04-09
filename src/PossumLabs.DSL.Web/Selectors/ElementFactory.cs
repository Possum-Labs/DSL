using OpenQA.Selenium;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using PossumLabs.DSL.Web.ApplicationElements;

namespace PossumLabs.DSL.Web.Selectors
{
    public class ElementFactory
    {
        public ElementFactory(ApplicationElementRegistry applicationElementRegistry)
        {
            ApplicationElementRegistry = applicationElementRegistry;
        }
        public ApplicationElementRegistry ApplicationElementRegistry { get; }

        virtual public Element Create(IWebDriver driver, IWebElement e)
        {
            if ((e.TagName == "textarea" || e.TagName == "input") && !e.Displayed)
            {
                var app = ApplciationElementFactory(driver, e);
                if (app != null)
                    return app;
                else
                    return new Element(e, driver); // must be some fetch by id
            }
            if (e.TagName == "select" || (e.TagName == "input" && !string.IsNullOrEmpty(e.GetAttribute("list"))))
            {
                return new SelectElement(e, driver);
            }
            if (e.TagName == "input" && e.GetAttribute("type") == "radio")
            {
                return RadioElementFactory(driver, e);
            }
            if (e.TagName == "input" && e.GetAttribute("type") == "checkbox")
            {
                return new CheckboxElement(e, driver);
            }

            return new Element(e, driver);
        }

        private static Element RadioElementFactory(IWebDriver driver, IWebElement e)
        {
            var elements = driver.FindElements(By.XPath($"//input[@type='radio' and @name='{e.GetAttribute("name")}']")).ToList();
            var value = e.GetAttribute("value");
            var first = elements.Where(x => x.GetAttribute("value") == value);
            return new RadioElement(first.Concat(elements.Except(first)), driver);
        }

        private Element ApplciationElementFactory(IWebDriver driver, IWebElement e)
        {
            var id = e.GetAttribute("id");
            IWebElement application;
            var elements = driver.FindElements(By.XPath(
                $"//textarea[@id='{id}']" +
                $"/following-sibling::div[@role='application']"));
            if (elements.Any())
                application = elements.First();
            else
                application = driver.FindElements(By.XPath(
                $"//textarea[@id='{id}']" +
                $"/preceding-sibling::div[@role='application']")).FirstOrDefault();
            if (application == null)
                return null;

            var classAttribute = application.GetAttribute("class");
            foreach (var c in ApplicationElementRegistry.ApplicationCommandSets)
            {
                if (c.IsClassMatch(classAttribute))
                {
                    return new ApplicationElement(e, driver, id, c);
                }
            }
            
            throw new Exception(
                $"the application element is not supported with class {classAttribute}.");
        }
    }
}
