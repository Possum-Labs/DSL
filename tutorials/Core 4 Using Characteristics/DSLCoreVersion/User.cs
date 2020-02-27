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
        }

        public string Title { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public int Height { get; set; }

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

        [Given(@"the Users?")]
        public void GivenTheUsers(Dictionary<string, User> users)
            => GivenTheUsers(null, Characteristics.None, users);

        [Given(@"the Users? of type '([^']*)'")]
        public void GivenTheUsers(string template, Dictionary<string, User> users)
            => GivenTheUsers(template, Characteristics.None, users);

        [Given(@"the Users? that (?:is|are) '([^']*)")]
        public void GivenTheUsers(Characteristics characteristics, Dictionary<string, User> users) 
            => GivenTheUsers(null, characteristics, users);

        [Given(@"the Users? of type '([^']*)' that (?:is|are) '([^']*)")]
        public void GivenTheUsers(
            string template, 
            Characteristics characteristics, 
            Dictionary<string, User> users)
        {
            foreach (var user in users.Values)
            {
                TemplateManager.ApplyTemplate(user, template);
                base.Repository.DecorateNewItem(user);
                base.Repository.CharacteristicsTransitionMethods[characteristics](user);
            }
            foreach (var key in users.Keys)
                Add(key, users[key]);
        }

        [BeforeScenario]
        public void RegisterCharacteristicsTransitionMethods()
        {
            base.Repository.InitializeCharacteristicsTransition(CreateUser, Characteristics.None);
        }

        private User CreateUser(User user)
        {
            //depends on your system on how you can or want to create a user.
            return user;
        }
    }
}
