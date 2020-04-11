using BoDi;
using PossumLabs.DSL;
using PossumLabs.DSL.Core.Variables;
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
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public int Height { get; set; }
        public string LogFormat()
            => $"Id:{Id}";

    }

    [Binding]
    public class UserRepositorySteps : RepositoryStepBase<User>
    {
        public UserRepositorySteps(
            IObjectContainer objectContainer) : base(objectContainer)
        {
        }

        [BeforeScenario(Order = int.MinValue + 12)]
        public void InitializeDefault()
        {
            Repository.InitializeDefault(() =>
                {
                    // you could return a user that comes from existing data here
                    var user = new User();
                    CreateUser(user);
                    return user;
                });

            Repository.InitializeCharacteristicsTransition((x) =>
                {
                    CreateUser(x);
                    return x;
                }, Characteristics.None);

            Repository.InitializeCharacteristicsTransition((x) =>
                {
                    Repository.CharacteristicsTransitionMethods[Characteristics.None](x);
                    //MakeSpecial(x);
                    return x;
                }, "special");
        }

        [Given(@"the Users?")]
        public void GivenTheUsers(Dictionary<string, User> users)
            => GivenTheUsers(null, Characteristics.None, users);

        [Given(@"the Users? of type '([\w ]*)'")]
        public void GivenTheUsers(string template, Dictionary<string, User> users)
            => GivenTheUsers(template, Characteristics.None, users);

        [Given(@"the Users? that (?:is|are) '([\w ,]*)'")]
        public void GivenTheUsers(Characteristics characteristics, Dictionary<string, User> users)
            => GivenTheUsers(null, characteristics, users);

        [Given(@"the Users? of type '([\w ]*)' that (?:is|are) '([\w ,]*)'")]
        public void GivenTheUsers(
            string template = null,
            Characteristics characteristics = null,
            Dictionary<string, User> users = null)
        {
            foreach (var user in users.Values)
            {
                TemplateManager.ApplyTemplate(user, template);
                Repository.DecorateNewItem(user);
                Repository.CharacteristicsTransitionMethods[characteristics](user);
            }
            foreach (var key in users.Keys)
                Add(key, users[key]);
        }

        private void CreateUser(User User)
        {
            // system specific logic here
        }

        [Given(@"Logged in as User '(\w*)'")]
        public void GivenLoggedInAsUser(User user)
        {
            // system specific logic here
        }
    }
}
