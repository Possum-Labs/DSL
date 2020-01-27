using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoDi;
using FluentAssertions;
using TechTalk.SpecFlow;
using PossumLabs.Specflow.Core.Variables;
using PossumLabs.Specflow.Core;

namespace Variables
{
    [Binding]
    public class VariableSteps : StepBase
    {
        public VariableSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
        }

        [Given(@"setting the properties")]
        public void GivenSettingTheProperties(Table table)
        {
            table.ContainsColumn("var").Should().BeTrue("You must specify a 'var' column");
            foreach (var row in table.Rows)
            {
                var target = Interpeter.Get<object>(row["var"]);
                var members = target.GetType().GetValueMembers();
                foreach (var column in table.Header.Except(new List<string> { "var" }))
                {
                    var member = members.FirstOrDefault(m => String.Equals(m.Name, column, StringComparison.InvariantCultureIgnoreCase));
                    if (member == null)
                        throw new GherkinException($"The column '{column}' does not exist on '{target.GetType().Name}' options are '{members.LogFormat(m=>m.Name)}'");
                    var value = Interpeter.Get<object>(row[column]);
                    member.SetValue(target, value);
                }
            }
        }
    }
}
