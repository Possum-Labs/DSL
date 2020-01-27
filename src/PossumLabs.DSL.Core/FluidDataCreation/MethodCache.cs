using System;
using System.Collections.Generic;
using System.Reflection;

namespace PossumLabs.DSL.Core.FluidDataCreation
{
    public class MethodCache
    {
        public MethodInfo Method { get; set; }
        public List<CreatorAttribute> Attributes { get; set; }
        public List<Type> ParameterTypes { get; set; }
    }
}