using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Core.FluidDataCreation
{
    public static class JsonExtensions
    {
        public static object DeserializeToDictionaryOrList(this string jo, bool isArray = false)
        {
            if (!isArray)
            {
                isArray = jo.Substring(0, 1) == "[";
            }
            if (!isArray)
            {
                var values = JsonConvert.DeserializeObject<Dictionary<string, object>>(jo);
                var values2 = new Dictionary<string, object>();

                foreach (KeyValuePair<string, object> d in values)
                {
                    if (d.Value is JObject)
                    {
                        values2.Add(d.Key, DeserializeToDictionaryOrList(d.Value.ToString()));
                    }
                    else if (d.Value is JArray)
                    {
                        values2.Add(d.Key, DeserializeToDictionaryOrList(d.Value.ToString(), true));
                    }
                    else
                    {
                        values2.Add(d.Key, d.Value);
                    }
                }
                return values2;
            }
            else
            {
                var values = JsonConvert.DeserializeObject<List<object>>(jo);
                var values2 = new List<object>();
                foreach (var d in values)
                {
                    if (d is JObject)
                    {
                        values2.Add(DeserializeToDictionaryOrList(d.ToString()));
                    }
                    else if (d is JArray)
                    {
                        values2.Add(DeserializeToDictionaryOrList(d.ToString(), true));
                    }
                    else
                    {
                        values2.Add(d);
                    }
                }
                return values2;
            }
        }
    }
}
