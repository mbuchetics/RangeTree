using System;
using System.Linq;
using NUnit.Framework;
using RangeTree;

namespace RangeTreeTests
{
    [TestFixture]
    internal class TreeOfDateTimeTests
    {
        private static readonly DateTime ZERO = new DateTime(2001, 01, 01, 10, 00, 00);

        [Test]
        public void BuildEmptyIntervalTree()
        {
            var emptyTree = new RangeTree<DateTime, int>();
            Assert.Pass();
        }

        [Test]
        public void CreateEmptyIntervalTree()
        {
            var emptyTree = new RangeTree<DateTime, int>();
            Assert.IsNotNull(emptyTree);
        }

        [Test]
        public void GetIntervalByExactEndTime()
        {
            var tree = new RangeTree<DateTime, int>();
            tree.Add(ZERO, ZERO.AddHours(1), 100);

            var result = tree.Query(ZERO.AddHours(1)).ToList();
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void GetIntervalByExactStartTime()
        {
            var tree = new RangeTree<DateTime, int>();
            tree.Add(ZERO, ZERO.AddHours(1), 100);

            var result = tree.Query(ZERO).ToList();
            Assert.AreEqual(1, result.Count);
        }

        /// <summary>
        ///     0-----5-----10------15--------20
        ///     |=====100====|
        ///     |==200=|
        ///     |====300==========|
        /// </summary>
        [Test]
        public void OverlapOnExactEndAndStart_AssertCount()
        {
            var tree = new RangeTree<DateTime, int>();
            tree.Add(ZERO, ZERO.AddHours(10), 100);
            tree.Add(ZERO.AddHours(10), ZERO.AddHours(15), 200);
            tree.Add(ZERO.AddHours(10), ZERO.AddHours(20), 200);

            var result = tree.Query(ZERO.AddHours(10)).ToList();
            Assert.AreEqual(3, result.Count);
        }

        [Test]
        public void TestSeparateIntervals()
        {
            var tree = new RangeTree<DateTime, int>();
            tree.Add(ZERO, ZERO.AddHours(10), 100);
            tree.Add(ZERO.AddHours(20), ZERO.AddHours(30), 200);

            var result = tree.Query(ZERO.AddHours(5)).ToList();
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(100, result[0]);
        }

        [Test]
        public void TwoIntersectingIntervals()
        {
            var tree = new RangeTree<DateTime, int>();
            tree.Add(ZERO, ZERO.AddHours(10), 100);
            tree.Add(ZERO.AddHours(3), ZERO.AddHours(30), 200);

            var result = tree.Query(ZERO.AddHours(5)).ToList();
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(100, result[0]);
            Assert.AreEqual(200, result[1]);
        }
    }
}