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
        public NullCoalesceWithDefaultAttribute() : this(null, null)
        {
        }

        public NullCoalesceWithDefaultAttribute(string characteristics = null, string template = null)
        {
            Template = template;
            Characteristics = characteristics ?? Characteristics.None;
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
        public DefaultToRepositoryDefaultAttribute():this(null,null)
        {
        }

        public DefaultToRepositoryDefaultAttribute(string characteristics = null, string template = null)
        {
            Template = template;
            Characteristics = characteristics ?? Characteristics.None;
        }

        public Characteristics Characteristics { get; }
        public string Template { get; set; }
    }
}
