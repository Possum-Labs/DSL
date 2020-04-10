using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Core.Variables
{
    public class ObjectFactory : IObjectFactory
    {
        public ObjectFactory()
        {
            Defaults = new Dictionary<Type, Func<ObjectFactory, Object>>();
        }

        private Dictionary<Type, Func<ObjectFactory, Object>> Defaults { get; }

        public void Register<T>(Func<ObjectFactory, Object> initialize)

        {
            if (!Defaults.ContainsKey(typeof(T)))
                Defaults.Add(typeof(T), null);
            Defaults[typeof(T)] = f => initialize.Invoke(f);
        }

        public T CreateInstance<T>()
        {
            if (Defaults.ContainsKey(typeof(T)))
                return (T)Defaults[typeof(T)].Invoke(this);
            else
                return Activator.CreateInstance<T>();
        }


        public object CreateInstance(Type t)
        {
            if (Defaults.ContainsKey(t))
                return Defaults[t].Invoke(this);
            else
                return Activator.CreateInstance(t);
        }
    }
}
