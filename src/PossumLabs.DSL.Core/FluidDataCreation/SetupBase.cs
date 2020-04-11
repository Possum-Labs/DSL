using PossumLabs.DSL.Core.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PossumLabs.DSL.Core.FluidDataCreation
{
    public abstract class SetupBase<C> : ISetup where C: SetupBase<C>
    {
        public SetupBase(
          IDataCreatorFactory dataCreatorFactory,
          IObjectFactory objectFactory,
          ITemplateManager templateManager,
          IInterpeter interpeter)
        {
            DataCreatorFactory = dataCreatorFactory;
            ObjectFactory = objectFactory;
            TemplateManager = templateManager;
            Interpeter = interpeter;
        }

        protected IObjectFactory ObjectFactory { get; }
        protected ITemplateManager TemplateManager { get; }
        protected IDataCreatorFactory DataCreatorFactory { get; }
        public IInterpeter Interpeter { get; }

        protected C With<T, S, Tid>(
            RepositoryBase<T> repository,
            string name,
            string template = null,
            Action<S> configurer = null)

            where T : IEntity
            where S : EntitySetupBase<T, Tid>
            where Tid : IEquatable<Tid>
        {
            var item = ObjectFactory.CreateInstance<T>();
            TemplateManager.ApplyTemplate(item, template);
            Func<T, Tid> creator = (i) =>
            {
                DataCreatorFactory.GetCreator<T>().Create(i);
                return Activator.CreateInstance<S>().GetId(i);
            };
            var itemSetup = (S)Activator.CreateInstance(typeof(S)) as S;
            itemSetup.Initialize(item, creator, this);
            configurer?.Invoke(itemSetup);
            repository.Add(name, itemSetup.Final);
            return (C)this;
        }

        protected C WithMany<T, S, Tid>(
            RepositoryBase<T> repository,
            Func<string, string, Action<S>, C> with,
            int count,
            string template = null,
            Action<S> configurer = null)

            where T : IEntity
            where S : EntitySetupBase<T, Tid>
            where Tid : IEquatable<Tid>
        {
            var max = GetMaxNumber(repository);
            foreach (var n in Enumerable.Range(max + 1, count))
                with(n.ToString(), template, configurer);

            return (C)this;
        }

        private int GetMaxNumber<T>(RepositoryBase<T> repository) where T : IValueObject
        {
            if (repository.None() || 
                repository
                .Select(x => x.Key)
                .Where(x => int.TryParse(x, out int junk)).None())
                return 0;

            return repository
                .Select(x => x.Key)
                .Where(x => int.TryParse(x, out int junk))
                .Select(x => int.Parse(x))
                .Max();
        }
    }
}
