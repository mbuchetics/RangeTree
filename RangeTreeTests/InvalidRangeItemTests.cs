namespace RangeTreeTests
{
    using System;

    using NUnit.Framework;

    using RangeTreeExamples;

    [TestFixture]
    public class InvalidRangeItemTests
    {
        [Test]
        public void ConstructNewRange_ProvideInvalidRange_ExpectArgumentOutOfRangeException()
        {
            // Act & Assert
            Assert.That(() => new RangeItem(2, 1), Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => new RangeItem(2, 1, ""), Throws.InstanceOf<ArgumentOutOfRangeException>());
        }
    }
}