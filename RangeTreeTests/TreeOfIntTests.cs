using System.Linq;
using NUnit.Framework;
using RangeTree;

namespace RangeTreeTests
{
    [TestFixture]
    internal class TreeOfIntTests
    {
        [Test]
        public void BuildEmptyIntervalTree()
        {
            var emptyTree = new RangeTree<int, int>();
            emptyTree.Rebuild();
            Assert.Pass();
        }

        [Test]
        public void CreateEmptyIntervalTree()
        {
            var emptyTree = new RangeTree<int, int>();
            Assert.IsNotNull(emptyTree);
        }

        [Test]
        public void TestSeparateIntervals()
        {
            var tree = new RangeTree<int, int>();
            tree.Add(0, 10, 100);
            tree.Add(20, 30, 200);

            var result = tree.Query(5).ToList();
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(100, result[0]);
        }

        [Test]
        public void TwoIntersectingIntervals()
        {
            var tree = new RangeTree<int, int>();
            tree.Add(0, 10, 100);
            tree.Add(3, 30, 200);

            var result = tree.Query(5).ToList();
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(100, result[0]);
            Assert.AreEqual(200, result[1]);
        }

        [Test]
        public void QueryOutOfSyncTree_ExpectObsoleteResults()
        {
            var tree = new RangeTree<int, int>();
            tree.AutoRebuild = false;
            tree.Add(0, 10, 100);
            tree.Add(3, 30, 200);

            var result = tree.Query(5).ToList();
            Assert.AreEqual(0, result.Count);
        }
    }
}