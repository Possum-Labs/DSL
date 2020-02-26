﻿using BoDi;
using LegacyTest.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace DSL.Documentation.Example
{
    [Binding]
    sealed public class UserSteps : RepositoryStepBase<User>
    {
        public UserSteps(IObjectContainer objectContainer) : base(objectContainer)
        {

        }

        [Given(@"the Users?")]
        public void GivenTheUser(Dictionary<string, User> users)
            => users.Keys.ToList().ForEach(k => Add(k, users[k]));

    }
}
