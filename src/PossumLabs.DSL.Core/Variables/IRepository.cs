using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Core.Variables
{
    public interface IRepository
    {
        bool ContainsKey(string root);
        object GetOnlyInstance();
        Type Type { get; }
        IValueObject this[string key]
        { 
            get;
        }
        IEnumerable<TypeConverter> RegisteredConversions { get; }

        void Add(string key, IValueObject item);

        Dictionary<string, object> AsDictionary();
        object GetDefault();
    }
}
