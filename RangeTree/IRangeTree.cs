using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Algodat
{
    /// <summary>
    /// Range tree interface.
    /// </summary>
    /// <typeparam name="TKey">The type of the range.</typeparam>
    /// <typeparam name="TValue">The type of the data items.</typeparam>
    public interface IRangeTree<TKey, TValue> : IEnumerable<RangeValuePair<TKey, TValue>>
    {
        IEnumerable<TValue> Values { get; }
        int Count { get; }

        IEnumerable<TValue> Query(TKey value);
        IEnumerable<TValue> Query(TKey from, TKey to);

        void Rebuild();
        void Add(TKey from, TKey to, TValue value);
        void Remove(TValue item);
        void Remove(IEnumerable<TValue> items);
        void Clear();
    }
}
