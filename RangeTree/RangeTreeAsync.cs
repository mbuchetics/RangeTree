using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MB.Algodat
{
    public class RangeTreeAsync<TKey, T> : IRangeTree<TKey, T>
        where TKey : IComparable<TKey>
        where T : IRangeProvider<TKey>
    {
        private RangeTree<TKey, T> _rangeTree;
        private List<T> _addedItems = new List<T>();
        private List<T> _removedItems = new List<T>();
        private List<T> _addedItemsRebuilding = new List<T>();
        private List<T> _removedItemsRebuilding = new List<T>();
        private IComparer<T> _rangeComparer;
        private Task _rebuildTask;
        private CancellationTokenSource _rebuildTaskCancelSource;
        private bool _isRebuilding;

        private object _locker = new object();

        public IEnumerable<T> Items
        {
            get { return _rangeTree.Items.Concat(_addedItemsRebuilding).Concat(_addedItems); }
        }

        public int Count
        {
            get { return _rangeTree.Count + _addedItemsRebuilding.Count + _addedItems.Count; }
        }

        public RangeTreeAsync(IComparer<T> rangeComparer)
        {
            _rangeTree = new RangeTree<TKey, T>(rangeComparer) { AutoRebuild = false };
            _rangeComparer = rangeComparer;
        }

        public RangeTreeAsync(IEnumerable<T> items, IComparer<T> rangeComparer)
        {
            _rangeTree = new RangeTree<TKey, T>(items, rangeComparer) { AutoRebuild = false };
            _rangeComparer = rangeComparer;
        }

        public List<T> Query(TKey value)
        {
            if (NeedsRebuild())
                RebuildTree();

            lock (_locker)
            {
                var result = _rangeTree.Query(value);

                result.AddRange(_addedItemsRebuilding.Where(item => item.Range.Contains(value)));
                result.AddRange(_addedItems.Where(item => item.Range.Contains(value)));

                if (_removedItemsRebuilding.Count > 0 || _removedItems.Count > 0)
                {
                    var hs = new HashSet<T>(result);
                    foreach (var item in _removedItemsRebuilding)
                        hs.Remove(item);
                    foreach (var item in _removedItems)
                        hs.Remove(item);
                    result = hs.ToList();
                }

                return result;
            }
        }

        public List<T> Query(Range<TKey> range)
        {
            if (NeedsRebuild())
                RebuildTree();

            lock (_locker)
            {
                var result = _rangeTree.Query(range);

                result.AddRange(_addedItemsRebuilding.Where(item => item.Range.Intersects(range)));
                result.AddRange(_addedItems.Where(item => item.Range.Intersects(range)));

                if (_removedItemsRebuilding.Count > 0 || _removedItems.Count > 0)
                {
                    var hs = new HashSet<T>(result);
                    foreach (var item in _removedItemsRebuilding)
                        hs.Remove(item);
                    foreach (var item in _removedItems)
                        hs.Remove(item);
                    result = hs.ToList();
                }

                return result;
            }
        }

        public void Add(T item)
        {
            lock (_locker)
                _addedItems.Add(item);
        }

        public void Add(IEnumerable<T> items)
        {
            lock (_locker)
                _addedItems.AddRange(items);
        }

        public void Remove(T item)
        {
            lock (_locker)
                _removedItems.Add(item);
        }

        public void Remove(IEnumerable<T> items)
        {
            lock (_locker)
                _removedItems.AddRange(items);
        }

        public void Clear()
        {
            lock (_locker)
            {
                _rangeTree.Clear();
                _addedItems = new List<T>();
                _removedItems = new List<T>();
                _addedItemsRebuilding = new List<T>();
                _removedItemsRebuilding = new List<T>();

                if (_rebuildTaskCancelSource != null)
                    _rebuildTaskCancelSource.Cancel();
            }
        }

        public void Rebuild()
        {
            if (NeedsRebuild())
                RebuildTree();
        }

        private void RebuildTree()
        {
            lock (_locker)
            {
                if (_isRebuilding || _addedItems.Count == 0)
                    return;

                _isRebuilding = true;
            }

            _rebuildTaskCancelSource = new CancellationTokenSource();

            _rebuildTask = Task.Factory.StartNew(() =>
                {
                    lock (_locker)
                    {
                        _addedItemsRebuilding = _addedItems.ToList();
                        _addedItems.Clear();

                        _removedItemsRebuilding = _removedItemsRebuilding.ToList();
                        _removedItems.Clear();
                    }

                    var newItems = _rangeTree.Items.ToList();
                    newItems.AddRange(_addedItemsRebuilding);

                    foreach (var item in _removedItemsRebuilding)
                        newItems.Remove(item);

                    var newTree = new RangeTree<TKey, T>(newItems, _rangeComparer) { AutoRebuild = false };

                    if (!_rebuildTaskCancelSource.Token.IsCancellationRequested)
                    {
                        lock (_locker)
                        {
                            _rangeTree = newTree;
                            _addedItemsRebuilding.Clear();
                            _removedItemsRebuilding.Clear();
                        }
                    }
                    else
                    {
                        // nop
                    }
                }, _rebuildTaskCancelSource.Token)
            .ContinueWith(task =>
            {
                _isRebuilding = false;

                if (NeedsRebuild())
                    RebuildTree();
            });
        }

        private bool NeedsRebuild()
        {
            lock (_locker)
            {
                return _addedItems.Count > 100 || _removedItems.Count > 10;
            }
        }
    }
}
