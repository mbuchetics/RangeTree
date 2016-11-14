RangeTree
=========

## A generic interval tree

A generic implementation of a centered interval tree in C#.

From [Wikipedia](http://en.wikipedia.org/wiki/Interval_tree):
> In computer science, an interval tree is an ordered tree data structure to hold intervals. Specifically, it allows one to efficiently find all intervals that overlap with any given interval or point. It is often used for windowing queries, for instance, to find all roads on a computerized map inside a rectangular viewport, or to find all visible elements inside a three-dimensional scene.

Based on the Java implementation found here: http://www.sanfoundry.com/java-program-implement-interval-tree/

Queries require O(log n + m) time, with n being the total number of intervals and m being the number of reported results. Construction requires O(n log n) time, and storage requires O(n) space.

### Requirements ###

It can compile to .Net >= 3.5, but currently targets .Net 4.0.

### Simple Interface ###

    public interface IRangeTree<TKey, TValue> 
        : IEnumerable<RangeValuePair<TKey, TValue>>
    {
        IEnumerable<TValue> Values { get; }
        int Count { get; }

        IEnumerable<TValue> Query(TKey value);
        IEnumerable<TValue> Query(TKey from, TKey to);

        void Rebuild();
        void Add(TKey from, TKey to, TValue value);
        void Remove(TValue item);
        void Remove(IEnumerable<TValue> items);
        void Clear();
    }
    
### Example ###

    var tree = new RangeTree<int, string>()
    {
        { 0, 10, "1" },
        { 20, 30, "2" },
        { 15, 17, "3" },
        { 25, 35, "4" },
    };
    
    // Alternatively, use the Add method, for example:
    // tree.Add(0, 10, "1");

    var results1 = tree.Query(5);     // 1 item: [0 - 10]
    var results2 = tree.Query(10);    // 1 item: [0 - 10]
    var results3 = tree.Query(29);    // 2 items: [20 - 30], [25 - 35]
    var results4 = tree.Query(5, 15); // 2 items: [0 - 10], [15 - 17]
    
The solution file contains a few examples and also a comparision of the default and async versions.
    
### Implementation Details ###

In the standard implementation, whenever you add or remove items from the tree, the tree goes "out of sync". Whenever it is queried next, the tree structure is then automatically rebuild (you can control this behaviour using the `AutoRebuild` flag). You may also call `Rebuild()` manually.
The creation of the tree requires O(n log n) time. Therefore, the standard implementation is best suited for trees that do not change often or small trees, where the creation time is negligible.
