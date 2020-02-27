using BoDi;
using PossumLabs.DSL;
using PossumLabs.DSL.Core.Variables;
using PossumLabs.DSL.DataGeneration;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace DSL.Documentation.Example
{
    public class User : IEntity
    {
        public User()
        {
            Functions = new List<string>();
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string LogFormat()
            => $"Id:{Id}";

        public List<string> Functions { get; set; }

        [DefaultToRepositoryDefault()]
        public Store Store { get; set; }
    }

    [Binding]
    public class UserRepositorySteps : RepositoryStepBase<User>
    {
        public UserRepositorySteps(
            IObjectContainer objectContainer) : base(objectContainer)
        {
        }

        /// <summary>
        /// Given the User
        /// |   var |
        /// | User1 |
        /// Given the User
        /// |       var | Height |
        /// |  TallUser |    100 | 
        /// Given the User
        /// |           var |       Title |
        /// | SameTitleUser | User1.Title | 
        /// When entering 'User1.Title' into element 'Search'
        /// </summary>

        [Given(@"the Users?")]
        public void GivenTheUsers(Dictionary<string, User> users)
        {
            foreach (var user in users.Values)
                TemplateManager.ApplyTemplate(user);
            foreach (var user in users.Values)
                CreateUser(user);
            foreach (var key in users.Keys)
                Add(key, users[key]);
        }

        /// <summary>
        /// Given the User of type 'short'
        /// | var |
        /// |  U1 |
        /// Given the User of type 'tall'
        /// | var |              Title |
        /// |  U2 |      Benalish Hero | 
        /// |  U3 | Roc of Kher Ridges | 
        /// Given the User
        /// |         var |    Title |
        /// | UNoTemplate | D2.Title | 
        /// When entering 'D1.Title' into element 'Search'
        /// </summary>
        [Given(@"the Users? of type '([^']*)'")]
        public void GivenTheUsers(string template, Dictionary<string, User> users)
        {
            foreach (var user in users.Values)
                TemplateManager.ApplyTemplate(user, template);
            foreach (var user in users.Values)
                CreateUser(user);
            foreach (var key in users.Keys)
                Add(key, users[key]);
        }

        private void CreateUser(User user)
        {
            //depends on your system on how you can or want to create a user.
        }
    }
}
