using PossumLabs.Specflow.Core.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyTest.ValueObjects
{

    public class Date : IValueObject
    {


        public Date(DateTime dateTime = default(DateTime))
        {
            DateTime = dateTime == default(DateTime) ? DateTime.Today : dateTime;
        }

        private DateTime DateTime { get; }

        public int Day => DateTime.Day;
    }
}
