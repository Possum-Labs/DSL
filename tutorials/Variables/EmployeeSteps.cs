using BoDi;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;

namespace Variables
{
    [Binding]
    sealed public class EmployeeSteps : RepositoryStepBase<Employee>
    {
        public EmployeeSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
        }

        [Given(@"the Employees?")]
        public void GivenTheEmployees(Dictionary<string, Employee> employees)
            => employees.Keys.ToList().ForEach(k => Add(k, TemplateManager.ApplyTemplate(employees[k])));

        [Given(@"the Employees? of type '(.*)'")]
        public void GivenTheEmployeesOfType(string type, Dictionary<string, Employee> employees)
            => employees.Keys.ToList().ForEach(k => Add(k, TemplateManager.ApplyTemplate(employees[k],type)));

    }
}
