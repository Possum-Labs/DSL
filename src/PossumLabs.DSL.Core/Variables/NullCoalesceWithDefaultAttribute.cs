using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Core.Variables
{
    /// <summary>
    /// Same as DefaultToRepositoryDefault, acurate C# vocabulary
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NullCoalesceWithDefaultAttribute : Attribute, INullCoalesceWithDefaultAttribute
    {
        public NullCoalesceWithDefaultAttribute()
        {
            Characteristics = Characteristics.None;
        }
        public NullCoalesceWithDefaultAttribute(Characteristics characteristics)
        {
            Characteristics = characteristics;
        }
        public NullCoalesceWithDefaultAttribute(string template)
        {
            Template = template;
            Characteristics = Characteristics.None;
        }

        public NullCoalesceWithDefaultAttribute(Characteristics characteristics, string template)
        {
            Template = template;
            Characteristics = characteristics;
        }


        public Characteristics Characteristics { get; }
        public string Template { get; set; }
    }

    /// <summary>
    /// Same as NullCoalesceWithDefault, less academic while still descriptive
    /// </summary>
    /// [AttributeUsage(AttributeTargets.Property)]
    public class DefaultToRepositoryDefaultAttribute : Attribute, INullCoalesceWithDefaultAttribute
    {
        public DefaultToRepositoryDefaultAttribute()
        {
            Characteristics = Characteristics.None;
        }

        public DefaultToRepositoryDefaultAttribute(Characteristics characteristics)
        {
            Characteristics = characteristics;
        }

        public DefaultToRepositoryDefaultAttribute(string template)
        {
            Template = template;
            Characteristics = Characteristics.None;
        }

        public DefaultToRepositoryDefaultAttribute(Characteristics characteristics, string template)
        {
            Template = template;
            Characteristics = characteristics;
        }


        public Characteristics Characteristics { get; }
        public string Template { get; set; }
    }
}
