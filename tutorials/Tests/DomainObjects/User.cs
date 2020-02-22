using PossumLabs.DSL.Core.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSL.Documentation.Example
{
    public class User : IEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public string LogFormat()
            => $"{UserName} : {Password}";
    }
}
