using System;

namespace RangeTree.Examples
{
    /// <summary>
    /// A simple example cass, which contains an integer range and a text property.
    /// </summary>
    public class RangeItem : IRangeProvider<int>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RangeItem"/> class.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        public RangeItem(int a, int b)
        {
            Range = new Range<int>(a, b);
            Text = a + " - " + b;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeItem"/> class.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="text">The text.</param>
        public RangeItem(int a, int b, string text)
        {
            Range = new Range<int>(a, b);
            Text = text;
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the range.
        /// </summary>
        /// <value>
        /// The range.
        /// </value>
        public Range<int> Range
        {
            get;
            set;
        }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0} ({1} - {2})", Text, Range.From, Range.To);
        }

        /// <summary>
        /// Determines whether the specified <see cref="RangeItem" />, is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="RangeItem" /> to compare with this instance.</param>
        /// <returns>
        ///     <c>true</c> if the specified <see cref="RangeItem" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        protected bool Equals(RangeItem other)
        {
            return string.Equals(Text, other.Text) && Range.Equals(other.Range);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((RangeItem)obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return ((Text != null ? Text.GetHashCode() : 0) * 397) ^ Range.GetHashCode();
            }
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(RangeItem left, RangeItem right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(RangeItem left, RangeItem right)
        {
            return !Equals(left, right);
        }
    }
}
