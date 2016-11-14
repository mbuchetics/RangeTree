using System;
using System.Collections.Generic;

namespace MB.Algodat
{
    /// <summary>
    /// Represents a range of values.
    /// Both values must be of the same type and comparable.
    /// </summary>
    /// <typeparam name="TKey">Type of the values.</typeparam>
    public struct RangeValuePair<TKey, TValue>
    {
        public TKey From { get; }
        public TKey To { get; }
        public TValue Value { get; }

        /// <summary>
        /// Initializes a new <see cref="RangeValuePair&lt;TKey, TValue&gt;"/> instance.
        /// </summary>
        public RangeValuePair(TKey from, TKey to, TValue value) : this()
        {
            From = from;
            To = to;
            Value = value;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("[{0} - {1}] {2}", From, To, Value);
        }

        public override int GetHashCode()
        {
            var hash = 23;
            if (From != null)
                hash = hash * 37 + From.GetHashCode();
            if (To != null)
                hash = hash * 37 + To.GetHashCode();
            if (Value != null)
                hash = hash * 37 + Value.GetHashCode();
            return hash;
        }
    }
}
