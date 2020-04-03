using OpenQA.Selenium;
using System.Text.RegularExpressions;

namespace PossumLabs.DSL.Web
{
    public class CKE4Commands : IRichTextEditorCommandSet
    {
        /// CKE 4
        ///<div id = "cke_editor1"
        /// class="cke_1 cke cke_reset cke_chrome cke_editor_editor1 cke_ltr cke_browser_webkit cke_hidpi"
        /// dir="ltr" lang="en" role="application"
        /// aria-labelledby="cke_editor1_arialbl">}

        public void Clear(string textareaId, IWebDriver driver)
            => ((IJavaScriptExecutor)driver)
                .ExecuteScript($"CKEDITOR.instances['{textareaId}'].setData('')");

        public string Get(string textareaId, IWebDriver driver)
            => ((IJavaScriptExecutor)driver)
                .ExecuteScript($"return CKEDITOR.instances['{textareaId}'].getData()").ToString();

        public bool IsClassMatch(string s)
            => new Regex("^(.* )?cke( .*)?$").IsMatch(s);

        public void Set(string textareaId, IWebDriver driver, string s)
            => ((IJavaScriptExecutor)driver)
                .ExecuteScript($"CKEDITOR.instances['{textareaId}'].setData(arguments[0])", s);
    }
}
