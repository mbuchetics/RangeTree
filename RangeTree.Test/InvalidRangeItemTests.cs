namespace RangeTreeTests
{
    using System;
    using RangeTree.Examples;
    using Xunit;

    public class InvalidRangeItemTests
    {
        [Fact]
        public void ConstructNewRange_ProvideInvalidRange_ExpectArgumentOutOfRangeException()
        {
            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new RangeItem(2, 1));
            Assert.Throws<ArgumentOutOfRangeException>(() => new RangeItem(2, 1, string.Empty));
        }
    }
}