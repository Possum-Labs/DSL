using BoDi;
using PossumLabs.DSL;
using PossumLabs.DSL.Core.Variables;
using PossumLabs.DSL.English;
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
        }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsVip { get; set; }
        public string LogFormat()
            => $"Id:{Username}";

    }

    [Binding]
    public class UserRepositorySteps : RepositoryStepBase<User>
    {
        public UserRepositorySteps(
            IObjectContainer objectContainer, DriverSteps driverSteps) : base(objectContainer)
        {
            DriverSteps = driverSteps;
            LoggedIn = "logged in";
        }
        public DriverSteps DriverSteps { get; }

        public Characteristics LoggedIn { get; }

        [BeforeScenario(Order = int.MinValue + 2)]
        public void InitializeDefault()
        {
            Repository.InitializeDefault(() => base.Repository["Admin"]);
            Repository.InitializeDefault(() => {
                var user = base.Repository["Admin"];
                Repository.CharacteristicsTransitionMethods[LoggedIn](user);
                return user;
            }, LoggedIn);

            Repository.InitializeCharacteristicsTransition((x) =>
            {
                CreateUser(x);
                return x;
            }, Characteristics.None);

            Repository.InitializeCharacteristicsTransition((x) =>
            {
                Repository.CharacteristicsTransitionMethods[Characteristics.None](x);
                GivenLoggedInAsUser(x);
                return x;
            }, LoggedIn);
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
            DriverSteps.GivenNavigatedToEnglish("http://possumlabs.com/testsite/");
            DriverSteps.WhenEnteringForTheElement(user.Username, "User Name");
            DriverSteps.WhenEnteringForTheElement(user.Password, "Password");
            DriverSteps.WhenClickingTheElement("Login");
        }
    }
}
