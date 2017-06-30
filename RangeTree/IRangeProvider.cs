using System;

namespace RangeTree
{
    /// <summary>
    /// Interface for classes which provide a range.
    /// </summary>
    /// <typeparam name="T">The generic <see cref="IComparable{T}"/></typeparam>
    public interface IRangeProvider<T> 
        where T : IComparable<T>
    {
        /// <summary>
        /// Gets the range.
        /// </summary>
        /// <value>
        /// The range.
        /// </value>
        Range<T> Range { get; }
    }
}
