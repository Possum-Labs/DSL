using PossumLabs.DSL.Core.FluidDataCreation;
using PossumLabs.DSL.Core.Variables;
using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Core.UnitTests.FluidDataCreation
{
    public class DataCreatorFactory : IDataCreatorFactory
    {
        public DataCreatorFactory()
        {
            ParentObjectDataCreator = new ParentObjectDataCreator();
            ChildObjectDataCreator = new ChildObjectDataCreator();
        }
        public ParentObjectDataCreator ParentObjectDataCreator { get; }
        public ChildObjectDataCreator ChildObjectDataCreator { get; }

        public IDataCreator<T> GetCreator<T>() where T : IValueObject
        {
            if (typeof(T) == typeof(ParentObject))
                return ParentObjectDataCreator as IDataCreator<T>;
            if (typeof(T) == typeof(ChildObject))
                return ChildObjectDataCreator as IDataCreator<T>;
            throw new NotImplementedException();
        }

        public bool Supports<T>() where T : IValueObject
        {
            throw new NotImplementedException();
        }
    }
}
