using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Core.FluidDataCreation
{
    [System.AttributeUsage(System.AttributeTargets.Parameter)]
    public class LinkCreatorParameterAttribute : Attribute
    {
        public LinkCreatorParameterAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
