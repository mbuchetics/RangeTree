namespace RangeTreeTests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using MB.Algodat;

    using NUnit.Framework;

    using RangeTreeExamples;

    [TestFixture]
    public class NonStaticComparerTests
    {
        private RangeTree<int, RangeItem> _tree;

        private RangeItemComparer _rangeItemComparer;

        private IEnumerable<RangeItem> _items;

        [SetUp]
        public void SetUp()
        {
            _rangeItemComparer = new RangeItemComparer();
            _items = new RangeItem[]
                         {
                             new RangeItem(0, 10, "1"),
                             new RangeItem(20, 30, "2"),
                             new RangeItem(15, 17, "3")
                         };
            _tree = new RangeTree<int, RangeItem>(_items, _rangeItemComparer);
        }

        [Test]
        public void CreateTree_ProvideComparer_ExpectComparerInAllNodes()
        {
            // Arrange

            // Act
            var nodes = GetAllNodes(_tree);
            var comparers = nodes.Select(GetComparerViaReflection);

            // Assert
            foreach (var comparer in comparers)
            {
                Assert.That(comparer, Is.EqualTo(_rangeItemComparer));
            }
        }

        [Test]
        public void CreateTree_GetAllNodes_ExpectAllNodesInAList()
        {
            // Arrange
            
            // Act
            var nodes = GetAllNodes(_tree);

            // Assert
            Assert.That(nodes.Count(), Is.EqualTo(3));
        }

        private static IEnumerable<RangeTreeNode<int, RangeItem>> GetAllNodes(RangeTree<int, RangeItem> rangeTree)
        {
            var bindFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static;
            var rootFieldInfo = typeof(RangeTree<int, RangeItem>).GetField("_root", bindFlags);
            var root = (RangeTreeNode<int, RangeItem>)rootFieldInfo.GetValue(rangeTree);
            yield return root;
            foreach (var rangeTreeNode in TraverseNode(root))
            {
                yield return rangeTreeNode;
            }            
        }

        private static IEnumerable<RangeTreeNode<int, RangeItem>> TraverseNode(RangeTreeNode<int, RangeItem> rangeTreeNode)
        {
            var bindFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static;
            var leftFieldInfo = typeof(RangeTreeNode<int, RangeItem>).GetField("_leftNode", bindFlags);
            var left = leftFieldInfo.GetValue(rangeTreeNode) as RangeTreeNode<int, RangeItem>;
            if (left != null)
            {
                yield return left;
                foreach (var node in TraverseNode(left))
                {
                    yield return node;
                }
            }

            var rightFieldInfo = typeof(RangeTreeNode<int, RangeItem>).GetField("_rightNode", bindFlags);
            var right = rightFieldInfo.GetValue(rangeTreeNode) as RangeTreeNode<int, RangeItem>;
            if (right != null)
            {
                yield return right;
                foreach (var node in TraverseNode(right))
                {
                    yield return node;
                }
            }
        }

        private static object GetComparerViaReflection(RangeTreeNode<int, RangeItem> rangeTreeNode)
        {
            var bindFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static;
            var comparerFieldInfo = typeof(RangeTreeNode<int, RangeItem>).GetField("_rangeComparer", bindFlags);
            var comparer = comparerFieldInfo.GetValue(rangeTreeNode);
            return comparer;
        }
    }
}