using System;
using System.Collections.Generic;
using IntervalTree;

namespace RangeTree
{
    [Obsolete("This class has been renamed to IntervalTreeNode. Please switch to that class. Starting with version 3.0.0, this class will be removed.")]
    internal class RangeTreeNode<TKey, TValue> : IntervalTreeNode<TKey, TValue>
    {
        public RangeTreeNode(IComparer<TKey> comparer) : base(comparer)
        {
        }

        public RangeTreeNode(IList<IntervalTree.RangeValuePair<TKey, TValue>> items, IComparer<TKey> comparer) : base(items, comparer)
        {
        }
    }
}