using PossumLabs.DSL.Core.Variables;
using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Core.UnitTests.Variables
{
    public class VariablesTestEntity : IEntity
    {
        public string Name { get; set; }
        public string LogFormat()
            => Name;

        [NullCoalesceWithDefault]
        VariablesTestChildEntity NullCoalesceWithDefault { get; set; }

        [DefaultToRepositoryDefault]
        VariablesTestChildEntity DefaultToRepositoryDefault { get; set; }

        [NullCoalesceWithDefault(template: "option1")]
        VariablesTestChildEntity Option1Template { get; set; }

        [NullCoalesceWithDefault(template: "option1")]
        VariablesTestChildEntity SecondOption1Template { get; set; }

        [NullCoalesceWithDefault(characteristics: "state1")]
        VariablesTestChildEntity State1Characteristic { get; set; }

        [NullCoalesceWithDefault(characteristics: "state1")]
        VariablesTestChildEntity SecondState1Characteristic { get; set; }

        [NullCoalesceWithDefault(characteristics: "state2", template: "option2")]
        VariablesTestChildEntity StateAndCharacteristic { get; set; }
    }
}
