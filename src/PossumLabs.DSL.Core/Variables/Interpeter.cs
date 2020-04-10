using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace PossumLabs.DSL.Core.Variables
{
    public class Interpeter : IInterpeter
    {
        public Interpeter(IObjectFactory objectFactory)
        {
            Repositories = new List<IRepository>();
            HasIndexer = new Regex(@"\[([0-9]+)\]", RegexOptions.Compiled);
            MatchVariable = new Regex(@"^[a-zA-Z][0-9a-zA-Z]*((?:\[[0-9]+\])|(?:\.[a-zA-Z][0-9a-zA-Z]*))*$", RegexOptions.Compiled);
            ObjectFactory = objectFactory;
        }

        private List<IRepository> Repositories { get; set; }
        public List<IRepository> RegisteredRepositories => Repositories.ToList();
        private const char Sepetator = '.';
        private Regex HasIndexer { get; }
        private Regex MatchVariable { get; }
        private IObjectFactory ObjectFactory { get; }

        public RepositoryView GenerateView()
        {
            return new RepositoryView
            {
                Types = Repositories
                .Select(r => new { repository = r, properties = r.Type.GetValueMembers().Where(m => m.CanRead).OrderBy(m => m.Name).ToList() })
                .Select(r => new TypeView
                {
                    Name = r.repository.Type.Name,
                    Properties = r.properties.Select(m => m.Name).ToList(),
                    Objects = r.repository.AsDictionary().Select(kv => new ObjectView
                    {
                        var = kv.Key,
                        LogFormat = (kv.Value is IEntity) ? ((IEntity)kv.Value).LogFormat() : null,
                        Values = r.properties.Select(p => p.GetValue(kv.Value)?.ToString()).ToList()
                    }).ToList()
                }).ToList()
            };
        }

        public void Register(IRepository repository)
        {
            Repositories.Add(repository);
        }

        public void Set<X>(string path, X value)
        {
            var target = Walker(path.Split(Sepetator), 1);
            var prop = target.GetType().GetProperty(path.Split(Sepetator).Last());
            prop.SetValue(target, value);
        }

        private object Resolve(string path)
            => Walker(path.Split('.'));

        protected virtual string DefaultKey => "default";

        public object Get(Type t, string path)
        {
            if ((path == string.Empty || path == DefaultKey) && Repositories.One(x => x.Type == t))
                return Repositories.First(x => x.Type == t).GetOnlyInstance();

            return IsVarialbe(path) ? Convert(t, Resolve(path)) : Convert(t, path);
        }

        public X Get<X>(string path)
        {
            if ((path == string.Empty || path == DefaultKey) && Repositories.One(x => x.Type == typeof(X)))
                return (X)Repositories.First(x => x.Type == typeof(X)).GetOnlyInstance();

            return IsVarialbe(path) ? Convert<X>(Resolve(path)) : Convert<X>(path);
        }

        private Func<object, object> ResolveIndexFactory(string rawRoot, out string root, IEnumerable<string> path)
        {
            if (HasIndexer.IsMatch(rawRoot))
            {
                var index = int.Parse(HasIndexer.Matches(rawRoot)[0].Groups[1].Value);
                root = rawRoot.Substring(0, rawRoot.IndexOf('['));
                return (source) =>
                {
                    if (source == null)
                        throw new GherkinException($"Unable to resolve [{ index }] of { path.Aggregate((x, y) => x + '.' + y)}");

                    var indexers = source.GetType().CachedGetProperties().Where(p => p.Name == "Item" && p.GetIndexParameters().One());
                    if (indexers.None())
                    {
                        if (source.GetType().IsArray)
                        {
                            try
                            {
                                return ((Array)source).GetValue(index);
                            }
                            catch (IndexOutOfRangeException)
                            {
                                throw new GherkinException($"Index [{ index }] of { path.Aggregate((x, y) => x + '.' + y)} is out of range, there are not enough elements");
                            }
                        }
                        throw new GherkinException($"The type of {source.GetType()} does not support [{ index }] of { path.Aggregate((x, y) => x + '.' + y)}");
                    }

                    try
                    {
                        return indexers.First().GetValue(source, index.AsObjectArray());
                    }
                    catch (TargetInvocationException e)
                    {
                        if (e.InnerException is ArgumentOutOfRangeException)
                            throw new GherkinException($"Argument [{ index }] of { path.Aggregate((x, y) => x + '.' + y)} is out of range, there are not enough elements");
                        else
                            throw;
                    }
                };
            }
            else
            {
                root = rawRoot;
                return (source) => source;
            }
        }

        public void Add(Type t, string name, Object item)
            => Repositories.First(x => x.Type == t).Add(name, (IValueObject)item);

        public void Add<T>(string name, T item) where T : IValueObject
            => Repositories.First(x => x.Type == typeof(T)).Add(name, item);

        private object Walker(IEnumerable<string> path, int leave = 0)
        {
            var parts = path.Skip(1);
            var rawRoot = path.First();
            var indexResolver = ResolveIndexFactory(rawRoot, out string root, path);

            var repo = Repositories.FirstOrDefault(x => x.ContainsKey(root));

            if (repo == null)
            {
                if (path.One())
                    return rawRoot;
                else
                    throw new GherkinException($"unable to resolve the varaiable with root {root}");
            }
            var current = indexResolver((object)repo[root]);

            while (parts.Count() > leave)
            {
                try
                {
                    indexResolver = ResolveIndexFactory(parts.First(), out string part, path);
                    if (current.GetType().GetInterfaces().Any(i => i == typeof(IDictionary)))
                    {
                        var d = (IDictionary)current;
                        if (d.Contains(part))
                            current = d[part];
                        else
                            throw new GherkinException($"The key {part} does not exist in dictionary " +
                                $"please choose one of these {d.Keys.AsObjectArray().Select(k => k.ToString()).LogFormat()}");
                    }
                    else
                    {
                        var prop = current.GetType().GetValueMember(part, StringComparison.InvariantCultureIgnoreCase);
                        if (prop == null)
                            throw new GherkinException($"The property {part} does not exist on {current.GetType().Name} " +
                                $"please choose one of these {current.GetType().GetValueMembers().LogFormat(m => m.Name)}");
                        current = prop.GetValue(current);
                    }
                    current = indexResolver(current);
                    parts = parts.Skip(1);
                }
                catch (InvalidOperationException)
                {
                    throw new GherkinException($"Unable to resolve {parts.First()} of {path.LogFormat()}. " +
                        $"Did find {current.GetType().CachedGetProperties().Select(x => x.Name).LogFormat()}");
                }
            }
            return current;
        }

        private bool IsVarialbe(string path) => MatchVariable.IsMatch(path);

        public X Convert<X>(object o) => (X)Convert(typeof(X), o);

        //not used yet, but might be useful at some point.
        public object Cast(object data, Type Type)
        {
            var DataParam = Expression.Parameter(typeof(object), "data");
            var Body = Expression.Block(Expression.Convert(Expression.Convert(DataParam, data.GetType()), Type));

            var Run = Expression.Lambda(Body, DataParam).Compile();
            var ret = Run.DynamicInvoke(data);
            return ret;
        }

        public object Convert(Type targetType, object o)
        {
            if (o == null)
            {
                if (targetType.IsValueType)
                    return ObjectFactory.CreateInstance(targetType);
                return null;
            }

            var sourceType = o.GetType();

            //handle nullables
            if (targetType.IsGenericType && targetType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                var r = Convert(Nullable.GetUnderlyingType(targetType), o);
                var c = Cast(r, targetType);
                return c;
            }

            if (targetType.IsAssignableFrom(sourceType))
            {
                if (targetType.GetInterfaces().Contains(typeof(IConvertible)))
                    return System.Convert.ChangeType(o, targetType);
                return o;
            }

            var conversions = Repositories.Where(x => x.Type == targetType).SelectMany(x => x.RegisteredConversions).Where(c => c.Test.Invoke(o));
            if (conversions.Any())
                return conversions.First().Conversion.Invoke(o);

            var convertConversions = typeof(System.Convert).CachedGetMethods()
                .Where(x =>
                    x.ReturnType == targetType &&
                    x.Name.StartsWith("To") &&
                    x.GetParameters().Any(p => p.ParameterType == sourceType) &&
                    x.GetParameters().One());

            if (convertConversions.Any())
                return convertConversions.First().Invoke(null, o.AsObjectArray());

            if (targetType.IsEnum && sourceType == typeof(string))
                return Enum.Parse(targetType, o as string);

            if (targetType.GetConstructors().Where(x => x.IsPublic && !x.IsStatic).Select(c => c.GetParameters())
                .Any(x => x.One() && x.First().ParameterType == sourceType))
                return Activator.CreateInstance(targetType, o);

            var parseMethod = targetType.CachedGetMethods().Where(m => m.IsStatic && m.IsPublic)
                .Where(m => m.Name == "Parse" && m.GetParameters().One() && m.GetParameters().First().ParameterType == sourceType);

            if (parseMethod.Any())
                return parseMethod.First().Invoke(null, o.AsObjectArray());

            if (targetType == typeof(string))
                return o.ToString();

            if (sourceType == typeof(string) && ((string)o).IsValidJson())
                return JsonConvert.DeserializeObject((string)o, targetType);

            // generic lists
            Type genericType = null;
            foreach (Type interfaceType in targetType.GetInterfaces())
            {
                if (interfaceType.IsGenericType &&
                    interfaceType.GetGenericTypeDefinition()
                    == typeof(IList<>))
                {
                    genericType = interfaceType.GetGenericArguments()[0];
                    break;
                }
            }
            if (genericType != null)
            {
                var collection = (IList)Activator.CreateInstance(targetType);
                if (sourceType == genericType || sourceType.IsInstanceOfType(genericType))
                    collection.Add(o);
                else if (sourceType == typeof(string))
                    foreach (var expression in ((string)o).Split(',').Select(x => x.Trim()).Where(x => !String.IsNullOrEmpty(x)))
                        collection.Add(Get(genericType, expression));
                return collection;
            }

            if (sourceType == typeof(string) && (string)o == "null")
                return null;

            throw new GherkinException($"Unable to convert from {sourceType} to {targetType}");
        }
    }
}
