using NUnit.Framework;
using RangeTree.Examples;

namespace RangeTree.Test
{
    [TestFixture]
    public class EmptyTreeTests
    {
        [Test]
        public void QueryEmptyTree_RemoveAllElementsFromTree_ExpectNoException()
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

        [Test]
        public void QueryEmptyTree_CreateEmptyTree_ExpectNoException()
        {
            var standardItemComparer = new RangeItemComparer();
            var rangeTree = new RangeTree<int, RangeItem>(standardItemComparer);

            // Act & Assert
            Assert.That(() => rangeTree.Query(2), Throws.Nothing);
        }
    }
}