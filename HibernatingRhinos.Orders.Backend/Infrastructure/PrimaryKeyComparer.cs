using System;
using System.Collections.Generic;

namespace HibernatingRhinos.Orders.Backend.Infrastructure
{
    public class PrimaryKeyComparer<T> : IEqualityComparer<T>
    {
        private readonly Func<T, object> primaryKeyExtractor;

        public PrimaryKeyComparer(Func<T, object> primaryKeyExtractor)
        {
            this.primaryKeyExtractor = primaryKeyExtractor;
        }

        public bool Equals(T x, T y)
        {
            return Equals(primaryKeyExtractor(x), primaryKeyExtractor(y));
        }

        public int GetHashCode(T obj)
        {
            return primaryKeyExtractor(obj).GetHashCode();
        }
    }
}