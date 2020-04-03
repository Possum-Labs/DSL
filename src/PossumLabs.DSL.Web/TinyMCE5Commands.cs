using OpenQA.Selenium;
using System.Text.RegularExpressions;

namespace PossumLabs.DSL.Web
{
    public class TinyMCE5Commands : IRichTextEditorCommandSet
    {
        /// TinyMCE 5
        /// <div role="application" class="tox tox-tinymce"
        /// style="visibility: hidden; height: 200px;">

        public void Clear(string textareaId, IWebDriver driver)
            => ((IJavaScriptExecutor)driver)
                .ExecuteScript($"tinymce.get('{textareaId}').setContent('')");

        public string Get(string textareaId, IWebDriver driver)
            => ((IJavaScriptExecutor)driver)
                .ExecuteScript($"return tinymce.get('{textareaId}').getContent()").ToString();

        public bool IsClassMatch(string s)
            => new Regex("^(.* )?tox-tinymce( .*)?$").IsMatch(s);

        public void Set(string textareaId, IWebDriver driver, string s)
            => ((IJavaScriptExecutor)driver)
                .ExecuteScript($"tinymce.get('{textareaId}').setContent(arguments[0])", s);
    }
}
