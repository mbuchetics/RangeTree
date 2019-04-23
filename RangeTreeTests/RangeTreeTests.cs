using NUnit.Framework;
using RangeTree;
using System;
using System.Collections.Generic;
using System.Text;

namespace RangeTreeTests
{
    [TestFixture]
    public class RangeTreeTests
    {
        [Test]
        public void GettingMin_InnerItems()
        {
            var tree = new RangeTree<int, int>
            {
                { 1, 5, -1 },
                { 2, 5, -1 },
                { 3, 5, -1 },
            };

            var min = tree.Min;

            Assert.AreEqual(1, min);
        }

        [Test]
        public void GettingMin_LeftRecurse()
        {
            var tree = new RangeTree<int, int>
            {
                { 1, 2, -1 },
                { 3, 4, -1 }
            };

            var min = tree.Min;

            Assert.AreEqual(1, min);
        }

        [Test]
        public void GettingMax_InnerItems()
        {
            var tree = new RangeTree<int, int>
            {
                { 1, 2, -1 },
                { 1, 3, -1 },
                { 1, 4, -1 },
            };

            var max = tree.Max;

            Assert.AreEqual(4, max);
        }

        [Test]
        public void GettingMax_RightRecurse()
        {
            var tree = new RangeTree<int, int>
            {
                { 1, 2, -1 },
                { 3, 4, -1 },
                { 5, 6, -1 }
            };

            var max = tree.Max;

            Assert.AreEqual(6, max);
        }
    }
}
