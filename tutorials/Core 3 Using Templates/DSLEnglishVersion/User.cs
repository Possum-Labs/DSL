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
            Title = $"{DataGenerator.GenerateCreatures?[0]}";
            SupportPriority = 0;
        }

        public string Title { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Height { get; set; }
        public int SupportPriority { get; set; }

        public string LogFormat()
            => $"email:{Email}";
    }

    [Binding]
    public class UserRepositorySteps : RepositoryStepBase<User>
    {
        public UserRepositorySteps(
            IObjectContainer objectContainer) : base(objectContainer)
        {
        }

        //TODO: errr.... this is a mess
        [BeforeScenario]
        public void Setup()
        {
            Repository.InitializeDefault(()=>this.CreateUser(new User()));
        }

        /// <summary>
        /// Given the User
        /// |   var |
        /// | User1 |
        /// </summary>
        [Given(@"the Users?")]
        public void GivenTheUsers(Dictionary<string, User> Users)
            => GivenTheUsers(null, Characteristics.None, Users);

        /// <summary>
        /// Given the User that (?:is|are) 'locked out'
        /// |   var |
        /// | User1 |
        /// </summary>
        [Given(@"the Users?")]
        public void GivenTheUsersWithCharacterisitics(Characteristics characteristics, Dictionary<string, User> Users)
            => GivenTheUsers(null, characteristics,  Users);

        /// <summary>
        /// Given the User of type 'short'
        /// |   var |
        /// | User1 |
        /// </summary>
        [Given(@"the Users? of type '(.*)'")]
        public void GivenTheUsers(string template, Dictionary<string, User> Users)
            => GivenTheUsers(template, Characteristics.None, Users);

        /// <summary>
        /// Given the User of type 'short' that (?:is|are) 'locked out'
        /// |   var |
        /// | User1 |
        /// </summary>
        [Given(@"the Users? of type '(.*)'")]
        public void GivenTheUsers(string template, Characteristics characteristics, Dictionary<string, User> Users)
        {
            //apply template
            foreach (var User in Users.Values)
                TemplateManager.ApplyTemplate(User, template);

            //Set characterisitcs
            foreach (var User in Users.Values)
                TemplateManager.ApplyTemplate(User, template);

            //Add to repository
            foreach (var key in Users.Keys)
                Add(key, Users[key]);
        }

        private User CreateUser(User user)
        {
            //depends on your system on how you can or want to create a user.
            return user;
        }

        private User LockOut(User user)
        {
            //depends on your system on how you can or want to Lock Out a user.
            user.SupportPriority = user.SupportPriority + 1;
            return user;
        }

        private User MakeVip(User user)
        {
            //depends on your system on how you can or want to Make Vip a user.
            user.SupportPriority = user.SupportPriority + 2;
            return user;
        }
    }
}
