using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PossumLabs.DSL.Core.FluidDataCreation
{
    public class DictionaryWithDefault<TKey, TValue> : Dictionary<TKey, TValue>
    {
        private Func<TKey, TValue> FactoryMethod
        {
            get;
        }
        public DictionaryWithDefault() : base() { }
        public DictionaryWithDefault(Func<TKey, TValue> factoryMethod) : base()
        {
            FactoryMethod = factoryMethod;
        }
        public new TValue this[TKey key]
        {
            get
            {
                if (!base.ContainsKey(key))
                    base.Add(key, FactoryMethod(key));
                return base[key];
            }
            set { base[key] = value; }
        }
    }
}
