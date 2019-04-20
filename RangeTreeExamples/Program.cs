using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using RangeTree;

namespace RangeTreeExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            TreeExample1();
            TreeExample2();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static void TreeExample1()
        {
            Console.WriteLine("Example 1");

            var tree = new RangeTree<int, string>()
            {
                { 0, 10, "1" },
                { 20, 30, "2" },
                { 15, 17, "3" },
                { 25, 35, "4" },
            };

            PrintQueryResult("query 1", tree.Query(5));
            PrintQueryResult("query 2", tree.Query(10));
            PrintQueryResult("query 3", tree.Query(29));
            PrintQueryResult("query 4", tree.Query(5, 15));

            Console.WriteLine();
        }

        static void TreeExample2()
        {
            Console.WriteLine("Example 2");

            var tree = new RangeTree<int, string>();
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                    RandomTreeInsert(tree, 1000);

                var resultCount = tree.Query(50, 60).Count();
                Console.WriteLine("query: {0} results (tree count: {1})", resultCount, tree.Count);
            }

            stopwatch.Stop();
            Console.WriteLine("elapsed time: {0}", stopwatch.Elapsed);
        }

        static Random s_rnd = new Random();

        static void RandomTreeInsert(IRangeTree<int, string> tree, int limit)
        {
            var a = s_rnd.Next(limit);
            var b = s_rnd.Next(limit);

            tree.Add(Math.Min(a, b), Math.Max(a, b), "value");
        }

        static void PrintQueryResult(string queryTitle, IEnumerable<string> result)
        {
            Console.WriteLine(queryTitle);
            foreach (var item in result)
                Console.WriteLine(item);
        }
    }
}
