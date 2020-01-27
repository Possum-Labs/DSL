using System;
using System.Collections.Generic;
using System.Text;
using PossumLabs.DSL.Core.Variables;

namespace PossumLabs.DSL.Core.FluidDataCreation
{
    public abstract class DataCreatorFactoryBase : IDataCreatorFactory
    {
        public DataCreatorFactoryBase()
        {
            Creators = new Dictionary<Type, IDataCreator>();
        }

        protected Dictionary<Type, IDataCreator> Creators { get; }

        public  void Initialize()
        {
            foreach(var dc in Init())
            {
                if (Creators.ContainsKey(dc.Type))
                    throw new Exception($"there is already a creator registred for {dc.Type.Name}");
                Creators.Add(dc.Type, dc);
            }
        }

        protected abstract List<IDataCreator> Init();

        public IDataCreator<T> GetCreator<T>() where T : IValueObject
        {
            if (Creators.ContainsKey(typeof(T)))
                return Creators[typeof(T)] as IDataCreator<T>;
            else throw new NotImplementedException($"there is no creator configured for {typeof(T).Name}");
        }

        public bool Supports<T>() where T : IValueObject
            => Creators.ContainsKey(typeof(T));
    }
}
