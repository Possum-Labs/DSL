using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace PossumLabs.DSL.Web
{
    public static class Parser
    {
        //#id	$("#lastname") The element with id="lastname"
        public static Regex IsId = new Regex(@"^#([\w-]+)$", RegexOptions.Compiled);
        //.class  $(".intro") All elements with class="intro"
        public static Regex IsClass = new Regex(@"^\.([\w-]+)$", RegexOptions.Compiled);
        //element	$("p")	All <p> elements
        public static Regex IsElement = new Regex(@"^<([a-z][a-z0-9]*)>$", RegexOptions.Compiled);
        //xpath //p	All <p> elements
        public static Regex IsXPath = new Regex(@"^(//.*)$", RegexOptions.Compiled);
    }
}
