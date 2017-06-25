using System;

namespace RangeTree
{
    /// <summary>
    /// Interface for classes which provide a range.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRangeProvider<T> where T : IComparable<T>
    {
        Range<T> Range { get; }
    }
}
