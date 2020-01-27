using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PossumLabs.DSL.Core.Variables
{
    public static class IdGenerator
    {
        private static readonly Random _Random = new Random(Guid.NewGuid().ToString().GetHashCode());

        public static Dictionary<string, string> RandomizationTypes = new Dictionary<string, string>()
        {
            { "Numeric","0123456789" },
            { "Alpha","ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789" },
            { "AlphaNumeric","ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789" },
            { "SpecialCharacters","ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 -&'.,|@+?;#)(" },
        };

        private static string Randomizer(int length, string chars)
        {
            return new string(Enumerable.Repeat(chars, length).Select(s => s[_Random.Next(chars.Length)]).ToArray());
        }

        public static string Generate(string type, int length)
        {
            if (!RandomizationTypes.ContainsKey(type))
                throw new GherkinException($"the randomization type of {type} is not supported please choose one of thest {RandomizationTypes.Keys.LogFormat()}");
            return Randomizer(length, RandomizationTypes[type]);
        }
    }
}
