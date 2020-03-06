using BoDi;
using PossumLabs.DSL;
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

        [BeforeScenario(Order = int.MinValue + 2)]
        public void InitializeDefault()
        {
            Repository.InitializeDefault(() =>
            {
                var dealer = new Dealer();
                CreateDealer(dealer);
                return dealer;
            });
            Repository.InitializeCharacteristicsTransition((x) =>
            {
                CreateDealer(x);
                return x;
            }, Characteristics.None);
            Repository.InitializeCharacteristicsTransition((x) =>
            {
                Repository.CharacteristicsTransitionMethods[Characteristics.None](x);
                //MakeSpecial(x);
                return x;
            }, "special");
        }

        [Given(@"the Dealers?")]
        public void GivenTheDealers(Dictionary<string, Dealer> dealers)
    => GivenTheDealers(null, Characteristics.None, dealers);

        [Given(@"the Dealers? of type '([\w ]*)'")]
        public void GivenTheDealers(string template, Dictionary<string, Dealer> dealers)
            => GivenTheDealers(template, Characteristics.None, dealers);

        [Given(@"the Dealers? that (?:is|are) '([\w ,]*)'")]
        public void GivenTheDealers(Characteristics characteristics, Dictionary<string, Dealer> dealers)
            => GivenTheDealers(null, characteristics, dealers);

        [Given(@"the Dealers? of type '([\w ]*)' that (?:is|are) '([\w ,]*)'")]
        public void GivenTheDealers(
            string template = null,
            Characteristics characteristics = null,
            Dictionary<string, Dealer> dealers = null)
        {
            foreach (var dealer in dealers.Values)
            {
                TemplateManager.ApplyTemplate(dealer, template);
                Repository.DecorateNewItem(dealer);
                Repository.CharacteristicsTransitionMethods[characteristics](dealer);
            }
            foreach (var key in dealers.Keys)
                Add(key, dealers[key]);
        }

        private void CreateDealer(Dealer Dealer)
        {
            //depends on your system on how you can or want to create a Dealer.
        }
    }
}
