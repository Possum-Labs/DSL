using PossumLabs.DSL.Core.FluidDataCreation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Core.UnitTests.FluidDataCreation
{
    public class ParentObjectDataCreator : IDataCreator<ParentObject>
    {
        public ParentObjectDataCreator()
        {
            Store = new List<ParentObject>();
        }
        public ParentObject Create(ParentObject item)
        {
            lock(Store)
            {
                Store.Add(item);
                item.Id = Store.Count;
            }
            return item;
        }

        public void Destroy(ParentObject item)
        {
            throw new NotImplementedException();
        }

        public List<ParentObject> Store { get; }

        public bool CanDestroy => false;

        public Type Type => typeof(ParentObject);
    }
}
