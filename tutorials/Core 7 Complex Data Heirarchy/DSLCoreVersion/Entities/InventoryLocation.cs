using BoDi;
using DSL.Documentation.Example;
using PossumLabs.DSL.Core.Variables;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace DSL.Documentation.Example
{
    public class InventoryLocation : IEntity
    {
        public InventoryLocation()
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
    public class InventoryLocationRepositorySteps : RepositoryStepBase<InventoryLocation>
    {
        public InventoryLocationRepositorySteps(
            IObjectContainer objectContainer) : base(objectContainer)
        {
        }

        [BeforeScenario(Order = int.MinValue + 2)]
        public void InitializeDefault()
        {
            Repository.InitializeDefault(() =>
            {
                var inventoryLocation = new InventoryLocation();
                CreateInventoryLocation(inventoryLocation);
                return inventoryLocation;
            });
            Repository.InitializeCharacteristicsTransition((x) =>
            {
                CreateInventoryLocation(x);
                return x;
            }, Characteristics.None);
            Repository.InitializeCharacteristicsTransition((x) =>
            {
                Repository.CharacteristicsTransitionMethods[Characteristics.None](x);
                //MakeSpecial(x);
                return x;
            }, "special");
        }

        [Given(@"the Inventory Locations?")]
        public void GivenTheInventoryLocations(Dictionary<string, InventoryLocation> inventoryLocations)
     => GivenTheInventoryLocations(null, Characteristics.None, inventoryLocations);

        [Given(@"the Inventory Locations? of type '([\w ]*)'")]
        public void GivenTheInventoryLocations(string template, Dictionary<string, InventoryLocation> inventoryLocations)
            => GivenTheInventoryLocations(template, Characteristics.None, inventoryLocations);

        [Given(@"the Inventory Locations? that (?:is|are) '([\w ,]*)'")]
        public void GivenTheInventoryLocations(Characteristics characteristics, Dictionary<string, InventoryLocation> inventoryLocations)
            => GivenTheInventoryLocations(null, characteristics, inventoryLocations);

        [Given(@"the Inventory Locations? of type '([\w ]*)' that (?:is|are) '([\w ,]*)'")]
        public void GivenTheInventoryLocations(
            string template = null,
            Characteristics characteristics = null,
            Dictionary<string, InventoryLocation> inventoryLocations = null)
        {
            foreach (var inventoryLocation in inventoryLocations.Values)
            {
                TemplateManager.ApplyTemplate(inventoryLocation, template);
                Repository.DecorateNewItem(inventoryLocation);
                Repository.CharacteristicsTransitionMethods[characteristics](inventoryLocation);
            }
            foreach (var key in inventoryLocations.Keys)
                Add(key, inventoryLocations[key]);
        }
        private void CreateInventoryLocation(InventoryLocation InventoryLocation)
        {
            //depends on your system on how you can or want to create a InventoryLocation.
        }

        [Given(@"Inventory Location '(\w*)' is associated to Store '(\w*)'")]
        public void GivenInventoryLocationIsAssociatedToStore(InventoryLocation loc, Store store)
        {
            // system specific logic here
        }
    }
}
