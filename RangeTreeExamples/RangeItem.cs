using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MB.Algodat;

namespace RangeTreeExamples
{
    /// <summary>
    /// A simple example cass, which contains an integer range
    /// and a text property.
    /// </summary>
    public class RangeItem : IRangeProvider<int>
    {
        public RangeItem(int a, int b)
        {
            Range = new Range<int>(a, b);
            Text = a + " - " + b;
        }

        public RangeItem(int a, int b, string text)
        {
            Range = new Range<int>(a, b);
            Text = text;
        }

        public string Text
        {
            get;
            set;
        }

        #region IRangeProvider<int> Members

        public Range<int> Range
        {
            get;
            set;
        }

        #endregion

        public override string ToString()
        {
            return string.Format("{0} ({1} - {2})", Text, Range.From, Range.To);
        }
    }

    /// <summary>
    /// Compares two range items by comparing their ranges.
    /// </summary>
    public class RangeItemComparer : IComparer<RangeItem>
    {
        #region IComparer<RangeItem> Members

        public int Compare(RangeItem x, RangeItem y)
        {
            return x.Range.CompareTo(y.Range);
        }

        #endregion
    }
}
