using NUnit.Framework;
using RangeTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RangeTreeTests
{
    [TestFixture]
    public class ComparerTests
    {
        [Test]
        public void AddingAnItem_FromIsLargerThanTo_ShouldThrowException()
        {
            var comparer = Comparer<int>.Create((x, y) => x - y);
            var tree = new RangeTree<int, string>(comparer);

            TestDelegate act = () => tree.Add(2, 0, "FOO");

            Assert.Throws<ArgumentOutOfRangeException>(act);
        }

        [Test]
        public void CreatingTreeWithNullComparer_AddingAnItem_ShouldNotThrowException()
        {
            var tree = new RangeTree<int, string>(null);

            TestDelegate act = () => tree.Add(0, 1, "FOO");

            Assert.DoesNotThrow(act);
        }
    }
}
