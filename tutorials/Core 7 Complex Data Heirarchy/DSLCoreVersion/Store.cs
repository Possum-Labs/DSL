using BoDi;
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

        /// <summary>
        /// Given the Store
        /// |   var |
        /// | Store1 |
        /// Given the Store
        /// |       var | Height |
        /// |  TallStore |    100 | 
        /// Given the Store
        /// |           var |       Title |
        /// | SameTitleStore | Store1.Title | 
        /// When entering 'Store1.Title' into element 'Search'
        /// </summary>

        [Given(@"the Stores?")]
        public void GivenTheStores(Dictionary<string, Store> Stores)
        {
            foreach (var Store in Stores.Values)
                TemplateManager.ApplyTemplate(Store);
            foreach (var Store in Stores.Values)
                CreateStore(Store);
            foreach (var key in Stores.Keys)
                Add(key, Stores[key]);
        }

        /// <summary>
        /// Given the Store of type 'short'
        /// | var |
        /// |  U1 |
        /// Given the Store of type 'tall'
        /// | var |              Title |
        /// |  U2 |      Benalish Hero | 
        /// |  U3 | Roc of Kher Ridges | 
        /// Given the Store
        /// |         var |    Title |
        /// | UNoTemplate | D2.Title | 
        /// When entering 'D1.Title' into element 'Search'
        /// </summary>
        [Given(@"the Stores? of type '(.*)'")]
        public void GivenTheStores(string template, Dictionary<string, Store> Stores)
        {
            foreach (var Store in Stores.Values)
                TemplateManager.ApplyTemplate(Store, template);
            foreach (var Store in Stores.Values)
                CreateStore(Store);
            foreach (var key in Stores.Keys)
                Add(key, Stores[key]);
        }

        private void CreateStore(Store Store)
        {
            //depends on your system on how you can or want to create a Store.
        }
    }
}
