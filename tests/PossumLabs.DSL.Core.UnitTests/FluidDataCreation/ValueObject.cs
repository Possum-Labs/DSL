using PossumLabs.DSL.Core.Variables;
using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Core.UnitTests.FluidDataCreation
{
    public class ValueObject:IValueObject
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }
}
