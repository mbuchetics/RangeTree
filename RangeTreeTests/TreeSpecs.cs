using MB.Algodat;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RangeTreeTests
{

    [TestFixture]
    public class If_the_user_searches_for_overlapping_entries_in_an_interval_tree : Spec
    {
        private static IEnumerable<Tuple<int, int>> TestEntries()
        {
            yield return Tuple.Create(1400, 1500);
            yield return Tuple.Create(0100, 0130);
            yield return Tuple.Create(1700, 1800);
            yield return Tuple.Create(0230, 0240);
            yield return Tuple.Create(0530, 0540);
            yield return Tuple.Create(2330, 2400);
            yield return Tuple.Create(0700, 0800);
            yield return Tuple.Create(0900, 1000);
            yield return Tuple.Create(0000, 0100);
            yield return Tuple.Create(0540, 0700);
            yield return Tuple.Create(1800, 2130);
            yield return Tuple.Create(2130, 2131);
            yield return Tuple.Create(0200, 0230);
        }

        private static IEnumerable TestCases
        {
            get
            {
                yield return new TestCaseData(Tuple.Create(2000, 2300)).Returns(2);
                yield return new TestCaseData(Tuple.Create(0000, 0100)).Returns(2);
                yield return new TestCaseData(Tuple.Create(0000, 0000)).Returns(1);
                yield return new TestCaseData(Tuple.Create(0100, 0100)).Returns(2);
                yield return new TestCaseData(Tuple.Create(1000, 1100)).Returns(1);
                yield return new TestCaseData(Tuple.Create(1030, 1400)).Returns(1);
                yield return new TestCaseData(Tuple.Create(0150, 0155)).Returns(0);
                yield return new TestCaseData(Tuple.Create(2132, 2133)).Returns(0);
                yield return new TestCaseData(Tuple.Create(1030, 1350)).Returns(0);
                yield return new TestCaseData(Tuple.Create(0000, 2359)).Returns(13);
            }
        }

        [Test, TestCaseSource("TestCases")]
        public int CorrectQuery_BuiltInOrder(Tuple<int, int> value)
        {
            var tree = CreateTree(TestEntries().OrderBy(interval => interval.Item1));
            return tree
                .Query(value.Item1, value.Item2)
                .Count();
        }

        [Test, TestCaseSource("TestCases")]
        public int CorrectQuery_BuiltInReverseOrder(Tuple<int, int> value)
        {
            var tree = CreateTree(TestEntries().OrderBy(interval => interval.Item1).Reverse());
            return tree
                .Query(value.Item1, value.Item2)
                .Count();
        }

        [Test, TestCaseSource("TestCases")]
        public int CorrectQuery_BuiltRandomly(Tuple<int, int> value)
        {
            var tree = CreateTree(TestEntries());
            return tree
                .Query(value.Item1, value.Item2)
                .Count();
        }

        private static IRangeTree<int, string> CreateTree(IEnumerable<Tuple<int, int>> entries)
        {
            var tree = new RangeTree<int, string>();

            foreach (var interval in entries)
            {
                tree.Add(interval.Item1, interval.Item2, "value");
            }

            return tree;
        }
    }

    /// <summary>
    /// Abstract helper class to make nunit tests more readable.
    /// </summary>
    [DebuggerStepThrough, DebuggerNonUserCode]
    public class Spec
    {
        [DebuggerStepThrough]
        [OneTimeSetUp]
        public void SetUp()
        {
            EstablishContext();
            BecauseOf();
        }

        [DebuggerStepThrough]
        [OneTimeTearDown]
        public void TearDown()
        {
            Cleanup();
        }

        /// <summary>
        /// Test setup. Place your initialization code here.
        /// </summary>
        [DebuggerStepThrough]
        protected virtual void EstablishContext() { }

        /// <summary>
        /// Test run. Place the tested method / action here.
        /// </summary>
        [DebuggerStepThrough]
        protected virtual void BecauseOf() { }

        /// <summary>
        /// Test clean. Close/delete files, close database connections ..
        /// </summary>
        [DebuggerStepThrough]
        protected virtual void Cleanup() { }

        /// <summary>
        /// Creates an Action delegate.
        /// </summary>
        /// <param name="func">Method the shall be created as delegate.</param>
        /// <returns>A delegate of type <see cref="Action"/></returns>
        protected Action Invoking(Action func)
        {
            return func;
        }
    }
}
