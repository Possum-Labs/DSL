using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Core.PossumLab
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class PossumLabAttribute : Attribute
    {
        public PossumLabAttribute(string name = null, string icon = null, string patternOverwrite = null, bool discoverable = true, int order = 0)
        {
            Name = name;
            Icon = icon;
            PatternOverwrite = patternOverwrite;
            Discoverable = discoverable;
            Order = order;
        }
        public string Name { get; }
        public string Icon { get; }
        public string PatternOverwrite { get; }
        public bool Discoverable { get; }
        public int Order { get; }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class PossumLabDefaultsAttribute : Attribute
    {
        public PossumLabDefaultsAttribute(string icon=null, bool discoverable = true, int orderSeed = 0)
        {
            Icon = icon;
            Discoverable = discoverable;
        }
        public string Icon { get; }
        public bool Discoverable { get; }
        public int OrderSeed { get; }
    }

    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    public class InputAttribute : Attribute
    {
        public InputAttribute(string name = null, string format = null)
        {
            Name = name;
            Format = format;
        }
        public string Name { get; }
        public string Format { get; }
    }
}





