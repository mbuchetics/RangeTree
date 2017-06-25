namespace RangeTree.Examples
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

        protected bool Equals(RangeItem other)
        {
            return string.Equals(Text, other.Text) && Range.Equals(other.Range);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return Equals((RangeItem)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Text != null ? Text.GetHashCode() : 0) * 397) ^ Range.GetHashCode();
            }
        }

        public static bool operator ==(RangeItem left, RangeItem right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(RangeItem left, RangeItem right)
        {
            return !Equals(left, right);
        }
    }
}
