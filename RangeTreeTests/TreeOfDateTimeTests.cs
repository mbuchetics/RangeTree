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
    class TreeOfDateTimeTests
    {
        private static readonly DateTime ZERO = new DateTime(2001, 01, 01, 10, 00, 00);

        [Test]
        public void CreateEmptyIntervalTree()
        {
            var emptyTree = new RangeTree<DateTime, int>();
            Assert.IsNotNull(emptyTree);
        }

        [Test]
        public void BuildEmptyIntervalTree()
        {
            var emptyTree = new RangeTree<DateTime, int>();
            emptyTree.Rebuild();
            Assert.Pass();
        }

        [Test]
        public void TestSeparateIntervals()
        {
            var tree = new RangeTree<DateTime, int>();
            tree.Add(ZERO, ZERO.AddHours(10), 100);
            tree.Add(ZERO.AddHours(20), ZERO.AddHours(30), 200);

            var result = tree[ZERO.AddHours(5)].ToList();
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(100, result[0].Value);
        }


        /// <summary>
        /// 0-----5-----10------15--------20
        /// |=====100====|
        ///              |==200=|
        ///              |====300==========|
        /// </summary>
        [Test]
        public void OverlapOnExactEndAndStart_AssertCount()
        {
            var tree = new RangeTree<DateTime, int>();
            tree.Add(ZERO, ZERO.AddHours(10), 100);
            tree.Add(ZERO.AddHours(10), ZERO.AddHours(15), 200);
            tree.Add(ZERO.AddHours(10), ZERO.AddHours(20), 200);

            var result = tree[ZERO.AddHours(10)].ToList();
            Assert.AreEqual(3, result.Count);
        }


        [Test]
        public void GetIntervalByExactStartTime()
        {
            var tree = new RangeTree<DateTime, int>();
            tree.Add(ZERO, ZERO.AddHours(1), 100);

            var result = tree[ZERO].ToList();
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void GetIntervalByExactEndTime()
        {
            var tree = new RangeTree<DateTime, int>();
            tree.Add(ZERO, ZERO.AddHours(1), 100);

            var result = tree[ZERO.AddHours(1)].ToList();
            Assert.AreEqual(1, result.Count);
        }



        [Test]
        public void TwoIntersectingIntervals()
        {
            var tree = new RangeTree<DateTime, int>();
            tree.Add(ZERO, ZERO.AddHours(10), 100);
            tree.Add(ZERO.AddHours(3), ZERO.AddHours(30), 200);

            var result = tree[ZERO.AddHours(5)].ToList();
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(100, result[0].Value);
            Assert.AreEqual(200, result[1].Value);
        }
    }
}
