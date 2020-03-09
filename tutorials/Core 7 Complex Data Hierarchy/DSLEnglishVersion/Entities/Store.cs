using BoDi;
using PossumLabs.DSL;
using PossumLabs.DSL.Core.Variables;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace DSL.Documentation.Example
{
    public class Store : IEntity
    {
        public Store()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public bool Defaulted { get; set; }
        public string LogFormat()
            => $"Id:{Id}";

        [DefaultToRepositoryDefault()]
        public Dealer Dealer { get; set; }
    }

    [Binding]
    public class StoreRepositorySteps : RepositoryStepBase<Store>
    {
        public StoreRepositorySteps(
            IObjectContainer objectContainer) : base(objectContainer)
        {
        }

        [BeforeScenario(Order = int.MinValue + 2)]
        public void InitializeDefault()
        {
            Repository.InitializeDefault(() =>
            {
                var store = new Store();
                CreateStore(store);
                store.Defaulted = true;
                Repository.DecorateNewItem(store);
                return store;
            });
            Repository.InitializeCharacteristicsTransition((x) =>
            {
                CreateStore(x);
                return x;
            }, Characteristics.None);
            Repository.InitializeCharacteristicsTransition((x) =>
            {
                Repository.CharacteristicsTransitionMethods[Characteristics.None](x);
                //MakeSpecial(x);
                return x;
            }, "special");
        }

        [Given(@"the Stores?")]
        public void GivenTheStores(Dictionary<string, Store> stores)
     => GivenTheStores(null, Characteristics.None, stores);

        [Given(@"the Stores? of type '([\w ]*)'")]
        public void GivenTheStores(string template, Dictionary<string, Store> stores)
            => GivenTheStores(template, Characteristics.None, stores);

        [Given(@"the Stores? that (?:is|are) '([\w ,]*)'")]
        public void GivenTheStores(Characteristics characteristics, Dictionary<string, Store> stores)
            => GivenTheStores(null, characteristics, stores);

        [Given(@"the Stores? of type '([\w ]*)' that (?:is|are) '([\w ,]*)'")]
        public void GivenTheStores(
            string template = null,
            Characteristics characteristics = null,
            Dictionary<string, Store> stores = null)
        {
            foreach (var store in stores.Values)
            {
                TemplateManager.ApplyTemplate(store, template);
                Repository.DecorateNewItem(store);
                Repository.CharacteristicsTransitionMethods[characteristics](store);
            }
            foreach (var key in stores.Keys)
                Add(key, stores[key]);
        }
        private void CreateStore(Store Store)
        {
            //depends on your system on how you can or want to create a Store.
        }
    }
}
