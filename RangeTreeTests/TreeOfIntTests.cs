using MB.Algodat;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RangeTreeTests
{
    [TestFixture]
    class TreeOfIntTests
    {
        [Test]
        public void CreateEmptyIntervalTree()
        {
            var emptyTree = new RangeTree<int, int>();
            Assert.IsNotNull(emptyTree);
        }

        [Test]
        public void BuildEmptyIntervalTree()
        {
            var emptyTree = new RangeTree<int, int>();
            emptyTree.Rebuild();
            Assert.Pass();
        }

        [Test]
        public void TestSeparateIntervals()
        {
            var tree = new RangeTree<int, int>();
            tree.Add(0, 10, 100);
            tree.Add(20, 30, 200);

            var result = tree[5].ToList();
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(100, result[0].Value);
        }

        [Test]
        public void TwoIntersectingIntervals()
        {
            var tree = new RangeTree<int, int>();
            tree.Add(0, 10, 100);
            tree.Add(3, 30, 200);

            var result = tree[5].ToList();
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(100, result[0].Value);
            Assert.AreEqual(200, result[1].Value);
        }
    }
}
