namespace RangeTreeTests
{
    using MB.Algodat;

    using NUnit.Framework;

    using RangeTreeExamples;

    [TestFixture]
    public class ReadmeExampleTests
    {
        [Test]
        public void Query_CreateTreeAndExecuteQuery_ExpectCorrectElementsToBeReturned()
        {
            var tree = new RangeTree<int, RangeItem>(new RangeItemComparer());

            tree.Add(new RangeItem(0, 10, "1"));
            tree.Add(new RangeItem(20, 30, "2"));
            tree.Add(new RangeItem(15, 17, "3"));
            tree.Add(new RangeItem(25, 35, "4"));

            var results1 = tree.Query(5);
            Assert.That(results1.Count, Is.EqualTo(1));
            Assert.That(results1[0], Is.EqualTo(new RangeItem(0, 10, "1")));

            var results2 = tree.Query(10);
            Assert.That(results2.Count, Is.EqualTo(1));
            Assert.That(results2[0], Is.EqualTo(new RangeItem(0, 10, "1")));

            var results3 = tree.Query(29);
            Assert.That(results3.Count, Is.EqualTo(2));
            Assert.That(results3[0], Is.EqualTo(new RangeItem(20, 30, "2")));
            Assert.That(results3[1], Is.EqualTo(new RangeItem(25, 35, "4")));

            var results4 = tree.Query(new Range<int>(5, 15));
            Assert.That(results4.Count, Is.EqualTo(2));
            Assert.That(results4[0], Is.EqualTo(new RangeItem(15, 17, "3")));
            Assert.That(results4[1], Is.EqualTo(new RangeItem(0, 10, "1")));
        }
    }
}