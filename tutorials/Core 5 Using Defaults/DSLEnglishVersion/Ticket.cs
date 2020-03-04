using BoDi;
using PossumLabs.DSL;
using PossumLabs.DSL.Core.Variables;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace DSL.Documentation.Example
{
    public class Ticket :IEntity
    {
        public int Id { get; set; }

        //you can use either attribute
        //[DefaultToRepositoryDefault()]
        [NullCoalesceWithDefault]
        public User User { get; set; }

        public string LogFormat()
            => $"{Id}";
    }

    [Binding]
    public class TicketRepositorySteps : RepositoryStepBase<Ticket>
    {
        public TicketRepositorySteps(
            IObjectContainer objectContainer) : base(objectContainer)
        {
        }

        [BeforeScenario(Order = int.MinValue + 2)]
        public void InitializeDefault()
        {
            Repository.InitializeDefault(() =>
            {
                var ticket = new Ticket();
                CreateTicket(ticket);
                return ticket;
            });
            Repository.InitializeCharacteristicsTransition((x) =>
            {
                CreateTicket(x);
                return x;
            }, Characteristics.None);
            Repository.InitializeCharacteristicsTransition((x) =>
            {
                Repository.CharacteristicsTransitionMethods[Characteristics.None](x);
                // something to make it special
                return x;
            }, "special");
        }

        [Given(@"the Tickets?")]
        public void GivenTheTickets(Dictionary<string, Ticket> tickets)
     => GivenTheTickets(null, Characteristics.None, tickets);

        [Given(@"the Tickets? of type '([\w ]*)'")]
        public void GivenTheTickets(string template, Dictionary<string, Ticket> tickets)
            => GivenTheTickets(template, Characteristics.None, tickets);

        [Given(@"the Tickets? that (?:is|are) '([\w ,]*)'")]
        public void GivenTheTickets(Characteristics characteristics, Dictionary<string, Ticket> tickets)
            => GivenTheTickets(null, characteristics, tickets);

        [Given(@"the Tickets? of type '([\w ]*)' that (?:is|are) '([\w ,]*)'")]
        public void GivenTheTickets(
            string template = null,
            Characteristics characteristics = null,
            Dictionary<string, Ticket> tickets = null)
        {
            foreach (var ticket in tickets.Values)
            {
                TemplateManager.ApplyTemplate(ticket, template);
                base.Repository.DecorateNewItem(ticket);
                base.Repository.CharacteristicsTransitionMethods[characteristics](ticket);
            }
            foreach (var key in tickets.Keys)
                Add(key, tickets[key]);
        }
        private void CreateTicket(Ticket Ticket)
        {
            //depends on your system on how you can or want to create a Ticket.
        }
    }
}
