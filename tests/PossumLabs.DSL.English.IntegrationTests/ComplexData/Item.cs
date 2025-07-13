using Reqnroll.BoDi;
using PossumLabs.DSL;
using PossumLabs.DSL.Core.Variables;
using System;
using System.Collections.Generic;
using System.Text;
using Reqnroll;

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

        [BeforeScenario(Order = int.MinValue + 11)]
        public void InitializeDefault()
        {
            Repository.InitializeDefault(() =>
            {
                var item = new Item();
                CreateItem(item);
                return item;
            });
            Repository.InitializeCharacteristicsTransition((x) =>
            {
                CreateItem(x);
                return x;
            }, Characteristics.None);
            Repository.InitializeCharacteristicsTransition((x) =>
            {
                Repository.CharacteristicsTransitionMethods[Characteristics.None](x);
                MarkAsDamaged(x);
                return x;
            }, "damaged");
        }

        [Given(@"the Items?")]
        public void GivenTheItems(Dictionary<string, Item> items)
    => GivenTheItems(null, Characteristics.None, items);

        [Given(@"the Items? of type '([\w ]*)'")]
        public void GivenTheItems(string template, Dictionary<string, Item> items)
            => GivenTheItems(template, Characteristics.None, items);

        [Given(@"the Items? that (?:is|are) '([\w ,]*)'")]
        public void GivenTheItems(Characteristics characteristics, Dictionary<string, Item> items)
            => GivenTheItems(null, characteristics, items);

        [Given(@"the Items? of type '([\w ]*)' that (?:is|are) '([\w ,]*)'")]
        public void GivenTheItems(
            string template = null,
            Characteristics characteristics = null,
            Dictionary<string, Item> items = null)
        {
            foreach (var item in items.Values)
                TemplateManager.ApplyTemplate(item, template);
            foreach (var item in items.Values)
                base.Repository.CharacteristicsTransitionMethods[characteristics](item);
            foreach (var key in items.Keys)
                Add(key, items[key]);
        }

        private void CreateItem(Item Item)
        {
            //depends on your system on how you can or want to create a Item.
        }

        [Given("the Item '(.*)' has been damaged")]
        public void MarkAsDamaged(Item Item)
        {
            //depends on your system on how you can or want to create a Item.
        }
    }
}
