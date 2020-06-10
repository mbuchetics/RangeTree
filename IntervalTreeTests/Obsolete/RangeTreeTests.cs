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

            Assert.That(min, Is.EqualTo(1));
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

            Assert.That(min, Is.EqualTo(1));
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

            Assert.That(max, Is.EqualTo(4));
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

            Assert.That(max, Is.EqualTo(6));
        }
    }
}
