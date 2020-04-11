using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PossumLabs.DSL.Core.Variables;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PossumLabs.DSL.Core.FluidDataCreation
{
    public class SetupDriver<C> where C : SetupBase<C>
    {
        public SetupDriver(C setup, IInterpeter interpeter)
        {
            Setup = setup;
            Interpeter = interpeter;
            Comparer = new IgnoreCaseEqualityComparer();
        }

        private C Setup { get; }
        private IInterpeter Interpeter { get; }
        private IgnoreCaseEqualityComparer Comparer { get; }

        public void Processor(string fileContent)
            => RecursiveSetup(Setup, fileContent.DeserializeToDictionaryOrList() as Dictionary<string, object>);

        public void RecursiveSetup(object setup, Dictionary<string, object> configuration)
        {
            var setupMembers = setup.GetType().GetValueMembers();
            var withMethods = GetWithMethods(setup);
            var linkMethods = GetLinkMethods(setup);

            var unmatched = configuration.Keys
                .Except(setupMembers.Select(sm => sm.Name), Comparer) //property setters
                .Except(withMethods.Select(sm => sm.JsonAttribute), Comparer) //with setters
                .Except(linkMethods.Select(sm => sm.JsonAttribute), Comparer) //with setters
                .Except(new string[] { "var", "count", "template" }) //magic driver values
                .ToList();

            if (unmatched.Any())
                throw new Exception($"{configuration.GetType().Name} does not have the properties {unmatched.LogFormat()}");

            ProcessValueMembers(setup, configuration, setupMembers);
            ProcessWithCreation(setup, configuration, withMethods);
            ProcessLinkCreation(setup, configuration, linkMethods);
        }

        private void ProcessValueMembers(object setup, Dictionary<string, object> configuration, IEnumerable<ValueMemberInfo> setupMembers)
        {
            foreach (var m in configuration.Keys.Where(m => setupMembers.Select(sm => sm.Name)
                            .Any(s => Comparer.Equals(s, m))))
            {
                var sm = setupMembers.First(s => Comparer.Equals(s.Name, m));

                // avoid repositories
                if (typeof(IRepository).IsAssignableFrom(sm.Type))
                    continue;
                var token = configuration[m];
                if (token is IList)
                {
                    var a = (IList)token;
                    var l = (IList)sm.GetValue(setup);
                    var listType = l.GetType().GetGenericArguments()[0];
                    foreach (var i in a)
                    {
                        l.Add(i.TryConvertTo(listType));
                    }
                }
                else
                {
                    sm.SetValue(setup, Interpeter.Get(sm.Type, token.ToString()));
                }
            }
        }

        private static List<WithMethod> GetWithMethods(object setup)
            => setup.GetType().GetMethods()
                .Select(x => new MethodCache
                {
                    Method = x,
                    Attributes = x.GetCustomAttributes(typeof(WithCreatorAttribute), true).Cast<CreatorAttribute>().ToList(),
                    ParameterTypes = x.GetParameters().Select(y => y.ParameterType).ToList()
                })
                //trying to be safe
                .Where(x =>
                    x.Attributes.Any() &&
                    x.ParameterTypes.Count == 3 &&
                    (x.ParameterTypes[0] == typeof(int) || x.ParameterTypes[0] == typeof(string)) &&
                    x.ParameterTypes[1] == typeof(string) &&
                    x.ParameterTypes[2].IsGenericType &&
                    x.ParameterTypes[2].GenericTypeArguments.Length == 1
                    )
                //data for future processing
                .Select(x =>
                new WithMethod
                {
                    Method = x.Method,
                    Type = x.ParameterTypes[2].GenericTypeArguments[0],
                    ActionType = x.ParameterTypes[2],
                    Single = x.ParameterTypes.First() == typeof(string),
                    JsonAttribute = x.Attributes.First().Name
                })
                .ToList();

        private void ProcessWithCreation(object setup, Dictionary<string, object> configuration, List<WithMethod> withMethods)
        {
            foreach (var m in configuration.Keys.Where(m => withMethods.Select(sm => sm.JsonAttribute)
                            .Any(s => Comparer.Equals(s, m))))
            {
                var children = configuration[m] as List<object>;

                foreach (var child in children.Cast<Dictionary<string, object>>())
                {
                    bool? single = null;
                    string var = null;
                    string template = null;
                    int? count = null;

                    if (child.ContainsKey("var"))
                    {
                        single = true;
                        if (!typeof(string).IsAssignableFrom(child["var"].GetType()))
                            throw new NotSupportedException($"var has to be a string under {m}");
                        var = (string)child["var"];
                    }
                    if (child.ContainsKey("count"))
                    {
                        single = false;
                        if (!typeof(Int64).IsAssignableFrom(child["count"].GetType()))
                            throw new NotSupportedException($"count has to be a integer under {m}");
                        count = (int)Convert.ToInt32((Int64)child["count"]);
                    }

                    if (child.ContainsKey("template"))
                    {
                        if (!typeof(string).IsAssignableFrom(child["template"].GetType()))
                            throw new NotSupportedException($"template has to be a string under {m}");
                        template = (string)child["template"];
                    }

                    if (child.ContainsKey("var") && child.ContainsKey("count"))
                        throw new InvalidOperationException($"can't have both var and count under {m}");
                    if (!single.HasValue)
                        throw new InvalidOperationException($"need either var and count under {m}");

                    var sm = withMethods.FirstOrDefault(s => Comparer.Equals(s.JsonAttribute, m) && s.Single == single);

                    var mode = single == true ? "single" : "count";
                    if (sm == null)
                        throw new NotImplementedException($"{setup.GetType().Name} does not support the {mode} creation of {m}");

                    Action<dynamic> r = (i) => RecursiveSetup(i, child);

                    if (single == true)
                        sm.Method.Invoke(setup, new object[] { var, template, r });
                    else
                        sm.Method.Invoke(setup, new object[] { count, template, r });
                }
            }
        }

        private static List<LinkMethod> GetLinkMethods(object setup)
            => setup.GetType().GetMethods()
                .Select(x => new MethodCache
                {
                    Method = x,
                    Attributes = x.GetCustomAttributes(typeof(LinkCreatorAttribute), true).Cast<CreatorAttribute>().ToList(),
                    ParameterTypes = x.GetParameters().Select(y => y.ParameterType).ToList()
                })
                .Where(x=>x.Attributes.Any())
                //data for future processing
                .Select(x =>
                new LinkMethod
                {
                    Method = x.Method,
                    Parameters = x.Method.GetParameters().Select(p =>
                        new LinkMethodParameter
                        {
                            JsonAttribute =
                                p.GetCustomAttributes(typeof(LinkCreatorParameterAttribute), true)
                                .Cast<LinkCreatorParameterAttribute>().First().Name,
                            Type = p.ParameterType
                        }).ToList(),
                    JsonAttribute = x.Attributes.First().Name
                })
                .ToList();

        private void ProcessLinkCreation(object setup, Dictionary<string, object> configuration, List<LinkMethod> linkMethods)
        {
            foreach (var m in configuration.Keys.Where(m => linkMethods.Select(sm => sm.JsonAttribute)
                            .Any(s => Comparer.Equals(s, m))))
            {
                var links = configuration[m] as List<object>;

                foreach (var link in links.Cast<Dictionary<string, object>>())
                {
                    var sm = linkMethods.FirstOrDefault(s => Comparer.Equals(s.JsonAttribute, m));
                    var l = new List<object>();
                    foreach(var p in sm.Parameters)
                    {
                        l.Add(Interpeter.Get(p.Type, link[p.JsonAttribute].ToString()));
                    }
                    sm.Method.Invoke(setup, l.ToArray());
                }
            }
        }

    }
}
