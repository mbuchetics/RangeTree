namespace RangeTree.Examples
{
    using System.Collections.Generic;

    /// <summary>
    /// Compares two range items by comparing their ranges.
    /// </summary>
    public class RangeItemComparer : IComparer<RangeItem>
    {
        public int Compare(RangeItem x, RangeItem y)
        {
            return x.Range.CompareTo(y.Range);
        }
    }
}