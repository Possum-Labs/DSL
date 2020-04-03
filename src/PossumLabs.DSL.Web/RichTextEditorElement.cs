using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PossumLabs.DSL.Web
{
    public class RichTextEditorElement : Element
    {
        ///https://yizeng.me/2014/01/31/test-wysiwyg-editors-using-selenium-webdriver/
        ///
        /// https://ckeditor.com/docs/ckeditor4/latest/guide/dev_installation.html#using-the-cdn
        /// https://www.tiny.cloud/docs/quick-start/
        /// https://www.tiny.cloud/docs/api/tinymce/tinymce.editor/
   
        public RichTextEditorElement(IWebElement element, IWebDriver driver) : base(element, driver)
        {
            TextareaId = element.GetAttribute("id");
            var elements = driver.FindElements(By.XPath(
                $"//textarea[@id='{TextareaId}']" +
                $"/following-sibling::div[@role='application']"));
            if (elements.Any())
                Application = elements.First();
            else
                Application = driver.FindElement(By.XPath(
                $"//textarea[@id='{TextareaId}']" +
                $"/preceding-sibling::div[@role='application']"));

            var classAttribute = Application.GetAttribute("class");
            foreach(var c in RichTextEditorCommandSets)
            {
                if (c.IsClassMatch(classAttribute))
                {
                    Commands = c;
                    return;
                }
            }
            throw new Exception(
                $"the application element is not supported with class {classAttribute}.");
        }

        public string TextareaId { get; }
        public IWebElement Application { get; }

        static RichTextEditorElement()
        {
            RichTextEditorCommandSets = new List<IRichTextEditorCommandSet>()
            {
                new CKE4Commands(),
                new CKE5Commands(),
                new TinyMCE4Commands(),
                new TinyMCE5Commands()
            };
        }

        private static  List<IRichTextEditorCommandSet> RichTextEditorCommandSets { get; }
        private IRichTextEditorCommandSet Commands { get; }

        public override void Clear()
            => Commands.Clear(TextareaId, WebDriver);

        public override void Enter(string text)
            => Commands.Set(TextareaId, WebDriver, text);

        public override string Value 
            => Commands.Get(TextareaId, WebDriver);

        public override List<string> Values 
            => new List<string> { Value };
    }
}
