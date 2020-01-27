using PossumLabs.DSL.Core.FluidDataCreation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Core.UnitTests.FluidDataCreation
{
    public class ChildObjectDataCreator : IDataCreator<ChildObject>
    {
        public ChildObjectDataCreator()
        {
            Store = new List<ChildObject>();
        }
        public ChildObject Create(ChildObject item)
        {
            lock (Store)
            {
                Store.Add(item);
                item.Id = Store.Count;
            }
            return item;
        }

        public void Destroy(ChildObject item)
        {
            throw new NotImplementedException();
        }

        public List<ChildObject> Store { get; }

        public bool CanDestroy => false;

        public Type Type => typeof(ChildObject);
    }
}
