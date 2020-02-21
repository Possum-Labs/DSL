using PossumLabs.DSL.Core.FluidDataCreation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Core.UnitTests.FluidDataCreation
{
    public class ChildObjectSetup : EntitySetupBase<ChildObject,int>
    {
        

        public string Name
        {
            get { return Item.Name; }
            set
            {
                Item.Name = value;
                NotifyChange();
            }
        }

        public string Category
        {
            get { return Item.Category; }
            set
            {
                Item.Category = value;
                NotifyChange();
            }
        }

        public int Value
        {
            get { return Item.Value; }
            set
            {
                Item.Value = value;
                NotifyChange();
            }
        }

        public List<int> ListOfInts
        {
            get { return Item.ListOfInts; }
            set
            {
                Item.ListOfInts = value;
                NotifyChange();
            }
        }

        public List<string> ListOfStrings
        {
            get { return Item.ListOfStrings; }
            set
            {
                Item.ListOfStrings = value;
                NotifyChange();
            }
        }

        private ValueObjectSetup _ComplexValue;
        public ValueObjectSetup ComplexValue
        {
            get { return _ComplexValue; }
            set
            {
                Unsubscribe(_ComplexValue);
                _ComplexValue = value;
                Subscribe(_ComplexValue);
                NotifyChange();
            }
        }

        public ParentObject ParentObject { set => Item.ParentObject = value; }
        public int ParentObjectId { get; internal set; }

        public override int GetId(ChildObject item)
            => item.Id;

        private Setup Setup { get; set; }
        protected override void SetSetup(ISetup setupBase)
            => Setup = setupBase as Setup;
    }
}
