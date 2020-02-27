using BoDi;
using PossumLabs.DSL.Core.Variables;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace DSL.Documentation.Example
{
    public class Dealer : IEntity
    {
        public Dealer()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string LogFormat()
            => $"Id:{Id}";
    }

    [Binding]
    public class DealerRepositorySteps : RepositoryStepBase<Dealer>
    {
        public DealerRepositorySteps(
            IObjectContainer objectContainer) : base(objectContainer)
        {
        }

        /// <summary>
        /// Given the Dealer
        /// |   var |
        /// | Dealer1 |
        /// Given the Dealer
        /// |       var | Height |
        /// |  TallDealer |    100 | 
        /// Given the Dealer
        /// |           var |       Title |
        /// | SameTitleDealer | Dealer1.Title | 
        /// When entering 'Dealer1.Title' into element 'Search'
        /// </summary>

        [Given(@"the Dealers?")]
        public void GivenTheDealers(Dictionary<string, Dealer> Dealers)
        {
            foreach (var Dealer in Dealers.Values)
                TemplateManager.ApplyTemplate(Dealer);
            foreach (var Dealer in Dealers.Values)
                CreateDealer(Dealer);
            foreach (var key in Dealers.Keys)
                Add(key, Dealers[key]);
        }

        /// <summary>
        /// Given the Dealer of type 'short'
        /// | var |
        /// |  U1 |
        /// Given the Dealer of type 'tall'
        /// | var |              Title |
        /// |  U2 |      Benalish Hero | 
        /// |  U3 | Roc of Kher Ridges | 
        /// Given the Dealer
        /// |         var |    Title |
        /// | UNoTemplate | D2.Title | 
        /// When entering 'D1.Title' into element 'Search'
        /// </summary>
        [Given(@"the Dealers? of type '(.*)'")]
        public void GivenTheDealers(string template, Dictionary<string, Dealer> Dealers)
        {
            foreach (var Dealer in Dealers.Values)
                TemplateManager.ApplyTemplate(Dealer, template);
            foreach (var Dealer in Dealers.Values)
                CreateDealer(Dealer);
            foreach (var key in Dealers.Keys)
                Add(key, Dealers[key]);
        }

        private void CreateDealer(Dealer Dealer)
        {
            //depends on your system on how you can or want to create a Dealer.
        }
    }
}
