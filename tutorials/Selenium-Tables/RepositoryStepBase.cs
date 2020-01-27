using BoDi;
using PossumLabs.Specflow.Core;
using PossumLabs.Specflow.Core.Variables;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Selenium_Tables
{
    public abstract class RepositoryStepBase<T> : StepBase
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

        [BeforeScenario(Order = int.MinValue+1)]
        public void RegisterRepositoryWithInterpeter()
        {
            var r = new RepositoryBase<T>(base.Interpeter, base.ObjectFactory);
            base.Register(r);
            Interpeter.Register(r);
        }

        [StepArgumentTransformation]
        public List<T> TransformList(Table table)
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
                       ).Augment(Repository.Defaults))).ToList();
        }

        [StepArgumentTransformation]
        public Dictionary<string, T> TransformDictionary(Table table)
        {
            var dupes = table.Header.GroupBy(x => x.Split().Aggregate((y,z)=>y+"."+z).ToUpper()).Where(x => x.Many());
            if (dupes.Any())
                throw new GherkinException($"the columns {dupes.LogFormat()} are effectively duplicates, matching of columns is case insesnitive");

            if(!table.Header.Contains(Parser.VaraibleKey))
                throw new GherkinException($"a column called \"{Parser.VaraibleKey}\" is required for this step");

            return table.Rows.ToDictionary(
                r => r[Parser.VaraibleKey],
                r => Repository.Map(table.Header.Except(new[] { Parser.VaraibleKey }).ToDictionary(
                           x => x.ToUpper(),
                           x => new KeyValuePair<string, string>(x, r[x])
                       ).Augment(Repository.Defaults)));
        }
        
        [StepArgumentTransformation]
        public T Transform(string id) => Interpeter.Get<T>(id);

        [AfterScenario]
        public void Records()
        {
            if (Repository.Any())
            {
                var msg = string.Empty;
                foreach (var item in Repository.Where(x => x.Value != null))
                {
                    var value = (item.Value is IDomainObject) ? ((IDomainObject)item.Value).LogFormat(): null;
                    if(string.IsNullOrWhiteSpace(value))
                        msg += $"Key:{item.Key} Id:{item.Value}\n";
                    else
                        msg += $"Key:{item.Key} Id:{value}\n";
                }
                base.Log.Section($"Repository for {typeof(T).Name}", msg);
            }
        }

        public void AddDefault(string key, string value)
            => Repository.Defaults.Add(key, value);
    }
}
