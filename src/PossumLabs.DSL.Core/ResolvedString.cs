using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Core
{
    public class ResolvedString
    {
        readonly protected string _value;

        public ResolvedString()
        {
            this._value = string.Empty;
        }

        public ResolvedString(string value)
        {
            this._value = value;
        }

        public static implicit operator string(ResolvedString d)
        {
            return d.ToString();
        }
        public static implicit operator ResolvedString(string d)
        {
            return new ResolvedString(d);
        }

        public override string ToString()
        {
            return _value;
        }
    }
}
