using System.Collections.Generic;

namespace IntervalTree
{
    /// <summary>
    ///     A node of the range tree. Given a list of items, it builds
    ///     its subtree. Also contains methods to query the subtree.
    ///     Basically, all interval tree logic is here.
    /// </summary>
    internal class IntervalTreeNode<TKey, TValue> : IComparer<RangeValuePair<TKey, TValue>>
    {
        private readonly TKey center;

        private readonly IComparer<TKey> comparer;
        private readonly RangeValuePair<TKey, TValue>[] items;
        private readonly IntervalTreeNode<TKey, TValue> leftNode;
        private readonly IntervalTreeNode<TKey, TValue> rightNode;

        /// <summary>
        ///     Initializes an empty node.
        /// </summary>
        /// <param name="comparer">The comparer used to compare two items.</param>
        public IntervalTreeNode(IComparer<TKey> comparer)
        {
            this.comparer = comparer ?? Comparer<TKey>.Default;

            center = default;
            leftNode = null;
            rightNode = null;
            items = null;
        }

        /// <summary>
        ///     Initializes a node with a list of items, builds the sub tree.
        /// </summary>
        /// <param name="items">The items that should be added to this node</param>
        /// <param name="comparer">The comparer used to compare two items.</param>
        public IntervalTreeNode(IList<RangeValuePair<TKey, TValue>> items, IComparer<TKey> comparer)
        {
            this.comparer = comparer ?? Comparer<TKey>.Default;

            // first, find the median
            var endPoints = new List<TKey>(items.Count * 2);
            foreach (var item in items)
            {
                endPoints.Add(item.From);
                endPoints.Add(item.To);
            }

            endPoints.Sort(this.comparer);

            // the median is used as center value
            if (endPoints.Count > 0)
            {
                Min = endPoints[0];
                center = endPoints[endPoints.Count / 2];
                Max = endPoints[endPoints.Count - 1];
            }

            var inner = new List<RangeValuePair<TKey, TValue>>();
            var left = new List<RangeValuePair<TKey, TValue>>();
            var right = new List<RangeValuePair<TKey, TValue>>();

            // iterate over all items
            // if the range of an item is completely left of the center, add it to the left items
            // if it is on the right of the center, add it to the right items
            // otherwise (range overlaps the center), add the item to this node's items
            foreach (var o in items)
                if (this.comparer.Compare(o.To, center) < 0)
                    left.Add(o);
                else if (this.comparer.Compare(o.From, center) > 0)
                    right.Add(o);
                else
                    inner.Add(o);

            // sort the items, this way the query is faster later on
            if (inner.Count > 0)
            {
                if (inner.Count > 1)
                    inner.Sort(this);
                this.items = inner.ToArray();
            }
            else
            {
                this.items = null;
            }

            // create left and right nodes, if there are any items
            if (left.Count > 0)
                leftNode = new IntervalTreeNode<TKey, TValue>(left, this.comparer);
            if (right.Count > 0)
                rightNode = new IntervalTreeNode<TKey, TValue>(right, this.comparer);
        }

        public TKey Max { get; }

        public TKey Min { get; }

        /// <summary>
        ///     Returns less than 0 if this range's From is less than the other, greater than 0 if greater.
        ///     If both are equal, the comparison of the To values is returned.
        ///     0 if both ranges are equal.
        /// </summary>
        /// <param name="x">The first item.</param>
        /// <param name="y">The other item.</param>
        /// <returns></returns>
        int IComparer<RangeValuePair<TKey, TValue>>.Compare(RangeValuePair<TKey, TValue> x,
            RangeValuePair<TKey, TValue> y)
        {
            var fromComp = comparer.Compare(x.From, y.From);
            if (fromComp == 0)
                return comparer.Compare(x.To, y.To);
            return fromComp;
        }

        /// <summary>
        ///     Performs a point query with a single value.
        ///     All items with overlapping ranges are returned.
        /// </summary>
        public IEnumerable<TValue> Query(TKey value)
        {
            var results = new List<TValue>();

            // If the node has items, check for leaves containing the value.
            if (items != null)
                foreach (var o in items)
                    if (comparer.Compare(o.From, value) > 0)
                        break;
                    else if (comparer.Compare(value, o.From) >= 0 && comparer.Compare(value, o.To) <= 0)
                        results.Add(o.Value);

            // go to the left or go to the right of the tree, depending
            // where the query value lies compared to the center
            var centerComp = comparer.Compare(value, center);
            if (leftNode != null && centerComp < 0)
                results.AddRange(leftNode.Query(value));
            else if (rightNode != null && centerComp > 0)
                results.AddRange(rightNode.Query(value));

            return results;
        }

        /// <summary>
        ///     Performs a range query.
        ///     All items with overlapping ranges are returned.
        /// </summary>
        public IEnumerable<TValue> Query(TKey from, TKey to)
        {
            var results = new List<TValue>();

            // If the node has items, check for leaves intersecting the range.
            if (items != null)
                foreach (var o in items)
                    if (comparer.Compare(o.From, to) > 0)
                        break;
                    else if (comparer.Compare(to, o.From) >= 0 && comparer.Compare(from, o.To) <= 0)
                        results.Add(o.Value);

            // go to the left or go to the right of the tree, depending
            // where the query value lies compared to the center
            if (leftNode != null && comparer.Compare(from, center) < 0)
                results.AddRange(leftNode.Query(from, to));
            if (rightNode != null && comparer.Compare(to, center) > 0)
                results.AddRange(rightNode.Query(from, to));

            return results;
        }
    }
}