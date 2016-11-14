using MB.Algodat;
using NUnit.Framework;
using System;
using System.Linq;

namespace RangeTreeTests
{

    [TestFixture]
    public class MultipleComparerTests
    {
        [Test]
        public void CreateTwoTrees_ProvideDifferentComparers_ExpectBothToHaveTheComparersFromConstruction()
        {
            var tree = new RangeTree<string, string>(StringComparer.Ordinal)
            {
                { "a", "e", "value1" },
                { "B", "D", "value2" },
            };
            var results = tree.Query("c").ToArray();
            Assert.That(results.Length, Is.EqualTo(1));
            Assert.That(results[0], Is.EqualTo("value1"));

            tree = new RangeTree<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "a", "e", "value1" },
                { "B", "D", "value2" },
            };
            results = tree.Query("c").ToArray();
            Assert.That(results.Length, Is.EqualTo(2));
            Assert.That(results[0], Is.EqualTo("value1"));
            Assert.That(results[1], Is.EqualTo("value2"));
        }
    }
}
