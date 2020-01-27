using PossumLabs.DSL.Core.Variables;
using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Core.UnitTests.FluidDataCreation
{
    
    public class ChildObject : IEntity
    {
        public ChildObject()
        {
            ListOfInts = new List<int>();
            ListOfStrings = new List<string>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int Value { get; set; }
        public ValueObject ComplexValue { get; set; }
        public ParentObject ParentObject { get; set; }

        public List<int> ListOfInts { get; set; }
        public List<string> ListOfStrings { get; set; }

        public string LogFormat()
        {
            throw new NotImplementedException();
        }
    }
}
