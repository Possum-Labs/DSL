using PossumLabs.DSL.Core.Variables;
using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Core.UnitTests
{
    class MyDomainObject : IEntity
    {
        public string MyString { get; set; }
        public int MyInt { get; set; }
        public int? MyNullableInt { get; set; }
        public List<string> MyStringList { get; set; }
        public int[] MyIntArray { get; set; }

        public MyValueObject MyValueObject { get; set; }
        public MyDomainObject NestedDomainObject { get; set; }

        public string LogFormat()
        => "logging";
    }
}
