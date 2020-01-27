using PossumLabs.DSL.Core.FluidDataCreation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Core.UnitTests.FluidDataCreation
{
    public class ParentObjectSetup : DomainObjectSetupBase<ParentObject, int>
    {
        public override void Initialize<C>(ParentObject item, Func<ParentObject, int> creator, SetupBase<C> setupBase)
        {
            base.Initialize(item, creator, setupBase);

            var c = new ValueObjectSetup();
            c.Initialize(item.ComplexValue);
            ComplexValue = c;
        }

        [WithCreator("ChildObjects")]
        public ParentObjectSetup WithChild(string name, string template = null, Action<ChildObjectSetup> configurer = null)
        {
            Setup.WithChildObject(name, template, child => {
                child.ParentObjectId = this.Id; // important
                child.ParentObject = this.Item;
                configurer?.Invoke(child);
            });
            return this;
        }

        [WithCreator("ChildObjects")]
        public ParentObjectSetup WithChilderen(int count, string template = null, Action<ChildObjectSetup> configurer = null)
        {
            Setup.WithChildObjects(count, template, child => {
                child.ParentObjectId = this.Id; // important
                child.ParentObject = this.Item;
                configurer?.Invoke(child);
            });
            return this;
        }

        public ParentObjectSetup WithChilderen(string c1, string c2)
            => WithChilderen(Setup.Interpeter.Get<ChildObject>(c1), Setup.Interpeter.Get<ChildObject>(c2));

        [LinkCreator("relations")]
        public ParentObjectSetup WithChilderen(
            [LinkCreatorParameter("first")] ChildObject c1,
            [LinkCreatorParameter("second")] ChildObject c2)
        {
            Item.Links.Add(c1, c2);
            return this;
        }

        public override int GetId(ParentObject item)
            => item.Id;

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

        private Setup Setup { get; set; }

        protected override void SetSetup(ISetup setupBase)
            => Setup = setupBase as Setup;
    }
}
