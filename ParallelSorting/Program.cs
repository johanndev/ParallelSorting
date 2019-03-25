using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Mathematics;
using BenchmarkDotNet.Running;
using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace ParallelSorting
{
    class Program
    {
        static void Main(string[] args)
        {
            var testArray = TestData.GetTestArray();

            var fileName = "TestArray.bin";
            var filePath = Path.Combine(Path.GetTempPath(), fileName);

            using (var stream = File.Open(filePath, FileMode.Create))
            {
                var binF = new BinaryFormatter();
                binF.Serialize(stream, testArray);
            }

            //BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, new DebugInProcessConfig());
            var summary = BenchmarkRunner.Run<SortBenchmarkHarness>();
        }
    }

    public class TestData
    {
        public static int[] GetTestArray()
        {
            var n = 2_000_000;
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

    [CategoriesColumn]
    [RankColumn(NumeralSystem.Stars)]
    [RankColumn(NumeralSystem.Roman)]
    public class SortBenchmarkHarness
    {
        private string fileName = $"TestArray.bin";
        private int[] orgArray;
        private int[] startArray;

        [GlobalSetup]
        public void GlobalSetup()
        {
            int[] importedArray;

            var filePath = Path.Combine(Path.GetTempPath(), fileName);

            using (var stream = File.Open(filePath, FileMode.Open))
            {
                var binF = new BinaryFormatter();
                importedArray = (int[])binF.Deserialize(stream);
            }

            orgArray = new int[importedArray.Length];
            importedArray.AsSpan().CopyTo(orgArray.AsSpan());
        }

        [IterationSetup]
        public void IterationSetup()
        {
            startArray = new int[orgArray.Length];
            orgArray.AsSpan().CopyTo(startArray.AsSpan());
        }

        [BenchmarkCategory("MergeSort"), Benchmark()]
        public void MergeSort()
        {
            MergeSort<int>.RunSequential(startArray);
        }

        [Benchmark, BenchmarkCategory("MergeSort")]
        public void MergeSortParallel()
        {
            MergeSort<int>.RunParallel(startArray);
        }

        [BenchmarkCategory("QuickSort"), Benchmark()]
        public void QuickSort()
        {
            QuickSort<int>.RunSequential(startArray);
        }

        [Benchmark, BenchmarkCategory("QuickSort")]
        public void QuickSortParallel()
        {
            QuickSort<int>.RunParallel(startArray);
        }

        [BenchmarkCategory("BaseLine"), Benchmark(Baseline = true)]
        public void ArraySort()
        {
            Array.Sort(startArray);
        }
    }
}
