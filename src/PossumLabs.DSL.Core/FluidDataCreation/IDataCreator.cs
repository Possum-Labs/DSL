using System;
using System.Collections.Generic;
using System.Text;
using PossumLabs.DSL.Core.Variables;

namespace PossumLabs.DSL.Core.FluidDataCreation
{
    public interface IDataCreator<T>: IDataCreator where T: IValueObject
    {
        T Create(T item);
        bool CanDestroy { get; }
        void Destroy(T item);
    }

    public interface IDataCreator
    {
        Type Type { get; }
    }
}
