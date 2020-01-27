using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Core.Validations
{
    public class ValidationException : Exception
    {
        public ValidationException(string msg):base(msg)
        {
        }
    }
}
