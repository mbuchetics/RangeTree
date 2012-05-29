using System;
using System.Collections.Generic;
using System.Linq;

namespace MB.Algodat
{
    public interface IRangeTree<TKey, T>
        where TKey : IComparable<TKey>
        where T : IRangeProvider<TKey>
    {
        IEnumerable<T> Items { get; }
        int Count { get; }

        List<T> Query(TKey value);
        List<T> Query(Range<TKey> range);

        void Rebuild();
        void Add(T item);
        void Add(IEnumerable<T> items);
        void Remove(T item);
        void Remove(IEnumerable<T> items);
        void Clear();
    }

    public class RangeTreeNode<TKey, T>
        where TKey : IComparable<TKey>
        where T : IRangeProvider<TKey>
    {
        private TKey _center;
        private RangeTreeNode<TKey, T> _leftNode;
        private RangeTreeNode<TKey, T> _rightNode;
        private List<T> _items;

        private static IComparer<T> s_rangeComparer;

        public RangeTreeNode(IComparer<T> _rangeComparer = null)
        {
            if (_rangeComparer != null)
                s_rangeComparer = _rangeComparer;

            _center = default(TKey);
            _leftNode = null;
            _rightNode = null;
            _items = null;
        }

        public RangeTreeNode(IEnumerable<T> items, IComparer<T> _rangeComparer = null)
        {
            if (_rangeComparer != null)
                s_rangeComparer = _rangeComparer;

            var endPoints = new List<TKey>();
            foreach (var o in items)
            {
                var range = o.Range;
                endPoints.Add(range.From);
                endPoints.Add(range.To);
            }
            endPoints.Sort();

            _center = endPoints[endPoints.Count / 2];
            _items = new List<T>();
            
            var left = new List<T>();
            var right = new List<T>();

            foreach (var o in items)
            {
                var range = o.Range;

                if (range.To.CompareTo(_center) < 0)
                    left.Add(o);
                else if (range.From.CompareTo(_center) > 0)
                    right.Add(o);
                else
                    _items.Add(o);
            }

            if (_items.Count > 0)
                _items.Sort(s_rangeComparer);
            else
                _items = null;

            if (left.Count > 0)
                _leftNode = new RangeTreeNode<TKey, T>(left);
            if (right.Count > 0)
                _rightNode = new RangeTreeNode<TKey, T>(right);
        }

        public List<T> Query(TKey value)
        {
            var results = new List<T>();

            if (_items != null)
            {
                foreach (var o in _items)
                {
                    if (o.Range.From.CompareTo(value) > 0)
                        break;
                    else if (o.Range.Contains(value))
                        results.Add(o);
                }
            }

            if (value.CompareTo(_center) < 0 && _leftNode != null)
                results.AddRange(_leftNode.Query(value));
            else if (value.CompareTo(_center) > 0 && _rightNode != null)
                results.AddRange(_rightNode.Query(value));
            
            return results;
        }

        public List<T> Query(Range<TKey> range)
        {
            var results = new List<T>();

            if (_items != null)
            {
                foreach (var o in _items)
                {
                    if (o.Range.From.CompareTo(range.To) > 0)
                        break;
                    else if (o.Range.Intersects(range))
                        results.Add(o);
                }
            }

            if (range.From.CompareTo(_center) < 0 && _leftNode != null)
                results.AddRange(_leftNode.Query(range));
            if (range.To.CompareTo(_center) > 0 && _rightNode != null)
                results.AddRange(_rightNode.Query(range));
            
            return results;
        }
    }

    public class RangeTree<TKey, T> : IRangeTree<TKey, T>
        where TKey : IComparable<TKey>
        where T : IRangeProvider<TKey>
    {
        private RangeTreeNode<TKey, T> _root;
        private List<T> _items;
        private bool _isInSync;
        private bool _autoRebuild;
        private IComparer<T> _rangeComparer;

        public bool IsInSync
        {
            get { return _isInSync; }
        }

        public IEnumerable<T> Items
        {
            get { return _items; }
        }

        public int Count
        {
            get { return _items.Count; }
        }

        public bool AutoRebuild
        {
            get { return _autoRebuild; }
            set { _autoRebuild = value; }
        }

        public RangeTree(IComparer<T> rangeComparer)
        {
            _rangeComparer = rangeComparer;
            _root = new RangeTreeNode<TKey, T>(rangeComparer);            
            _items = new List<T>();
            _isInSync = true;
            _autoRebuild = true;
            
        }

        public RangeTree(IEnumerable<T> items, IComparer<T> rangeComparer)
        {
            _rangeComparer = rangeComparer;
            _root = new RangeTreeNode<TKey, T>(items, rangeComparer);            
            _items = items.ToList();
            _isInSync = true;
            _autoRebuild = true;
        }

        public List<T> Query(TKey value)
        {
            if (!_isInSync && _autoRebuild)
                Rebuild();

            return _root.Query(value);
        }

        public List<T> Query(Range<TKey> range)
        {
            if (!_isInSync && _autoRebuild)
                Rebuild();

            return _root.Query(range);
        }

        public void Rebuild()
        {
            if (_isInSync)
                return;

            _root = new RangeTreeNode<TKey, T>(_items, _rangeComparer);
            _isInSync = true;
        }

        public void Add(T item)
        {
            _isInSync = false;
            _items.Add(item);
        }

        public void Add(IEnumerable<T> items)
        {
            _isInSync = false;
            _items.AddRange(items);
        }

        public void Remove(T item)
        {
            _isInSync = false;
            _items.Remove(item);
        }

        public void Remove(IEnumerable<T> items)
        {
            _isInSync = false;

            foreach (var item in items)
                _items.Remove(item);
        }

        public void Clear()
        {
            _root = new RangeTreeNode<TKey, T>(_rangeComparer);            
            _items = new List<T>();
            _isInSync = true;
        }
    }

    
}
