using PossumLabs.DSL.Core.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PossumLabs.DSL.Web.Integration
{
    public class HtmlInjection : IValueObject
    {
        public string Content { get; set; }

        public string Script { set
            {
                Content = Content.Replace("<script-token>", $"<script>{value}</script>");
            }
        }

        public string Html
        {
            set
            {
                Content = $"<html><head><script-token></head><body>{value}</body></html>";
            }
        }
        public string HtmlBootStrap
        {
            set
            {
                Content = $"<html><head></head><body>{value}" +
                    @"<script-token>"+
                    @"<script type=""text/javascript"" src=""http://cdnjs.cloudflare.com/ajax/libs/jquery/2.1.3/jquery.min.js""></script>" +
                    @"<script type=""text/javascript"" src=""http://maxcdn.bootstrapcdn.com/bootstrap/3.3.2/js/bootstrap.min.js""></script>" +
                    @"<script type=""text/javascript"" src=""http://cdnjs.cloudflare.com/ajax/libs/moment.js/2.9.0/moment-with-locales.min.js""></script>" +
                    @"<script type=""text/javascript"" src=""http://cdnjs.cloudflare.com/ajax/libs/bootstrap-datetimepicker/3.1.3/js/bootstrap-datetimepicker.min.js""></script>" +
                    @"</body></html>";
            }
        }
        public string HtmlJQueryMask
        {
            set
            {
                Content = $"<html><head></head><body>{value}" +
                    @"<script-token>" +
                    @"<script type=""text/javascript"" src=""http://code.jquery.com/jquery-3.0.0.min.js""></script>" +
                    @"<script type=""text/javascript"" src=""http://code.jquery.com/qunit/qunit-1.11.0.js""></script>"+
                    @"<script type=""text/javascript"" src=""https://igorescobar.github.io/jQuery-Mask-Plugin/js/jquery.mask.min.js""></script>" +
                     @"</body></html>";
            }
        }

        public string Form
        {
            set
            {
                Content = $"<html><head></head><body><script-token><form>{value}</form></body></html>";
            }
        }

        public string Table
        {
            set
            {
                Content = $"<html><head></head><body><script-token><table>{value}</table></body></html>";
            }
        }

        public string TableRow
        {
            set
            {
                Content = $"<html><head></head><body><script-token><table><tr>{value}</tr></table></body></html>";
            }
        }

        public string IFrame
        {
            set
            {
                Content = $"<html><head></head><body><script-token><iframe srcdoc=\"{XmlEscapeAttribute(value)}\" src=\"mock.htm\"></iframe></body></html>";
            }
        }

        public string TinyMCE5
        {
            set
            {
                Content =
                    @"
<!DOCTYPE html>
<html lang=""en"">
  <head>
    <meta charset = ""utf-8"">
    <meta name = ""viewport"" content = ""width=device-width, initial-scale=1"">
    <script src = ""https://cdn.tiny.cloud/1/no-api-key/tinymce/5/tinymce.min.js"" referrerpolicy = ""origin""></script>
    <script>
      tinymce.init({
        selector: ""#myeditor""
        });
    </script>
  </head>
  <body>" +
$"{value}" +
   @"      
  </body>
</html>";
            }
        }

        public string TinyMCE4
        {
            set
            {
                Content =
                    @"
<!DOCTYPE html>
<html lang=""en"">
  <head>
    <meta charset = ""utf-8"">
    <meta name = ""viewport"" content = ""width=device-width, initial-scale=1"">
    <script src = ""https://cdnjs.cloudflare.com/ajax/libs/tinymce/4.5.5/tinymce.min.js"" referrerpolicy = ""origin""></script>
    <script>
      tinymce.init({
        selector: ""#myeditor""
        });
    </script>
  </head>
  <body>
    <form>" +
$"{value}" +
   @"      
    </form>
  </body>
</html>";
            }
        }

        public string CKEditor4
        {
            set
            {
                Content =
                    @"
<!DOCTYPE html>
<html>
  <head>
    <meta charset=""utf - 8"">
    <title> CKEditor </title>
    <script src = ""https://cdn.ckeditor.com/4.14.0/standard/ckeditor.js""></script>
  </head>
   
  <body>" +
$"{value}" +
   @"   
    <script>
      CKEDITOR.replace('myeditor');
    </script>
  </body>
</html>
";
                    }
        }
        public string CKEditor5
        {
            set
            {
                Content =
                    @"
<!DOCTYPE html>
<html>
  <head>
    <meta charset=""utf - 8"">
    <title> CKEditor </title>
    <script src = ""https://cdn.ckeditor.com/ckeditor5/18.0.0/classic/ckeditor.js""></script>
  </head>
   
  <body>" +
$"{value}" +
   @"   
    <script>
      ClassicEditor
        .create( document.querySelector( '#myeditor' ) )
        .then( editor => {
                console.log( editor );
        } )
        .catch( error => {
                console.error( error );
        } );
    </script>
  </body>
</html>
";
            }
        }

        public static string XmlEscapeAttribute(string unescaped)
            => unescaped.Replace("\"", "&quot;");
    }
}
