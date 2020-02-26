using PossumLabs.DSL.Core.Variables;
using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Core.UnitTests.Variables
{
    public class VariablesTestChildEntity : IEntity
    {
        private static int counter = 0;
        public VariablesTestChildEntity()
        {
            Name = $"Bob{counter++}";
        }
        public string Name { get; set; }
        public string Title { get; set; }
        public string LogFormat()
            => Name;
    }
}
