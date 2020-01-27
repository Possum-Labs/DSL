using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PossumLabs.DSL.DataGeneration
{
    public class DataStore
    {
        public DataStore(string resourceName)
        {
            Name = resourceName;
            Random = new Random();
            Percentile = 100;
        }

        public string this[int i]
        {
            get {
                var value = GetValue();
                if (i == 0)
                    return value;
                else if (value.Length <= i)
                    return value;
                else
                    return new string(value.ToCharArray().Take(i).ToArray());
            }
        }

        private string[] Options { get; set; }
        private int Length { get; set; }
        private Random Random { get; }

        public string Name { get; }
        public int Percentile { get; set; }

        public void Initialize(string content)
        {
            Options = content.Split('\n');
            Length = Options.Length;
        }

        public string GetValue()
        {
            var percentile = Percentile < 1 ? 1 : Percentile;
            percentile = percentile > 100 ? 100 : percentile;
            return Options[this.Random.Next((Length * percentile) / 100)];
        }
    }
}
