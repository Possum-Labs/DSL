using System;
using PossumLabs.DSL.Core.Variables;

namespace PossumLabs.DSL.Core.FluidDataCreation
{
    public interface IEntitySetupBase<T, Tid>
        where T : IEntity
        where Tid : IEquatable<Tid>
    {

        Tid GetId(T item);
    }
}