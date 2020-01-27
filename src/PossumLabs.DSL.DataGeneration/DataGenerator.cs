using PossumLabs.DSL.Core.Variables;
using System;

namespace PossumLabs.DSL.DataGeneration
{
    public class DataGenerator : IValueObject
    {
        public static DataStore GenerateCreatures { get; set; }
        public static DataStore GenerateFemaleFirstNames { get; set; }
        public static DataStore GenerateLastNames { get; set; }
        public static DataStore GenerateMaleFirstNames { get; set; }
        public static DataStore GenerateSeeds { get; set; }

        public DataStore Creatures { get; set; }
        public DataStore FemaleFirstNames { get; set; }
        public DataStore LastNames { get; set; }
        public DataStore MaleFirstNames { get; set; }
        public DataStore Seeds { get; set; }
    }
}
