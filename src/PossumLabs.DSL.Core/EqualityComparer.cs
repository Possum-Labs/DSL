using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Core
{
    public class EqualityComparer<T> : IEqualityComparer<T>
    {
        public EqualityComparer(Func<T, T, bool> comparer)
        {
            Comparer = comparer;
        }

        private Func<T, T, bool> Comparer {get;}

        public bool Equals(T x, T y)
            => Comparer(x, y);

        public int GetHashCode(T obj)
            =>0;
    }
}
