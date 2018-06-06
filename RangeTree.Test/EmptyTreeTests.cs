using NUnit.Framework;
using RangeTree.Examples;

namespace RangeTree.Test
{
    [TestFixture]
    public class EmptyTreeTests
    {
        [Test]
        public void CreateTwoTrees_ProvideDifferentComparers_ExpectBothToHaveTheComparersFromConstruction()
        {
            // Arrang
            var standardItemComparer = new RangeItemComparer();
            var rangeTree = new RangeTree<int, RangeItem>(standardItemComparer);
            var item = new RangeItem(1, 3);
            rangeTree.Add(item);
            rangeTree.Remove(item);

            // Act & Assert
            Assert.That(() => rangeTree.Query(2), Throws.Nothing);
        }
    }
}