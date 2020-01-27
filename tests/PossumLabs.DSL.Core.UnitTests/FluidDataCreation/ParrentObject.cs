using PossumLabs.DSL.Core.Variables;
using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Core.UnitTests.FluidDataCreation
{
    public class ParentObject:IEntity
    {
        public ParentObject()
        {
            Links = new Dictionary<ChildObject, ChildObject>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int Value { get; set; }
        public ValueObject ComplexValue { get; set; }
        public ChildObject Child { get; set; }
        public string ParentObjectId { get; internal set; }
        public Dictionary<ChildObject, ChildObject> Links { get; }

        public string LogFormat()
        {
            throw new NotImplementedException();
        }
    }
}
