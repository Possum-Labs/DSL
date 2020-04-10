using System;

namespace PossumLabs.DSL.Core.Variables
{
    public interface IObjectFactory
    {
        object CreateInstance(Type t);
        T CreateInstance<T>();
        void Register<T>(Func<ObjectFactory, object> initialize);
    }
}