using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Core
{
    public class GherkinException : Exception
    {
        public GherkinException(string message) : base(message)
        {
        }
    }
}
