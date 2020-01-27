using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Core.Variables
{
    /// <summary>
    /// Same as DefaultToRepositoryDefault, acurate C# vocabulary
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NullCoalesceWithDefaultAttribute : Attribute
    {
    }

    /// <summary>
    /// Same as NullCoalesceWithDefault, better English Version
    /// </summary>
    /// [AttributeUsage(AttributeTargets.Property)]
    public class DefaultToRepositoryDefaultAttribute : Attribute
    {
    }
}
