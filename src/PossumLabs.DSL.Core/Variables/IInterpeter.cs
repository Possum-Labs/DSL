using System;
using System.Collections.Generic;

namespace PossumLabs.DSL.Core.Variables
{
    public interface IInterpeter
    {
        List<IRepository> RegisteredRepositories { get; }

        void Add(Type t, string name, object item);
        void Add<T>(string name, T item) where T : IValueObject;
        object Cast(object data, Type Type);
        object Convert(Type targetType, object o);
        X Convert<X>(object o);
        RepositoryView GenerateView();
        object Get(Type t, string path);
        X Get<X>(string path);
        void Register(IRepository repository);
        void Set<X>(string path, X value);
    }
}