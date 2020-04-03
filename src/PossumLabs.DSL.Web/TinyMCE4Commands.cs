using OpenQA.Selenium;
using System.Text.RegularExpressions;

namespace PossumLabs.DSL.Web
{
    public class TinyMCE4Commands : IRichTextEditorCommandSet
    {
        /// TinyMCE 4.5.5 (2017-03-07)
        ///<div id="mceu_11" 
        /// class="mce-tinymce mce-container mce-panel"
        /// hidefocus="1" tabindex="-1"
        /// role="application"
        /// style="visibility: hidden; border-width: 1px;">

        public void Clear(string textareaId, IWebDriver driver)
            => ((IJavaScriptExecutor)driver)
                .ExecuteScript($"tinymce.get('{textareaId}').setContent('')");

        public string Get(string textareaId, IWebDriver driver)
            => ((IJavaScriptExecutor)driver)
                .ExecuteScript($"return tinymce.get('{textareaId}').getContent()").ToString();

        public bool IsClassMatch(string s)
            => new Regex("^(.* )?mce-tinymce( .*)?$").IsMatch(s);

        public void Set(string textareaId, IWebDriver driver, string s)
            => ((IJavaScriptExecutor)driver)
                .ExecuteScript($"tinymce.get('{textareaId}').setContent(arguments[0])", s);
    }
}
