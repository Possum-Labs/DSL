using BoDi;
using PossumLabs.DSL.Core;
using PossumLabs.DSL.Core.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;

namespace DSL.Documentation.Example
{
    public abstract class RepositoryStepBase<T> : StepsBase
     where T : IValueObject
    {
        public RepositoryStepBase(IObjectContainer objectContainer) : base(objectContainer)
        {
        }

        protected RepositoryBase<T> Repository => ObjectContainer.Resolve<RepositoryBase<T>>();

        protected T this[string name]
        {
            get => (T)Repository[name];
        }

        public void Add(string name, T item)
        {
            if (Repository.ContainsKey(name))
                throw new GherkinException($"There is already a varaible of type {typeof(T)} and name {name}");
            Repository.Add(name, item);
        }

        virtual protected void Create(T item)
        {
            throw new NotImplementedException("Create is not supported for this repository.");
        }

        [BeforeScenario(Order = int.MinValue)]
        public void RegisterRepositoryWithInterpeter()
        {
            var r = new RepositoryBase<T>(base.Interpeter, base.ObjectFactory);
            base.Register(r);
            Interpeter.Register(r);
        }

        [StepArgumentTransformation]
        public List<T> TransformList(Table table)
            => Executor.ReturnNullWhenErrorOccured(() =>
            {
                var dupes = table.Header.GroupBy(x => x.Split()
                    .Aggregate((y, z) => y + "." + z).ToUpper()).Where(x => x.Many());
                if (dupes.Any())
                    throw new GherkinException(
                        $"the columns {dupes.LogFormat()} are effectively duplicates, matching of columns is case insesnitive");

                return table.Rows.Select(
                    r => Repository.Map(table.Header.ToDictionary(
                               x => x.ToUpper(),
                               x => new KeyValuePair<string, string>(x, r[x])
                           ).Augment(Repository.PropertyDefaults))).ToList();
            });

        [StepArgumentTransformation]
        public Dictionary<string, T> TransformDictionary(Table table)
            => Executor.ReturnNullWhenErrorOccured(() =>
            {
                var dupes = table.Header.GroupBy(x => x.Split().Aggregate((y, z) => y + "." + z).ToUpper()).Where(x => x.Many());
                if (dupes.Any())
                    throw new GherkinException($"the columns {dupes.LogFormat()} are effectively duplicates, matching of columns is case insesnitive");

                if (!table.Header.Contains("var"))
                    throw new GherkinException($"a column called \"{"var"}\" is required for this step");

                return table.Rows.ToDictionary(
                    r => r["var"],
                    r => Repository.Map(table.Header.Except(new[] { "var" }).ToDictionary(
                               x => x.ToUpper(),
                               x => new KeyValuePair<string, string>(x, r[x])
                           ).Augment(Repository.PropertyDefaults)));
            });

        [StepArgumentTransformation]
        public T Transform(string id)
           => Executor.ReturnNullWhenErrorOccured(() =>
                Interpeter.Get<T>(id));

        [AfterScenario]
        public void Records()
        {
            if (Repository.Any())
            {
                base.Log.Section(this.GetType().Name, new
                {
                    Elements = Repository
                    .Where(x => x.Value != null)
                .OrderBy(x => x.Key)
                .Select(x => new
                {
                    x.Key,
                    Value = (x.Value is IEntity) ? ((IEntity)x.Value).LogFormat() : null
                }).ToList()
                });
            }
        }

        public void AddDefault(string key, string value)
            => Repository.PropertyDefaults.Add(key, value);
    }
}
