using BoDi;
using PossumLabs.DSL.Core.Variables;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace PossumLabs.DSL.English.Integration
{
    public class User : IEntity
    {
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
            IObjectContainer objectContainer, 
            DriverSteps driverSteps) : base(objectContainer)
        {
            DriverSteps = driverSteps;
        }

        private DriverSteps DriverSteps { get; }

        [Given("logged in as User '(.*)'")]
        public void GivenLoggedInAs(User user)
        {
            //Given navigated to 'https://possumlabs.pipedrive.com/'
            DriverSteps.GivenNavigatedToEnglish(@"https://possumlabs.pipedrive.com/");
            //When entering 'Admin.Email' into element 'Email'
            DriverSteps.WhenEnteringForTheElement(user.Email, @"Email");
            //And entering 'Admin.Password' into element 'Password'
            DriverSteps.WhenEnteringForTheElement(user.Password, @"Password");
            //And clicking the element 'Log in'
            DriverSteps.WhenClickingTheElement(@"Log in");
        }
    }
}
