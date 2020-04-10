using System;
using System.Reflection;

namespace PossumLabs.DSL.Core.Variables
{
    public interface ITemplateManager
    {
        object ApplyTemplate(Type t, object item, string name = null);
        T ApplyTemplate<T>(T item, string name = null) where T : IValueObject;
        void Initialize(Assembly assembly);
        void Register<T>(Action<T> template, string name = null) where T : IValueObject;
    }
}