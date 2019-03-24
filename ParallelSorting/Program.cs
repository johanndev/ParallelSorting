using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using System;

namespace ParallelSorting
{
    class Program
    {
        static void Main(string[] args)
        {
            var mergeSortSummary = BenchmarkRunner.Run<SortBenchmarkHarness>();
        }

    }

    public static class TestData
    {
        public static int[] GetTestArray()
        {
            int n = 100;
            int min = 0;
            int max = 1_000_000;
            var result = new int[n];

            var random = new Random();
            for (int i = 0; i < n; i++)
            {
                result[i] = random.Next(min, max);
            }

            return result;
        }
    }

    [GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
    [CategoriesColumn]
    public class SortBenchmarkHarness
    {
        private int[] startArray;
        public SortBenchmarkHarness()
        {
            startArray = TestData.GetTestArray();
        }

        [Benchmark(Baseline = true), BenchmarkCategory(nameof(MergeSort))]
        public void RunMergeSort()
        {
            var orgArray = startArray;
            MergeSort.Sort(ref orgArray);
        }

        [Benchmark, BenchmarkCategory(nameof(MergeSort))]
        public void RunMergeSortParallel()
        {
            var orgArray = startArray;
            MergeSort.Sort(ref orgArray);
        }

        [Benchmark(Baseline = true), BenchmarkCategory(nameof(QuickSort))]
        public void RunQuickSort()
        {
            var orgArray = startArray;
            QuickSort.Sort(ref orgArray);
        }

        [Benchmark, BenchmarkCategory(nameof(QuickSort))]
        public void RunQuickSortParallel()
        {
            var orgArray = startArray;
            QuickSort.Sort(ref orgArray);
        }
    }
}
