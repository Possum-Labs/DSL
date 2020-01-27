using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using PossumLabs.DSL.Core;

namespace PossumLabs.DSL.Web
{
    public static class Extensions
    {
        public static string LogFormat(this Dictionary<string, WebValidation> validations)
            => validations.Keys.Select(column => $"column:'{column}' with validation:'{validations[column].Text}'").LogFormat();

        public static IEnumerable<T> Concat<T>(this IEnumerable<T> source, T item)
            =>source.Concat(new List<T> { item });

        public static TValue AddOrUpdate<TKey, TValue>(
            this IDictionary<TKey, TValue> dict,
            TKey key,
            TValue addValue)
        {
            TValue existing;
            if (dict.TryGetValue(key, out existing))
            {
                dict[key] = addValue;
            }
            else
            {
                dict.Add(key, addValue);
            }

            return addValue;
        }

        public static TValue AddUnlessPresent<TKey, TValue>(
            this IDictionary<TKey, TValue> dict,
            TKey key,
            TValue addValue)
        {
            TValue existing;
            if (!dict.TryGetValue(key, out existing))
            {
                dict.Add(key, addValue);
            }

            return addValue;
        }

        public static string SafeGetProperty(this IWebElement webElement, string name)
        {
            try
            {
                return webElement.GetProperty(name);
            }
            catch
            {
                return null;
            }
        }

        public static void ScriptClear(this IJavaScriptExecutor ScriptExecutor, IWebElement e)
           => ScriptExecutor.ScriptSet(e, "");

        public static void ScriptSet(this IJavaScriptExecutor ScriptExecutor, IWebElement e, string val)
        {
            var r = ScriptExecutor.ExecuteScript(@"
try{
    var i = $(arguments[1]);
    i.val(arguments[0]);
    i.trigger( 'change' );
    return  i.val();
}
catch(err) {
return err
}", val, e);
            if (r?.ToString() != val)
                throw new Exception(r.ToString());
        }
    }
}
