using System;
using System.Collections.Generic;

namespace RangeTree
{
    /// <summary>
    /// Range tree interface.
    /// </summary>
    /// <typeparam name="TKey">The type of the range.</typeparam>
    /// <typeparam name="T">The type of the data items.</typeparam>
    public interface IRangeTree<TKey, T>
        where TKey : IComparable<TKey>
        where T : IRangeProvider<TKey>
    {
        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        IEnumerable<T> Items { get; }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        int Count { get; }

        /// <summary>
        /// Queries the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        List<T> Query(TKey value);

        /// <summary>
        /// Queries the specified range.
        /// </summary>
        /// <param name="range">The range.</param>
        /// <returns></returns>
        List<T> Query(Range<TKey> range);

        /// <summary>
        /// Rebuilds this instance.
        /// </summary>
        void Rebuild();

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        void Add(T item);

        /// <summary>
        /// Adds the specified items.
        /// </summary>
        /// <param name="items">The items.</param>
        void Add(IEnumerable<T> items);

        /// <summary>
        /// Removes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        void Remove(T item);

        /// <summary>
        /// Removes the specified items.
        /// </summary>
        /// <param name="items">The items.</param>
        void Remove(IEnumerable<T> items);

        /// <summary>
        /// Clears this instance.
        /// </summary>
        void Clear();
    }
}
