using System.Collections.Generic;

namespace RangeTree
{
    /// <summary>
    /// Range tree interface.
    /// </summary>
    /// <typeparam name="TKey">The type of the range.</typeparam>
    /// <typeparam name="TValue">The type of the data items.</typeparam>
    public interface IRangeTree<TKey, TValue> : IEnumerable<RangeValuePair<TKey, TValue>>
    {
        /// <summary>
        /// Returns all items contained in the tree.
        /// </summary>
        IEnumerable<TValue> Values { get; }

        /// <summary>
        /// Gets the number of elements contained in the tree.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Whether the tree should be rebuild automatically. Defaults to true.
        ///
        /// Keep in mind, that if you disable auto-rebuild, you have to call Rebuild manually,
        /// after updating items in the tree or you will query an obsolete tree.
        /// </summary>
        bool AutoRebuild { get; set; }

        /// <summary>
        /// Rebuilds the tree if it is out of sync.
        /// </summary>
        void Rebuild();

        /// <summary>
        /// Whether the tree is currently in sync or not. If it is "out of sync"
        /// you can either rebuild it manually (call Rebuild) or let it rebuild
        /// automatically when you query it next.
        ///
        /// Most of the time, you can simply ignore this, as the tree keeps track of whether it needs rebuilding.
        /// </summary>
        bool IsInSync { get; }

        /// <summary>
        /// Performs a point query with a single value. All items with overlapping ranges are returned.
        /// </summary>
        IEnumerable<TValue> Query(TKey value);

        /// <summary>
        /// Performs a range query. All items with overlapping ranges are returned.
        /// </summary>
        IEnumerable<TValue> Query(TKey from, TKey to);
        
        /// <summary>
        /// Adds the specified item.
        /// </summary>
        void Add(TKey from, TKey to, TValue value);
        
        /// <summary>
        /// Removes the specified item.
        /// </summary>
        void Remove(TValue item);

        /// <summary>
        /// Removes the specified items.
        /// </summary>
        void Remove(IEnumerable<TValue> items);

        /// <summary>
        /// Removes all elements from the range tree.
        /// </summary>
        void Clear();
    }
}
