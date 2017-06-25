using RangeTree;
using RangeTree.Examples;
using Xunit;

namespace RangeTreeTests
{
    public class ReadmeExampleTests
    {
        [Fact]
        public void Query_CreateTreeAndExecuteQuery_ExpectCorrectElementsToBeReturned()
        {
            var tree = new RangeTree<int, RangeItem>(new RangeItemComparer());

            tree.Add(new RangeItem(0, 10, "1"));
            tree.Add(new RangeItem(20, 30, "2"));
            tree.Add(new RangeItem(15, 17, "3"));
            tree.Add(new RangeItem(25, 35, "4"));

            var results1 = tree.Query(5);
            Assert.Equal(1, results1.Count);
            Assert.Equal(new RangeItem(0, 10, "1"), results1[0]);

            var results2 = tree.Query(10);
            Assert.Equal(1, results2.Count);
            Assert.Equal(new RangeItem(0, 10, "1"), results2[0]);

            var results3 = tree.Query(29);
            Assert.Equal(2, results3.Count);
            Assert.Equal(new RangeItem(20, 30, "2"), results3[0]);
            Assert.Equal(new RangeItem(25, 35, "4"), results3[1]);

            var results4 = tree.Query(new Range<int>(5, 15));
            Assert.Equal(2, results4.Count);
            Assert.Equal(new RangeItem(15, 17, "3"), results4[0]);
            Assert.Equal(new RangeItem(0, 10, "1"), results4[1]);
        }
    }
}