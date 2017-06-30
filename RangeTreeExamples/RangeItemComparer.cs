namespace RangeTree.Examples
{
    using System.Collections.Generic;

    /// <summary>
    /// Compares two range items by comparing their ranges.
    /// </summary>
    public class RangeItemComparer : IComparer<RangeItem>
    {
        /// <summary>
        /// Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.
        /// </summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <returns>
        /// A signed integer that indicates the relative values of <paramref name="x" /> and <paramref name="y" />, as shown in the following table.Value Meaning Less than zero<paramref name="x" /> is less than <paramref name="y" />.Zero<paramref name="x" /> equals <paramref name="y" />.Greater than zero<paramref name="x" /> is greater than <paramref name="y" />.
        /// </returns>
        public int Compare(RangeItem x, RangeItem y)
        {
            return x.Range.CompareTo(y.Range);
        }
    }
}