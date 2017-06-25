using System;

namespace RangeTree
{
    /// <summary>
    /// Represents a range of values. 
    /// Both values must be of the same type and comparable.
    /// </summary>
    /// <typeparam name="T">Type of the values.</typeparam>
    public struct Range<T> : IComparable<Range<T>>
        where T : IComparable<T>
    {
        public T From { get; }

        public T To { get; }

        /// <summary>
        /// Initializes a new <see cref="Range&lt;T&gt;"/> instance.
        /// </summary>
        public Range(T value)
            : this()
        {
            From = value;
            To = value;
        }

        /// <summary>
        /// Initializes a new <see cref="Range&lt;T&gt;"/> instance.
        /// </summary>
        public Range(T from, T to)
            : this()
        {
            if (from.CompareTo(to) == 1)
            {
                throw new ArgumentOutOfRangeException($"{nameof(from)} cannot be larger than {nameof(to)}");
            }

            From = from;
            To = to;
        }

        /// <summary>
        /// Whether the value is contained in the range. 
        /// Border values are considered inside.
        /// </summary>
        public bool Contains(T value)
        {
            return value.CompareTo(From) >= 0 && value.CompareTo(To) <= 0;
        }

        /// <summary>
        /// Whether the value is contained in the range. 
        /// Border values are considered outside.
        /// </summary>
        public bool ContainsExclusive(T value)
        {
            return value.CompareTo(From) > 0 && value.CompareTo(To) < 0;
        }

        /// <summary>
        /// Whether two ranges intersect each other.
        /// </summary>
        public bool Intersects(Range<T> other)
        {
            return other.To.CompareTo(From) >= 0 && other.From.CompareTo(To) <= 0;
        }

        /// <summary>
        /// Whether two ranges intersect each other.
        /// </summary>
        public bool IntersectsExclusive(Range<T> other)
        {
            return other.To.CompareTo(From) > 0 && other.From.CompareTo(To) < 0;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0} - {1}", From, To);
        }

        public override int GetHashCode()
        {
            int hash = 23;
            hash = hash * 37 + From.GetHashCode();
            hash = hash * 37 + To.GetHashCode();
            return hash;
        }

        #region IComparable<Range<T>> Members

        /// <summary>
        /// Returns -1 if this range's From is less than the other, 1 if greater.
        /// If both are equal, To is compared, 1 if greater, -1 if less.
        /// 0 if both ranges are equal.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public int CompareTo(Range<T> other)
        {
            if (From.CompareTo(other.From) < 0)
                return -1;
            else if (From.CompareTo(other.From) > 0)
                return 1;
            else if (To.CompareTo(other.To) < 0)
                return -1;
            else if (To.CompareTo(other.To) > 0)
                return 1;
            else
                return 0;
        }

        #endregion
    }
}
