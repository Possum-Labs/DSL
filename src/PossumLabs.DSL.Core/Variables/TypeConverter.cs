using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Core.Variables
{
    public class TypeConverter
    {
        public TypeConverter(Func<object, IValueObject> conversion, Predicate<object> test)
        {
            Conversion = conversion;
            Test = test;
        }

        public Func<object, IValueObject> Conversion { get; }
        public Predicate<object> Test { get; }
    }
}
