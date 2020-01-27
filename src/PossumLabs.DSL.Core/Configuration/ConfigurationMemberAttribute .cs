using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Core.Configuration
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ConfigurationMemberAttribute : Attribute
    {
        public ConfigurationMemberAttribute(string path)
        {
            Path = path;
        }

        public string Path { get; set; }
    }
}
