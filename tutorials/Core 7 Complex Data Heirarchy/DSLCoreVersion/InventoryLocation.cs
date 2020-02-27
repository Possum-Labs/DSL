using BoDi;
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

        /// <summary>
        /// Given the InventoryLocation
        /// |   var |
        /// | InventoryLocation1 |
        /// Given the InventoryLocation
        /// |       var | Height |
        /// |  TallInventoryLocation |    100 | 
        /// Given the InventoryLocation
        /// |           var |       Title |
        /// | SameTitleInventoryLocation | InventoryLocation1.Title | 
        /// When entering 'InventoryLocation1.Title' into element 'Search'
        /// </summary>

        [Given(@"the InventoryLocations?")]
        public void GivenTheInventoryLocations(Dictionary<string, InventoryLocation> InventoryLocations)
        {
            foreach (var InventoryLocation in InventoryLocations.Values)
                TemplateManager.ApplyTemplate(InventoryLocation);
            foreach (var InventoryLocation in InventoryLocations.Values)
                CreateInventoryLocation(InventoryLocation);
            foreach (var key in InventoryLocations.Keys)
                Add(key, InventoryLocations[key]);
        }

        /// <summary>
        /// Given the InventoryLocation of type 'short'
        /// | var |
        /// |  U1 |
        /// Given the InventoryLocation of type 'tall'
        /// | var |              Title |
        /// |  U2 |      Benalish Hero | 
        /// |  U3 | Roc of Kher Ridges | 
        /// Given the InventoryLocation
        /// |         var |    Title |
        /// | UNoTemplate | D2.Title | 
        /// When entering 'D1.Title' into element 'Search'
        /// </summary>
        [Given(@"the InventoryLocations? of type '(.*)'")]
        public void GivenTheInventoryLocations(string template, Dictionary<string, InventoryLocation> InventoryLocations)
        {
            foreach (var InventoryLocation in InventoryLocations.Values)
                TemplateManager.ApplyTemplate(InventoryLocation, template);
            foreach (var InventoryLocation in InventoryLocations.Values)
                CreateInventoryLocation(InventoryLocation);
            foreach (var key in InventoryLocations.Keys)
                Add(key, InventoryLocations[key]);
        }

        private void CreateInventoryLocation(InventoryLocation InventoryLocation)
        {
            //depends on your system on how you can or want to create a InventoryLocation.
        }
    }
}
