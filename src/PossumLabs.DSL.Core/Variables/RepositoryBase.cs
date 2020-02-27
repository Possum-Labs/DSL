using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;

namespace PossumLabs.DSL.Core.Variables
{
    public class RepositoryBase<T> : IRepository, IEnumerable<KeyValuePair<string, T>>
        where T : IValueObject
    {
        public RepositoryBase(
            Interpeter interpeter, 
            ObjectFactory objectFactory, 
            TemplateManager templateManager = null)
        {
  
            Interpeter = interpeter;
            ObjectFactory = objectFactory;
            TemplateManager = templateManager;
            CharacteristicsTransitionMethods = new Dictionary<Characteristics, Func<T, T>>(); 
            PropertyDefaults = new Dictionary<string, string>();
            Defaults = new Dictionary<Characteristics, Dictionary<string, Lazy<T>>>();
            Conversions = new List<TypeConverter>();
            Dictionary = new Dictionary<string, IValueObject>();

            SetupDefaultConversions();
        }

        public static string DefaultTemplateName => "default";

        private Interpeter Interpeter { get; }
        private ObjectFactory ObjectFactory { get; }
        private TemplateManager TemplateManager { get; }

        private List<TypeConverter> Conversions { get; }

        private Dictionary<string, IValueObject> Dictionary { get; }


        public Dictionary<Characteristics, Dictionary<string, Lazy<T>>> Defaults { get; private set; }
        public Dictionary<Characteristics, Func<T, T>> CharacteristicsTransitionMethods { get; private set; }
        public Type Type => typeof(T);
        public IEnumerable<TypeConverter> RegisteredConversions => Conversions;
        public Dictionary<string, string> PropertyDefaults { get; private set; }

        Type IRepository.Type => throw new NotImplementedException();

        IEnumerable<TypeConverter> IRepository.RegisteredConversions => throw new NotImplementedException();

        public T this[string key] => (T)Dictionary[key];
        IValueObject IRepository.this[string key] => Dictionary[key];


        #region defaults & factory methods

        public virtual void DecorateNewItem(T target)
        {
            var props = target.GetType().GetProperties()
                .Where(x => x.CustomAttributes.Select(y=>y.AttributeType).Any(y => y == typeof(NullCoalesceWithDefaultAttribute) || y == typeof(DefaultToRepositoryDefaultAttribute)));
            foreach (var prop in props)
            {
                if (prop.GetValue(target) != null)
                    continue;
                var attr = prop.CustomAttributes.Where(y =>
                    y.AttributeType == typeof(NullCoalesceWithDefaultAttribute) ||
                    y.AttributeType == typeof(DefaultToRepositoryDefaultAttribute))
                    .Select(x=>prop.GetCustomAttribute(x.AttributeType) as INullCoalesceWithDefaultAttribute)
                    .First();

                var ret = Interpeter.RegisteredRepositories.Where(x => prop.PropertyType == x.Type);
                if (ret.None())
                    throw new Exception($"Unable to set the default for {target.GetType().Name}.{prop.Name} as " +
                        $"there is no repository for {prop.PropertyType.Name}");
                if (ret.One())
                {
                    prop.SetValue(target, ret.First().GetDefault());
                }
                else
                    throw new Exception($"Too many repositories for type {prop.PropertyType.Name}");
            }
        }

        public void InitializeCharacteristicsTransition(Func<T,T> defaultFactory)
            => InitializeCharacteristicsTransition(defaultFactory, Characteristics.None);

        public void InitializeCharacteristicsTransition(Func<T,T> defaultFactory, Characteristics characteristics)
        {
            if (!CharacteristicsTransitionMethods.ContainsKey(characteristics))
                CharacteristicsTransitionMethods.Add(characteristics, defaultFactory);
            else
                throw new Exception($"there is already a default for characteristics:{characteristics}");
        }

        public void InitializeDefault(Func<T> defaultFactory)
            => InitializeDefault(defaultFactory, Characteristics.None, null);

        public void InitializeDefault(Func<T> defaultFactory, Characteristics characteristics, string template = null)
        {
            template = template ?? DefaultTemplateName;

            if (!Defaults.ContainsKey(characteristics))
                Defaults.Add(characteristics, new Dictionary<string, Lazy<T>>());

            if (!Defaults[characteristics].ContainsKey(template))
                Defaults[characteristics].Add(template, new Lazy<T>(defaultFactory));
            else
                throw new Exception($"there is already a default for characteristics:{characteristics}");
        }

        object IRepository.GetDefault()
            => GetDefault();

        public T GetDefault()
            => GetDefault(Characteristics.None);

        public T GetDefault(Characteristics characteristics, string template = null)
        {
            template = template ?? DefaultTemplateName;

            if (characteristics == null)
                throw new InvalidOperationException("Can't have a null characteristics; use Characteristics.None");

            //check for characteristics
            if (!CharacteristicsTransitionMethods.ContainsKey(characteristics))
                throw new Exception($"unknown characteristics:{characteristics}");

            if (!Defaults.ContainsKey(characteristics))
                Defaults.Add(characteristics, new Dictionary<string, Lazy<T>>());

            if (!Defaults[characteristics].ContainsKey(template))
            {
                //initialize
                Func<T> factory = () =>
                   {
                       var ret = ObjectFactory.CreateInstance<T>();
                       if (TemplateManager == null && template != null)
                           throw new NullReferenceException("No template manager was provided");

                       if (template == DefaultTemplateName)
                           TemplateManager?.ApplyTemplate(ret);
                       else
                           TemplateManager?.ApplyTemplate(ret, template);

                       DecorateNewItem(ret);
                       CharacteristicsTransitionMethods[characteristics](ret);
                       return ret;
                   };
                Defaults[characteristics].Add(template, new Lazy<T>(factory));
            }

            return Defaults[characteristics][template].Value;
        }
        #endregion

        public void Add(string key, IValueObject item) => Dictionary.Add(key, item);
        public void Add(Dictionary<string, T> d) => d.Keys.ToList().ForEach(key => Dictionary.Add(key, d[key]));
        public bool ContainsKey(string root) => Dictionary.ContainsKey(root);

        public IEnumerator<KeyValuePair<string, T>> GetEnumerator()
            => Dictionary.ToDictionary(x => x.Key, x => (T)x.Value).ToList().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => Dictionary.ToDictionary(x => x.Key, x => (T)x.Value).ToList().GetEnumerator();

        public void RegisterConversion<C>(Func<C, T> conversion, Predicate<C> test) =>
            Conversions.Add(new TypeConverter((x) => conversion.Invoke((C)x), x => test.Invoke((C)x)));

        protected virtual void SetupDefaultConversions()
        {
            RegisterConversion<object>(
                c => (T)c,
                c => typeof(T).IsAssignableFrom(c.GetType()));

            RegisterConversion<string>(
                c => JsonConvert.DeserializeObject<T>((string)c),
                c => typeof(string).IsAssignableFrom(c.GetType()) && ((string)c).IsValidJson());
        }

        public T Map(Dictionary<string, KeyValuePair<string, string>> values)
        {
            foreach (var key in PropertyDefaults.Keys)
            {
                if (!values.ContainsKey(key.ToUpper()))
                {
                    values.Add(key.ToUpper(), new KeyValuePair<string, string>($"default/{key}", PropertyDefaults[key]));
                }
            }
            return values.MapTo<T>(Interpeter, ObjectFactory);
        }

        public Dictionary<string, object> AsDictionary()
            => Dictionary.ToDictionary(
                x => x.Key,
                x => (object)x.Value);

        object IRepository.GetOnlyInstance()
        {
            if (Defaults.SelectMany(x => x.Value).Many())
                throw new Exception($"There are multiple {typeof(T).Name} in repository, " +
                    $"this prevents the usage of defaults");
            if (Defaults.SelectMany(x => x.Value).One(x => x.Value.IsValueCreated))
                return Defaults.SelectMany(x => x.Value).First().Value.Value;
            if (Defaults.ContainsKey(Characteristics.None))
                return Defaults[Characteristics.None][DefaultTemplateName].Value;
            throw new Exception("Unable tot Get the default, nothing is registred");
        }
    }
}
