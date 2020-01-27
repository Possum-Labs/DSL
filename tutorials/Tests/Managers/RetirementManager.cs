using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LegacyTest.DomainObjects;

namespace LegacyTest.Managers
{
    public class RetirementManager
    {
        private Employee CEO { get; set; }
        internal void SetCEO(Employee ceo)
        {
            CEO = ceo;
            CEO.Role = "CEO";
            Minionize(CEO.Reports);
        }

        internal void Retire(Employee retiree)
        {
            if(retiree == CEO)
            {
                var newCEO = CEO.Reports.OrderByDescending(r => r.Seniority).First();
                newCEO.Reports.AddRange(CEO.Reports.Except(new[] { newCEO }));
                SetCEO(newCEO);

            }
            else
            {
                Remove(CEO, retiree);
            }
        }

        private void Remove(Employee root, Employee target)
        {
            if (root.Reports.Contains(target))
            {
                root.Reports.Remove(target);
                root.Reports.AddRange(target.Reports);
                target.Reports.Clear();
                target.Role = null;
            }
            else
            {
                foreach (var r in root.Reports)
                    Remove(r, target);
            }
        }

        private void Minionize(IEnumerable<Employee> employees)
        {
            if (employees == null)
                return;
            foreach(var e in employees)
            {
                e.Role = "Minion";
                Minionize(e.Reports);
            }
        }
    }
}
