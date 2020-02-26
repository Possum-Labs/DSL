using PossumLabs.DSL.Core.Variables;
using System.Collections.Generic;

namespace DSL.Documentation.Example
{
    public class Employee : IEntity
    {
        public Employee()
        {
            Reports = new List<Employee>();
        }
        public string Name { get; set; }
        public List<Employee> Reports { get; set; }
        public string Role { get; set; }
        public int Seniority { get; set; }
        public string LogFormat()
            => Name;
    }
}
