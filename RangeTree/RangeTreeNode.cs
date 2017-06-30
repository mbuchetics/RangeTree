using System;
using System.Collections.Generic;

namespace RangeTree
{
    /// <summary>
    /// A node of the range tree. Given a list of items, it builds its subtree. Also contains methods to query the subtree. Basically, all interval tree logic is here.
    /// </summary>
    /// <typeparam name="TKey">The type of <see cref="ICollection{T}"/> key.</typeparam>
    /// <typeparam name="T">The type of <see cref="IRangeProvider{T}"/></typeparam>
    public class RangeTreeNode<TKey, T>
        where TKey : IComparable<TKey>
        where T : IRangeProvider<TKey>
    {
        private TKey center;
        private RangeTreeNode<TKey, T> leftNode;
        private RangeTreeNode<TKey, T> rightNode;
        private List<T> items;

        private readonly IComparer<T> rangeComparer;

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeTreeNode{TKey, T}"/> class.
        /// </summary>
        /// <param name="rangeComparer">The range comparer.</param>
        public RangeTreeNode(IComparer<T> rangeComparer = null)
        {
            if (rangeComparer != null)
            {
                this.rangeComparer = rangeComparer;
            }

            center = default(TKey);
            leftNode = null;
            rightNode = null;
            items = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeTreeNode{TKey, T}"/> class.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="rangeComparer">The range comparer.</param>
        public RangeTreeNode(IEnumerable<T> items, IComparer<T> rangeComparer = null)
        {
            if (rangeComparer != null)
            {
                this.rangeComparer = rangeComparer;
            }

            // first, find the median
            var endPoints = new List<TKey>();
            foreach (var o in items)
            {
                var range = o.Range;
                endPoints.Add(range.From);
                endPoints.Add(range.To);
            }
            endPoints.Sort();

            // the median is used as center value
            center = endPoints[endPoints.Count / 2];
            this.items = new List<T>();
            
            var left = new List<T>();
            var right = new List<T>();

            // iterate over all items
            // if the range of an item is completely left of the center, add it to the left items
            // if it is on the right of the center, add it to the right items
            // otherwise (range overlaps the center), add the item to this node's items
            foreach (var o in items)
            {
                var range = o.Range;

                if (range.To.CompareTo(center) < 0)
                {
                    left.Add(o);
                }
                else if (range.From.CompareTo(center) > 0)
                {
                    right.Add(o);
                }
                else
                {
                    this.items.Add(o);
                }
            }

            // sort the items, this way the query is faster later on
            if (this.items.Count > 0)
            {
                this.items.Sort(this.rangeComparer);
            }
            else
            {
                this.items = null;
            }

            // create left and right nodes, if there are any items
            if (left.Count > 0)
            {
                leftNode = new RangeTreeNode<TKey, T>(left, this.rangeComparer);
            }

            if (right.Count > 0)
            {
                rightNode = new RangeTreeNode<TKey, T>(right, this.rangeComparer);
            }
        }

        /// <summary>
        /// Performans a "stab" query with a single value. All items with overlapping ranges are returned.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The resulting <see cref="List{T}"/></returns>
        public List<T> Query(TKey value)
        {
            var results = new List<T>();

            // If the node has items, check their ranges.
            if (items != null)
            {
                foreach (var o in items)
                {
                    if (o.Range.From.CompareTo(value) > 0)
                    {
                        break;
                    }
                    else if (o.Range.Contains(value))
                    {
                        results.Add(o);
                    }
                }
            }

            // go to the left or go to the right of the tree, depending
            // where the query value lies compared to the center
            if (value.CompareTo(center) < 0 && leftNode != null)
            {
                results.AddRange(leftNode.Query(value));
            }
            else if (value.CompareTo(center) > 0 && rightNode != null)
            {
                results.AddRange(rightNode.Query(value));
            }

            return results;
        }

        /// <summary>
        /// Performans a range query. All items with overlapping ranges are returned.
        /// </summary>
        /// <param name="range">The range.</param>
        /// <returns>The resulting <see cref="List{T}"/></returns>
        public List<T> Query(Range<TKey> range)
        {
            var results = new List<T>();

            // If the node has items, check their ranges.
            if (items != null)
            {
                foreach (var o in items)
                {
                    if (o.Range.From.CompareTo(range.To) > 0)
                    {
                        break;
                    }
                    else if (o.Range.Intersects(range))
                    {
                        results.Add(o);
                    }
                }
            }

            // go to the left or go to the right of the tree, depending
            // where the query value lies compared to the center
            if (range.From.CompareTo(center) < 0 && leftNode != null)
            {
                results.AddRange(leftNode.Query(range));
            }

            if (range.To.CompareTo(center) > 0 && rightNode != null)
            {
                results.AddRange(rightNode.Query(range));
            }

            return results;
        }
    }
}
