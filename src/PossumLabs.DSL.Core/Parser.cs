using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace PossumLabs.DSL.Core
{
    public static class Parser
    {
        public static Regex IsNull = new Regex(@"null");
        public static Regex IsVariable = new Regex(@"[a-zA-Z]\w*(\.[a-zA-Z]\w*)*");
        public static Regex IsValidMappedHeader = new Regex(@"[a-zA-Z]\w*([\. ][a-zA-Z]\w*)*");
        public static string VaraibleKey = "var";

        // "== 1"   "~= 2"   "!= 1"    ">= 1"    "> 2"   "< 1"   "<= 2"
        public static Regex IsTest = new Regex(@"^([=~!><]=|[><]) ?([0-9,\.\-]+) ?%?$", RegexOptions.Compiled);

        // /regex/
        public static Regex IsRegex = new Regex(@"^/(.*)/$", RegexOptions.Compiled);

        // /regex/
        public static Regex IsJson = new Regex(@"^((?:\[.*\])|(?:{.*}))$", RegexOptions.Compiled);

        // 'litteral'
        // "litteral"
        public static Regex IsLitteral = new Regex(@"^['""](.*)['""]$", RegexOptions.Compiled);
        public static Regex FindLitterals = new Regex(@"{([\w\.]+)}", RegexOptions.Compiled);

        // `stuff${variable}stuff`
        public static Regex IsSubstituted = new Regex(@"^`(.*)`$", RegexOptions.Compiled);

        // 1
        // 0.1
        // 1,000,000.1
        // 0.100
        public static Regex IsNumber = new Regex(@"^([0-9\.,])+$", RegexOptions.Compiled);

        // 1%
        // 1.00%
        public static Regex IsPercentage = new Regex(@"^([0-9\.,]+) ?%$", RegexOptions.Compiled);

        // $.01
        // -$.01
        // $(1.10)
        // ($1.10)
        // $ .01
        // -$ .01
        // $ (1.10)
        // ($ 1.10)
        public static Regex IsMoney = new Regex(@"^[\p{Sc}\(\- ]*(\-?[0-9\.,]+)\)?$", RegexOptions.Compiled);
        // \Sc - currency 
    }
}
