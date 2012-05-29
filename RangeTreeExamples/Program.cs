using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MB.Algodat;
using System.Diagnostics;

namespace RangeTreeExamples
{    
    class Program
    {
        static void Main(string[] args)
        {
            TreeExample1();
            TreeExample2();
            TreeExample3();
        }

        static void TreeExample1()
        {
            Console.WriteLine("Example 1");

            var tree = new RangeTree<int, RangeItem>(new RangeItemComparer());

            tree.Add(new RangeItem(0, 10, "1"));
            tree.Add(new RangeItem(20, 30, "2"));
            tree.Add(new RangeItem(15, 17, "3"));
            tree.Add(new RangeItem(25, 35, "4"));

            PrintQueryResult("query 1", tree.Query(5));
            PrintQueryResult("query 2", tree.Query(10));
            PrintQueryResult("query 3", tree.Query(29));
            PrintQueryResult("query 4", tree.Query(new Range<int>(5, 15)));

            Console.WriteLine();
        }

        static void TreeExample2()
        {
            Console.WriteLine("Example 2");

            var tree = new RangeTree<int, RangeItem>(new RangeItemComparer());
            var range = new Range<int>(50, 60);

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                    RandomTreeInsert(tree, 1000);

                var resultCount = tree.Query(range).Count();
                Console.WriteLine("query: {0} results (tree count: {1})", resultCount, tree.Count);
            }

            stopwatch.Stop();
            Console.WriteLine("elapsed time: {0}", stopwatch.Elapsed);
        }

        static void TreeExample3()
        {
            Console.WriteLine("Example 3");

            var tree = new RangeTreeAsync<int, RangeItem>(new RangeItemComparer());
            var range = new Range<int>(50, 60);

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                    RandomTreeInsert(tree, 1000);

                var resultCount = tree.Query(range).Count();
                Console.WriteLine("query: {0} results (tree count: {1})", resultCount, tree.Count);
            }

            stopwatch.Stop();
            Console.WriteLine("elapsed time: {0}", stopwatch.Elapsed);
        }

        static Random s_rnd = new Random();
        
        static void RandomTreeInsert(IRangeTree<int, RangeItem> tree, int limit)
        {
            var a = s_rnd.Next(limit);
            var b = s_rnd.Next(limit);

            tree.Add(new RangeItem(Math.Min(a, b), Math.Max(a, b)));
        }

        static void PrintQueryResult(string queryTitle, IEnumerable<RangeItem> result)
        {
            Console.WriteLine(queryTitle);
            foreach (var item in result)
                Console.WriteLine(item);
        }
    }
}
