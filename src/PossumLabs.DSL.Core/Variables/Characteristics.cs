using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PossumLabs.DSL.Core.Variables
{
    public class Characteristics : List<String>, IComparable<Characteristics>, IEquatable<Characteristics>
    {
        static Characteristics()
        {
            None = new Characteristics();
        }

        public static Characteristics None{get;}
        public Characteristics(params string[] items):base(items)
        {

        }

        public int CompareTo(Characteristics other)
        {
            //cast prevents an infinite loop
            if (((object)other) == null)
                return -2;

            var meNotOther = this.Except(other).ToList();
            var otherNotMe = other.Except(this).ToList();

            // not enough
            if (meNotOther.Any())
                return -1 * meNotOther.Count();
            // too many
            if (otherNotMe.Any())
                return 1 * otherNotMe.Count();

            // just right
            return 0;
        }

        public override int GetHashCode()
        {
            if (this.None())
                return 0;
            return this
                .Select(x=>(x?.GetHashCode() ?? -1) % 100) //mod will keep numbers small ish, will still overflow when it gets too big.
                .Sum(x => x);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() == this.GetType())
                return Equals((Characteristics)obj);
            return false;
        }

        public bool Equals(Characteristics other)
            => CompareTo(other) == 0;

        public static bool operator ==(Characteristics a, Characteristics b)
            => a?.Equals(b)??b==null;

        public static bool operator !=(Characteristics a, Characteristics b)
            => !(a?.Equals(b)??b!=null);
    }
}
