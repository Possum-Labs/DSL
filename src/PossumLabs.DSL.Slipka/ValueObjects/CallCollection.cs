using PossumLabs.DSL.Core;
using PossumLabs.DSL.Core.Variables;
using PossumLabs.DSL.Slipka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PossumLabs.DSL.Slipka.ValueObjects
{

    public class CallCollection : List<CallRecord>, IValueObject
    {
        public CallCollection(IEnumerable<CallRecord> calls):base(calls)
        {

        }
    }
}
