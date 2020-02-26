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

        //[DefaultToRepositoryDefault(characteristics= "vip, short")]
        //[NullCoalesceWithDefault]
        public User User { get; set; }

        public string LogFormat()
        {
            throw new NotImplementedException();
        }
    }

    [Binding]
    public class TicketRepositorySteps : RepositoryStepBase<Ticket>
    {
        public TicketRepositorySteps(
            IObjectContainer objectContainer) : base(objectContainer)
        {
        }

        /// <summary>
        /// Given the Ticket
        /// |     var |
        /// | Ticket1 |
        /// Given the Ticket
        /// |      var | 
        /// |  Ticket2 | 
        /// Then the value of 'Ticket1.User' is 'Ticket2.User'
        /// Given the User
        /// |   var |
        /// | User1 |
        /// Given the Ticket
        /// |      var |  User |
        /// |  Ticket3 | User1 |
        /// Then the value of 'Ticket1.User' is not 'Ticket3.User'
        /// </summary>

        [Given(@"the Tickets?")]
        public void GivenTheTickets(Dictionary<string, Ticket> Tickets)
        {
            foreach (var Ticket in Tickets.Values)
                TemplateManager.ApplyTemplate(Ticket);
            foreach (var key in Tickets.Keys)
                Add(key, Tickets[key]);
        }
    }
}
