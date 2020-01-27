using PossumLabs.Specflow.Core.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyTest.DomainObjects
{
    public class User : IDomainObject
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public string LogFormat()
            => $"{UserName} : {Password}";
    }
}
