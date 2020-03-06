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
            Functions = new List<string>();
        }
        public Guid Id { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string LogFormat()
            => $"Id:{Id}";

        public bool IslocakedOut { get; set; }
        public List<string> Functions { get; set; }

    }

    [Binding]
    public class UserRepositorySteps : RepositoryStepBase<User>
    {
        public UserRepositorySteps(
            IObjectContainer objectContainer) : base(objectContainer)
        {
        }

        [BeforeScenario(Order = int.MinValue + 2)]
        public void InitializeDefault()
        {
            Repository.InitializeDefault(() =>
            {
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
                GivenTheUserIsLocked_Out(x);
                return x;
            }, "locked out");
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
                TemplateManager.ApplyTemplate(user, template);
            foreach (var user in users.Values)
                base.Repository.CharacteristicsTransitionMethods[characteristics](user);
            foreach (var key in users.Keys)
                Add(key, users[key]);
        }
        private void CreateUser(User User)
        {
            //depends on your system on how you can or want to create a User.
        }

        [Given(@"Logged in as User '(\w*)'")]
        public void GivenLoggedInAsUser(User user)
        {
            // system specific logic here
        }

        [Given(@"the User '(.*)' is locked-out")]
        public void GivenTheUserIsLocked_Out(User user)
        {
            user.IslocakedOut = true;
        }



    }
}
