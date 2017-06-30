using System;
using System.Collections.Generic;
using System.Linq;

namespace RangeTree
{
    /// <summary>
    /// The standard range tree implementation. Keeps a root node and forwards all queries to it. Whenenver new items are added or items are removed, the tree goes "out of sync" and is rebuild when it's queried next.
    /// </summary>
    /// <typeparam name="TKey">The type of the range.</typeparam>
    /// <typeparam name="T">The type of the data items.</typeparam>
    public class RangeTree<TKey, T> : IRangeTree<TKey, T>
        where TKey : IComparable<TKey>
        where T : IRangeProvider<TKey>
    {
        private RangeTreeNode<TKey, T> root;
        private List<T> items;
        private bool isInSync;
        private bool autoRebuild;
        private IComparer<T> rangeComparer;

        /// <summary>
        /// Gets a value indicating whether the tree is currently in sync or not. If it is "out of sync"  you can either rebuild it manually (call Rebuild) or let it rebuild automatically when you query it next.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is in synchronize; otherwise, <c>false</c>.
        /// </value>
        public bool IsInSync
        {
            get { return isInSync; }
        }

        /// <summary>
        /// Gets all of the tree items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public IEnumerable<T> Items
        {
            get { return items; }
        }

        /// <summary>
        /// Gets the count of all tree items.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int Count
        {
            get { return items.Count; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether rebuild automatically. Defaults to true.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [automatic rebuild]; otherwise, <c>false</c>.
        /// </value>
        public bool AutoRebuild
        {
            get { return autoRebuild; }
            set { autoRebuild = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeTree{TKey, T}"/> class.
        /// </summary>
        /// <param name="rangeComparer">The range comparer.</param>
        public RangeTree(IComparer<T> rangeComparer)
        {
            this.rangeComparer = rangeComparer;
            root = new RangeTreeNode<TKey, T>(rangeComparer);            
            items = new List<T>();
            isInSync = true;
            autoRebuild = true;    
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeTree{TKey, T}"/> class.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="rangeComparer">The range comparer.</param>
        public RangeTree(IEnumerable<T> items, IComparer<T> rangeComparer)
        {
            this.rangeComparer = rangeComparer;
            root = new RangeTreeNode<TKey, T>(items, rangeComparer);
            this.items = items.ToList();
            isInSync = true;
            autoRebuild = true;
        }

        /// <summary>
        /// Performans a "stab" query with a single value. All items with overlapping ranges are returned.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The resulting <see cref="List{T}"/></returns>
        public List<T> Query(TKey value)
        {
            if (!isInSync && autoRebuild)
            {
                Rebuild();
            }

            return root.Query(value);
        }

        /// <summary>
        /// Performans a range query. All items with overlapping ranges are returned.
        /// </summary>
        /// <param name="range">The range.</param>
        /// <returns>The resulting <see cref="List{T}"/></returns>
        public List<T> Query(Range<TKey> range)
        {
            if (!isInSync && autoRebuild)
            {
                Rebuild();
            }

            return root.Query(range);
        }

        /// <summary>
        /// Rebuilds the tree if it is out of sync.
        /// </summary>
        public void Rebuild()
        {
            if (isInSync)
            {
                return;
            }

            root = new RangeTreeNode<TKey, T>(items, rangeComparer);
            isInSync = true;
        }

        /// <summary>
        /// Adds the specified item. Tree will go out of sync.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Add(T item)
        {
            isInSync = false;
            items.Add(item);
        }

        /// <summary>
        /// Adds the specified items. Tree will go out of sync.
        /// </summary>
        /// <param name="items">The items.</param>
        public void Add(IEnumerable<T> items)
        {
            isInSync = false;
            this.items.AddRange(items);
        }

        /// <summary>
        /// Removes the specified item. Tree will go out of sync.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Remove(T item)
        {
            isInSync = false;
            items.Remove(item);
        }

        /// <summary>
        /// Removes the specified items. Tree will go out of sync.
        /// </summary>
        /// <param name="items">The items.</param>
        public void Remove(IEnumerable<T> items)
        {
            isInSync = false;

            foreach (var item in items)
            {
                this.items.Remove(item);
            }
        }

        /// <summary>
        /// Clears the tree (removes all items).
        /// </summary>
        public void Clear()
        {
            root = new RangeTreeNode<TKey, T>(rangeComparer);            
            items = new List<T>();
            isInSync = true;
        }
    }
}
