using System;
using System.Collections.Generic;
using IntervalTree;
using NUnit.Framework;

namespace IntervalTreeTests
{
    [TestFixture]
    public class ComparerTests
    {
        [Test]
        public void AddingAnItem_FromIsLargerThanTo_ShouldThrowException()
        {
            var comparer = Comparer<int>.Create((x, y) => x - y);
            var tree = new IntervalTree<int, string>(comparer);

            Assert.That(() => tree.Add(2, 0, "FOO"), Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void CreatingTreeWithNullComparer_AddingAnItem_ShouldNotThrowException()
        {
            var tree = new IntervalTree<int, string>(null);

            Assert.That(() => tree.Add(0, 1, "FOO"), Throws.Nothing);
        }
    }
}
