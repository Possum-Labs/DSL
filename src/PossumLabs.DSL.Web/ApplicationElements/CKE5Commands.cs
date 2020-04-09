using OpenQA.Selenium;
using System.Text.RegularExpressions;

namespace PossumLabs.DSL.Web.ApplicationElements
{
    // not supported untill we resolve 
    // https://github.com/ckeditor/ckeditor5/issues/6554
    public class CKE5Commands : IApplicationCommandSet
    {
        /// CKE 5
        /// <div class="ck ck-reset ck-editor ck-rounded-corners"
        /// role="application" dir="ltr" lang="en"
        /// aria-labelledby="ck-editor__label_ea6a21cc8d14e654b20a2009339039144">

        public void Clear(string textareaId, IWebDriver driver)
            => ((IJavaScriptExecutor)driver)
                .ExecuteScript("CKEDITOR.instances.ckeditor.setData('')");

        public string Get(string textareaId, IWebDriver driver)
            => ((IJavaScriptExecutor)driver)
                .ExecuteScript("return CKEDITOR.instances.ckeditor.getData()").ToString();

        public bool IsClassMatch(string s)
            => new Regex("^(.* )?ck-editor( .*)?$").IsMatch(s);

        public void Set(string textareaId, IWebDriver driver, string s)
            => ((IJavaScriptExecutor)driver)
                .ExecuteScript("CKEDITOR.instances.ckeditor.setData(arguments[0])", s);
    }
}
