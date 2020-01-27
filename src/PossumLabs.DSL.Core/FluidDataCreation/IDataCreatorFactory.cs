using System;
using System.Collections.Generic;
using System.Text;
using PossumLabs.DSL.Core.Variables;

namespace PossumLabs.DSL.Core.FluidDataCreation
{
    public interface IDataCreatorFactory
    {
        IDataCreator<T> GetCreator<T>() where T : IValueObject;
        bool Supports<T>() where T : IValueObject;
    }
}
