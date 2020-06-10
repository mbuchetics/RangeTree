using System;
using IntervalTree;

namespace RangeTree
{
    /// <summary>
    /// The standard interval tree implementation. Keeps a root node and forwards all queries to it.
    /// Whenever new items are added or items are removed, the tree goes temporarily "out of sync", which means that the
    /// internal index is not updated immediately, but upon the next query operation.    
    /// </summary>
    /// <typeparam name="TKey">The type of the range.</typeparam>
    /// <typeparam name="TValue">The type of the data items.</typeparam>
    [Obsolete("This interface has been renamed to IIntervalTree. Please switch to that interface. Starting with version 3.0.0, this interface will be removed.")]
    public interface IRangeTree<TKey, TValue> : IIntervalTree<TKey, TValue>
    {
        
    }
}
