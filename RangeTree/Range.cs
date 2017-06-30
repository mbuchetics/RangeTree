using System;

namespace RangeTree
{
    /// <summary>
    /// Represents a range of values. Both values must be of the same type and comparable.
    /// </summary>
    /// <typeparam name="T">Type of the values.</typeparam>
    public class Range<T> : IComparable<Range<T>>
        where T : IComparable<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Range{T}"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public Range(T value)
            : this(value, value)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Range{T}"/> class.
        /// </summary>
        /// <param name="from">The range from (start).</param>
        /// <param name="to">The range to (end).</param>
        /// <exception cref="System.ArgumentOutOfRangeException">from cannot be larger than to</exception>
        public Range(T from, T to)
        {
            if (from.CompareTo(to) == 1)
            {
                throw new ArgumentOutOfRangeException($"{nameof(from)} cannot be larger than {nameof(to)}");
            }

            From = from;
            To = to;
        }

        /// <summary>
        /// Gets from.
        /// </summary>
        /// <value>
        /// From.
        /// </value>
        public T From { get; }

        /// <summary>
        /// Gets to.
        /// </summary>
        /// <value>
        /// To.
        /// </value>
        public T To { get; }

        /// <summary>
        /// Determines whether the value is contained in the range. Border values are considered inside.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if [contains] [the specified value]; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(T value)
        {
            return value.CompareTo(From) >= 0 && value.CompareTo(To) <= 0;
        }

        /// <summary>
        /// Determines whether the value is contained in the range. Border values are considered outside.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if [contains] [the specified value]; otherwise, <c>false</c>.
        /// </returns>
        public bool ContainsExclusive(T value)
        {
            return value.CompareTo(From) > 0 && value.CompareTo(To) < 0;
        }

        /// <summary>
        /// Whether two ranges intersect each other.
        /// </summary>
        /// <param name="other">The <see cref="Range{T}"/> to check intersection with.</param>
        /// <returns>[true] if intesecting, otherwise [false]</returns>
        public bool Intersects(Range<T> other)
        {
            return other.To.CompareTo(From) >= 0 && other.From.CompareTo(To) <= 0;
        }

        /// <summary>
        /// Whether two ranges intersect each other.
        /// </summary>
        /// <param name="other">The <see cref="Range{T}"/> to check intersection with.</param>
        /// <returns>[true] if intesecting, otherwise [false]</returns>
        public bool IntersectsExclusive(Range<T> other)
        {
            return other.To.CompareTo(From) > 0 && other.From.CompareTo(To) < 0;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>
        /// true if the specified object is equal to the current object; otherwise, false.
        /// </returns>
        public override bool Equals(object obj)
        {
            Range<T> range = obj as Range<T>;
            if (range == null)
            {
                return false;
            }

            return range.From.Equals(From) && range.To.Equals(To);
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0} - {1}", From, To);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            int hash = 23;
            hash = (hash * 37) + From.GetHashCode();
            hash = (hash * 37) + To.GetHashCode();
            return hash;
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="other">An object to compare with this instance.</param>
        /// <returns>
        /// Returns -1 if this range's From is less than the other, 1 if greater. If both are equal, To is compared, 1 if greater, -1 if less. 0 if both ranges are equal.
        /// </returns>
        public int CompareTo(Range<T> other)
        {
            if (From.CompareTo(other.From) < 0)
            {
                return -1;
            }
            else if (From.CompareTo(other.From) > 0)
            {
                return 1;
            }
            else if (To.CompareTo(other.To) < 0)
            {
                return -1;
            }
            else if (To.CompareTo(other.To) > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
