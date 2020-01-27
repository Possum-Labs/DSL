using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Core.FluidDataCreation
{
    public class CreatorAttribute : Attribute
    {
        public CreatorAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
