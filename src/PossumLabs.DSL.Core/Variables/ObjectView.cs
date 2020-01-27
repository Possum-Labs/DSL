using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Core.Variables
{
    public class ObjectView
    {
        public string var { get; set; }
        public string LogFormat { get; set; }

        public List<string> Values { get; set; }
    }
}
