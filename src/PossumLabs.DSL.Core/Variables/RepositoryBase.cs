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
        public RepositoryBase(Interpeter interpeter, ObjectFactory objectFactory)
        {
            SetupDefaultConversions();
            Defaults = new Dictionary<string, string>();
            Interpeter = interpeter;
            ObjectFactory = objectFactory;
            DefaultInitialized = false;
            FactoryMethods = new Dictionary<Characteristics, Func<T, T>>();
            DefaultCharacteristics = Characteristics.None;

        }

        private Dictionary<string, IValueObject> dictionary = new Dictionary<string, IValueObject>();
        private List<TypeConverter> conversions = new List<TypeConverter>();
        private Interpeter Interpeter { get; }
        private ObjectFactory ObjectFactory { get; }
        //public List<Action<T>> Decorators {get;}
        //IEnumerable<Action<object>> IRepository.Decorators => this.Decorators.Select(x => DetypeAction(x)).ToList();
        //private Action<object> DetypeAction(Action<T> a) => (x) => a((T)x);

        public T this[string key] => (T)dictionary[key];
        IValueObject IRepository.this[string key] => dictionary[key];


        #region defaults & factory methods

        public virtual void DecorateNewItem(T target)
        {
            var props = target.GetType().GetProperties()
                .Where(x => x.CustomAttributes.Select(y=>y.AttributeType).Any(y => y == typeof(NullCoalesceWithDefaultAttribute) || y == typeof(DefaultToRepositoryDefaultAttribute)));
            foreach (var prop in props)
            {
                if (prop.GetValue(target) != null)
                    continue;
                var ret = Interpeter.RegisteredRepositories.Where(x => prop.PropertyType == x.Type);
                if (ret.None())
                    throw new Exception($"Unable to set the default for {target.GetType().Name}.{prop.Name} as " +
                        $"there is no repository for {prop.PropertyType.Name}");
                if (ret.One())
                    prop.SetValue(target, ret.First().GetDefault());
                else
                    throw new Exception($"Too many repositories for type {prop.PropertyType.Name}");
            }
        }

        public Dictionary<Characteristics, Func<T,T>> FactoryMethods { get; }
        public Lazy<T> Default { get; private set; }
        public Characteristics DefaultCharacteristics { get; private set;}
        protected virtual T Factory() {
            var ret = ObjectFactory.CreateInstance<T>();
            DecorateNewItem(ret);
            return ret;
        }
        private bool DefaultInitialized { get; set; }

        public void InitializeDefault(Func<T> defaultFactory)
            => InitializeDefault(defaultFactory, Characteristics.None);
        public void InitializeDefault(Func<T> defaultFactory, Characteristics characteristics)
        {
            if (DefaultInitialized)
                throw new InvalidOperationException("default factory is already set");
            DefaultInitialized = true;
            DefaultCharacteristics = characteristics;
            Default = new Lazy<T>(defaultFactory);
        }

        object IRepository.GetDefault()
            => GetDefault();

        public T GetDefault()
            => GetDefault(DefaultCharacteristics);

        public T GetDefault(Characteristics characteristics)
        {
            if (characteristics == null)
                throw new InvalidOperationException("Can't have a null characteristics; use Characteristics.None");

            //default exitst
            if (DefaultInitialized && Default.IsValueCreated)
            {
                if (DefaultCharacteristics == characteristics)
                    return Default.Value;

                throw new InvalidOperationException("unmatched Default Characteristics for existing default");
            }

            //default is prepped
            if (DefaultCharacteristics == characteristics && Default != null)
                return Default.Value;

            //initialize
            if(FactoryMethods.ContainsKey(characteristics))
            {
                InitializeDefault(() =>  FactoryMethods[characteristics](Factory()));
                return Default.Value;
            }
            else
                throw new InvalidOperationException("un understood set to characteristics, no factory method found");
        }
        #endregion

        

        public Type Type => typeof(T);
        public IEnumerable<TypeConverter> RegisteredConversions => conversions;
        public Dictionary<string, string> Defaults { get; }

        public void Add(string key, IValueObject item) => dictionary.Add(key, item);
        public void Add(Dictionary<string, T> d) => d.Keys.ToList().ForEach(key => dictionary.Add(key, d[key]));
        public bool ContainsKey(string root) => dictionary.ContainsKey(root);

        public IEnumerator<KeyValuePair<string, T>> GetEnumerator()
            => dictionary.ToDictionary(x => x.Key, x => (T)x.Value).ToList().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => dictionary.ToDictionary(x => x.Key, x => (T)x.Value).ToList().GetEnumerator();

        public void RegisterConversion<C>(Func<C, T> conversion, Predicate<C> test) =>
            conversions.Add(new TypeConverter((x) => conversion.Invoke((C)x), x => test.Invoke((C)x)));

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
            foreach (var key in Defaults.Keys)
            {
                if (!values.ContainsKey(key.ToUpper()))
                {
                    values.Add(key.ToUpper(), new KeyValuePair<string, string>($"default/{key}", Defaults[key]));
                }
            }
            return values.MapTo<T>(Interpeter, ObjectFactory);
        }

        public Dictionary<string, object> AsDictionary()
            => dictionary.ToDictionary(
                x => x.Key,
                x => (object)x.Value);
    }
}
