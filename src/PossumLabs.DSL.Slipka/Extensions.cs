using PossumLabs.DSL.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Slipka
{
    public static class Extensions
    {
        public static string ToHttpFormat(this CallRecord call)
        {
            var ret = $"{call.Method.ToUpper()} {call.Uri}\n";
            if(call.Request == null)
            {
                ret += "### no request data available \n";
                return ret;
            }
            foreach (var h in call.Request.Headers)
                ret += h.ToHttpFormat();

            ret += '\n';

            if (call.Request.Content != null)
                ret += call.Request.Content + '\n';

            ret += "###\n";
            return ret;
        }

        public static string ToHttpFormat(this Header header)
        {
            var ret = string.Empty;
            if (header.Values ==null || header.Values.None())
                ret += $"{header.Key}:\n";
            else
                foreach (var value in header.Values)
                    ret += $"{header.Key}:{value}\n";
            return ret;
        }
    }
}
