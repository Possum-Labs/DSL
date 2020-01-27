using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Core.Variables
{
    public interface IRepository
    {
        bool ContainsKey(string root);
        Type Type { get; }
        IValueObject this[string key]
        { 
            get;
        }
        IEnumerable<TypeConverter> RegisteredConversions { get; }

        void Add(string key, IValueObject item);

        //IEnumerable<Action<object>> Decorators { get; }

        Dictionary<string, object> AsDictionary();
        object GetDefault();
    }
}
