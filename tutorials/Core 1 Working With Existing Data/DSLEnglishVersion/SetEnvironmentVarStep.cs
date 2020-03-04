using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace DSL.Documentation.Example
{
    [Binding]
    public class SetEnvironmentVarStep
    {
        [BeforeFeature]
        public static void SetVariable()
        {
            System.Environment.SetEnvironmentVariable("Admin_Password", "Sup3rS3cret");
        }
    }
}
