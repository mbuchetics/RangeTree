using System;
using System.Collections.Generic;
using IntervalTree;

namespace RangeTree
{
    [Obsolete("This class has been renamed to IntervalTree. Please switch to that class. Starting with version 3.0.0, this class will be removed.")]
    public class RangeTree<TKey, TValue> : IntervalTree<TKey, TValue>, IRangeTree<TKey, TValue>
    {
        public RangeTree()
        {
        }

        public RangeTree(IComparer<TKey> comparer) : base(comparer)
        {
        }
    }
}
