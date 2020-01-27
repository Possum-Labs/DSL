using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Core.Variables
{
    public class TypeView
    {
        public string Name { get; set; }
        public List<string> Properties { get; set; }
        public List<ObjectView> Objects { get; set; }
    }
}
