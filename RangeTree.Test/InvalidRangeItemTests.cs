using System;
using NUnit.Framework;
using RangeTree.Examples;

namespace RangeTree.Test
{
    [TestFixture]
    public class InvalidRangeItemTests
    {
        [Test]
        public void ConstructNewRange_ProvideInvalidRange_ExpectArgumentOutOfRangeException()
        {
            // Act & Assert
            Assert.That(() => new RangeItem(2, 1), Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => new RangeItem(2, 1, string.Empty), Throws.InstanceOf<ArgumentOutOfRangeException>());
        }
    }
}