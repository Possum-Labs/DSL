// C#
using System;
using System.Collections.Generic;
using System.Threading;

// Possum Labs
using PossumLabs.DSL.DataGeneration;
using PossumLabs.DSL.Core.Variables;

// Specflow
using BoDi;
using TechTalk.SpecFlow;

namespace PossumLabs.DSL.English.Integration
{
    public class Deal : IEntity
    {
        public Deal()
        {
            ContactPersonName = $"{DataGenerator.GenerateMaleFirstNames?[0]} {DataGenerator.GenerateSeeds?[0]}";
            OrganizationName = $"{DataGenerator.GenerateSeeds?[0]} Inc.";
            Title = $"{DataGenerator.GenerateCreatures?[0]}";
            ExpectedCloseDate = DateTime.Today.AddDays(5);
            Value = "42";
        }
        public string ContactPersonName { get; set; }

        public string OrganizationName { get; set; }

        public string Title { get; set; }

        public string Value { get; set; }

        public DateTime ExpectedCloseDate { get; set; }

        public string LogFormat()
            => Title;
    }

    [Binding]
    public class DealRepositorySteps : RepositoryStepBase<Deal>
    {
        public DealRepositorySteps(IObjectContainer objectContainer,
            DriverSteps driverSteps) : base(objectContainer)
        {
            DriverSteps = driverSteps;
        }

        private DriverSteps DriverSteps { get; }

        /// <summary>
        /// Given the Deal
        /// | var |
        /// |  D1 |
        /// Given the Deal
        /// | var | Value |
        /// |  D2 |   100 | 
        /// Given the Deal
        /// |       var |    Title |
        /// | DNameSake | D2.Title | 
        /// When entering 'D1.Title' into element 'Search'
        /// </summary>
        /// <param name="deals"></param>

        [Given(@"the Deals?")]
        public void GivenTheDeals(Dictionary<string, Deal> deals)
        {
            foreach (var deal in deals.Values)
                UICreate(deal);
            foreach (var key in deals.Keys)
                Add(key, deals[key]);
        }

        /// <summary>
        /// Given the Deal of type 'small'
        /// | var |
        /// |  D1 |
        /// Given the Deal of type 'international'
        /// | var | Value |
        /// |  D2 |   100 | 
        /// |  D3 |   100 | 
        /// Given the Deal
        /// |         var |    Title |
        /// | DNoTemplate | D2.Title | 
        /// When entering 'D1.Title' into element 'Search'
        /// </summary>
        [Given(@"the Deals? of type '(.*)'")]
        public void GivenTheDeals(string template, Dictionary<string, Deal> deals)
        {
            foreach (var deal in deals.Values)
                TemplateManager.ApplyTemplate(deal, template);
            foreach (var deal in deals.Values)
                UICreate(deal);
            foreach (var key in deals.Keys)
                Add(key, deals[key]);
        }

        [Given(@"the Deals? that (?:is|are) '(.*)'")]
        public void GivenTheDealsInState(string characteristics, Dictionary<string, Deal> deals)
        {
            foreach (var deal in deals.Values)
            {
                UICreate(deal);

            }
            foreach (var key in deals.Keys)
                Add(key, deals[key]);
        }

        private void UICreate(Deal deal)
        {
            DriverSteps.GivenNavigatedTo(@"https://possumlabs.pipedrive.com/pipeline");

            //When clicking the element 'Add deal'
            DriverSteps.WhenClickingTheElement(@"Add deal");
            //And entering 'Bob' into element 'Contact person name'
            DriverSteps.WhenEnteringForTheElement(deal.ContactPersonName, @"Contact person name");
            //And entering 'Possum Labs' into element 'Organization name'
            DriverSteps.WhenEnteringForTheElement(deal.OrganizationName, @"Organization name");
            //And entering 'Testing 123' into element 'Deal title'
            DriverSteps.WhenEnteringForTheElement(deal.Title, @"Deal title");
            //And entering '42' into element 'Deal value'
            DriverSteps.WhenEnteringForTheElement(deal.Value, @"Deal value");
            //And entering '1/1/2000' into element 'Expected close date'
            DriverSteps.WhenEnteringForTheElement(deal.ExpectedCloseDate.ToShortDateString(), @"expected_close_date");
            //And clicking the element 'Save'
            DriverSteps.WhenClickingTheElement(@"Save");
            Thread.Sleep(1000);
        }

        private void Lose(Deal deal)
        {

        }

        private void Win(Deal deal)
        {

        }
    }
}
