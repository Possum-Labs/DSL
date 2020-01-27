using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Core.FluidDataCreation
{
    [System.AttributeUsage(System.AttributeTargets.Method)]
    public class LinkCreatorAttribute : CreatorAttribute
    {
        public LinkCreatorAttribute(string name) : base(name)
        { }
    }
}
