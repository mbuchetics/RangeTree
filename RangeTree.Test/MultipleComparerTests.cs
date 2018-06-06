using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using RangeTree.Examples;

namespace RangeTree.Test
{
    [TestFixture]
    public class MultipleComparerTests
    {
        private static object GetComparerViaReflection(RangeTree<int, RangeItem> rangeTree1)
        {
            var bindFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static;
            var rootFieldInfo = typeof(RangeTree<int, RangeItem>).GetField("root", bindFlags);
            var root = (RangeTreeNode<int, RangeItem>) rootFieldInfo.GetValue(rangeTree1);
            var comparerFieldInfo = typeof(RangeTreeNode<int, RangeItem>).GetField("rangeComparer", bindFlags);
            var comparer = comparerFieldInfo.GetValue(root);
            return comparer;
        }

        private class ReverseItemComparer : IComparer<RangeItem>
        {
            public int Compare(RangeItem x, RangeItem y)
            {
                return y.Range.CompareTo(x.Range);
            }
        }

        [Test]
        public void CreateTwoTrees_ProvideDifferentComparers_ExpectBothToHaveTheComparersFromConstruction()
        {
            var reverseItemComparer = new ReverseItemComparer();
            var standardItemComparer = new RangeItemComparer();

            var rangeTree1 = new RangeTree<int, RangeItem>(standardItemComparer);
            var rangeTree2 = new RangeTree<int, RangeItem>(reverseItemComparer);

            var comparerRangeTree1 = GetComparerViaReflection(rangeTree1);
            var comparerRangeTree2 = GetComparerViaReflection(rangeTree2);

            Assert.That(standardItemComparer, Is.EqualTo(comparerRangeTree1));
            Assert.That(reverseItemComparer, Is.EqualTo(comparerRangeTree2));
        }
    }
}