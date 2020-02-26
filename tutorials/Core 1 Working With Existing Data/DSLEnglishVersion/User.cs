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
    }
}
