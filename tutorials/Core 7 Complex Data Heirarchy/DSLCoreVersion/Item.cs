using BoDi;
using PossumLabs.DSL.Core.Variables;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace DSL.Documentation.Example
{
    public class Item : IEntity
    {
        public Item()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string LogFormat()
            => $"Id:{Id}";

        [DefaultToRepositoryDefault()]
        public InventoryLocation InventoryLocation { get; set; }
        

        public string Title { get; set; }
        public string Upc { get; set; }
        public decimal Price { get; set; }
    }

    [Binding]
    public class ItemRepositorySteps : RepositoryStepBase<Item>
    {
        public ItemRepositorySteps(
            IObjectContainer objectContainer) : base(objectContainer)
        {
        }

        /// <summary>
        /// Given the Item
        /// |   var |
        /// | Item1 |
        /// Given the Item
        /// |       var | Height |
        /// |  TallItem |    100 | 
        /// Given the Item
        /// |           var |       Title |
        /// | SameTitleItem | Item1.Title | 
        /// When entering 'Item1.Title' into element 'Search'
        /// </summary>

        [Given(@"the Items?")]
        public void GivenTheItems(Dictionary<string, Item> Items)
        {
            foreach (var Item in Items.Values)
                TemplateManager.ApplyTemplate(Item);
            foreach (var Item in Items.Values)
                CreateItem(Item);
            foreach (var key in Items.Keys)
                Add(key, Items[key]);
        }

        /// <summary>
        /// Given the Item of type 'short'
        /// | var |
        /// |  U1 |
        /// Given the Item of type 'tall'
        /// | var |              Title |
        /// |  U2 |      Benalish Hero | 
        /// |  U3 | Roc of Kher Ridges | 
        /// Given the Item
        /// |         var |    Title |
        /// | UNoTemplate | D2.Title | 
        /// When entering 'D1.Title' into element 'Search'
        /// </summary>
        [Given(@"the Items? of type '(.*)'")]
        public void GivenTheItems(string template, Dictionary<string, Item> Items)
        {
            foreach (var Item in Items.Values)
                TemplateManager.ApplyTemplate(Item, template);
            foreach (var Item in Items.Values)
                CreateItem(Item);
            foreach (var key in Items.Keys)
                Add(key, Items[key]);
        }

        private void CreateItem(Item Item)
        {
            //depends on your system on how you can or want to create a Item.
        }
    }
}
