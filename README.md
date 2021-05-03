# IntervalTree #

[![Build status](https://ci.appveyor.com/api/projects/status/t8xvh5oquuvk17ks?svg=true)](https://ci.appveyor.com/project/apacha/rangetree)
[![NuGet version](https://img.shields.io/nuget/v/RangeTree.svg?style=flat-square)](https://www.nuget.org/packages/RangeTree)

## A generic interval tree ##

A generic implementation of a centered interval tree in C#. 

From [Wikipedia](http://en.wikipedia.org/wiki/Interval_tree):
> In computer science, an interval tree is an ordered tree data structure to hold intervals. Specifically, it allows one to efficiently find all intervals that overlap with any given interval or point. It is often used for windowing queries, for instance, to find all roads on a computerized map inside a rectangular viewport, or to find all visible elements inside a three-dimensional scene.

Based on the Java implementation found here: http://www.sanfoundry.com/java-program-implement-interval-tree/

Queries require `O(log n + m)` time, with `n` being the total number of intervals and `m` being the number of reported results. Construction requires `O(n log n)` time, and storage requires `O(n)` space.

### Requirements ###
- Consuming this NuGet package requires .NET Framework >= 4.5 or .NET Standard >= 1.2
- Developing this project requires Visual Studio 2017 with .NET Framework >= 4.5 and .NET Standard >= 2.0.

## Simple Interface ###

```csharp
public interface IIntervalTree<TKey, TValue> 
    : IEnumerable<RangeValuePair<TKey, TValue>>
{
    IEnumerable<TValue> Values { get; }
    int Count { get; }

    IEnumerable<TValue> Query(TKey value);
    IEnumerable<TValue> Query(TKey from, TKey to);

    void Add(TKey from, TKey to, TValue value);
    void Remove(TValue item);
    void Remove(IEnumerable<TValue> items);
    void Clear();
}
```
    
## Usage ###

```csharp
var tree = new IntervalTree<int, string>()
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
```
    
The solution file contains more examples and tests, that show how to use IntervalTree with other data types.
    
## Implementation Details ##

In this implementation, whenever you add or remove items from the tree, the tree goes "out of sync" internally, which means that the items are stored, but the tree-index is not updated yet. Upon the next query, the tree structure is automatically rebuild. Subsequent queries will use the cached index and be much faster. The creation of the tree-index requires `O(n log n)` time. Therefore, it is best suited for trees that do not change often or small trees, where the creation time is negligible.

## RangeTree vs. IntervalTree ##

This project contains an IntervalTree (see [Issue #24](https://github.com/mbuchetics/RangeTree/issues/24)), but was incorrectly named RangeTree at the beginning. It was mostly renamed to IntervalTree in version 3.0.0. However, given that a large number of users are using this project, renaming the NuGet package and repository was not possible without breaking too much, so we settled with (just) renaming all occurences in the source code and documentation.