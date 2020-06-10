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
            Assert.That(emptyTree, Is.Not.Null);
        }

        [Test]
        public void GetIntervalByExactEndTime()
        {
            var tree = new RangeTree<DateTime, int>();
            tree.Add(ZERO, ZERO.AddHours(1), 100);

            var result = tree.Query(ZERO.AddHours(1)).ToList();
            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public void GetIntervalByExactStartTime()
        {
            var tree = new RangeTree<DateTime, int>();
            tree.Add(ZERO, ZERO.AddHours(1), 100);

            var result = tree.Query(ZERO).ToList();
            Assert.That(result.Count, Is.EqualTo(1));
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
            Assert.That(result.Count, Is.EqualTo(3));
        }

        [Test]
        public void TestSeparateIntervals()
        {
            var tree = new RangeTree<DateTime, int>();
            tree.Add(ZERO, ZERO.AddHours(10), 100);
            tree.Add(ZERO.AddHours(20), ZERO.AddHours(30), 200);

            var result = tree.Query(ZERO.AddHours(5)).ToList();
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0], Is.EqualTo(100));
        }

        [Test]
        public void TwoIntersectingIntervals()
        {
            var tree = new RangeTree<DateTime, int>();
            tree.Add(ZERO, ZERO.AddHours(10), 100);
            tree.Add(ZERO.AddHours(3), ZERO.AddHours(30), 200);

            var result = tree.Query(ZERO.AddHours(5)).ToList();
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0], Is.EqualTo(100));
            Assert.That(result[1], Is.EqualTo(200));
        }
    }
}