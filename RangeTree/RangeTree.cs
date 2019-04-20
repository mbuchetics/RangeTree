using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RangeTree
{
    /// <summary>
    /// The standard range tree implementation. Keeps a root node and forwards all queries to it.
    /// Whenever new items are added or items are removed, the tree goes "out of sync" and is rebuild when its queried next.
    /// </summary>
    /// <typeparam name="TKey">The type of the range.</typeparam>
    /// <typeparam name="TValue">The type of the data items.</typeparam>
    public class RangeTree<TKey, TValue> : IRangeTree<TKey, TValue>
    {
        private RangeTreeNode<TKey, TValue> root;
        private List<RangeValuePair<TKey, TValue>> items;
        private readonly IComparer<TKey> comparer;

  
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public TKey Max => root.Max;

        public TKey Min => root.Min;

        public bool IsInSync { get; private set; }

        public bool AutoRebuild { get; set; }

        public IEnumerable<TValue> Values => items.Select(i => i.Value);

        public int Count => items.Count;

        /// <summary>
        /// Initializes an empty tree.
        /// </summary>
        public RangeTree() : this(Comparer<TKey>.Default) { }

        /// <summary>
        /// Initializes an empty tree.
        /// </summary>
        public RangeTree(IComparer<TKey> comparer)
        {
            this.comparer = comparer;
            AutoRebuild = true;
            IsInSync = true;
            root = new RangeTreeNode<TKey, TValue>(this.comparer);
            items = new List<RangeValuePair<TKey, TValue>>();
        }

        public IEnumerable<TValue> Query(TKey value)
        {
            if (!IsInSync && AutoRebuild)
                Rebuild();

            return root.Query(value);
        }

        public IEnumerable<TValue> Query(TKey from, TKey to)
        {
            if (!IsInSync && AutoRebuild)
                Rebuild();

            return root.Query(from, to);
        }

        public void Rebuild()
        {
            if (IsInSync)
                return;

            if (items.Count > 0)
                root = new RangeTreeNode<TKey, TValue>(items, comparer);
            else
                root = new RangeTreeNode<TKey, TValue>(comparer);
            IsInSync = true;
        }

        public void Add(TKey from, TKey to, TValue value)
        {
            if (comparer.Compare(from, to) == 1)
                throw new ArgumentOutOfRangeException($"{nameof(from)} cannot be larger than {nameof(to)}");

            IsInSync = false;
            items.Add(new RangeValuePair<TKey, TValue>(from, to, value));
        }
        public void Remove(TValue value)
        {
            IsInSync = false;
            items = items.Where(l => !l.Value.Equals(value)).ToList();
        }

        public void Remove(IEnumerable<TValue> items)
        {
            IsInSync = false;            
            this.items = this.items.Where(l => !items.Contains(l.Value)).ToList();
        }

        public void Clear()
        {
            root = new RangeTreeNode<TKey, TValue>(comparer);
            items = new List<RangeValuePair<TKey, TValue>>();
            IsInSync = true;
        }
        public IEnumerator<RangeValuePair<TKey, TValue>> GetEnumerator()
        {
            if (!IsInSync && AutoRebuild)
                Rebuild();

            return items.GetEnumerator();
        }
    }
}
