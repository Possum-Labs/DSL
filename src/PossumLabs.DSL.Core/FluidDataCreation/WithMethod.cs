using System;
using System.Reflection;

namespace PossumLabs.DSL.Core.FluidDataCreation
{
    public class WithMethod
    {
        public Type Type { get; set; }
        public Type ActionType { get; set; }
        public bool Single { get; set; }
        public string JsonAttribute { get; set; }
        public MethodInfo Method { get; set; }
    }
}