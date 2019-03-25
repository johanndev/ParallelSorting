using System;
using System.Threading.Tasks;

namespace ParallelSorting
{
    public class MergeSort<T> where T : IComparable<T>
    {
        public static T[] RunSequential(T[] array)
        {
            return RunSequentialInternal(array, 0, array.Length - 1);
        }
        private static T[] RunSequentialInternal(T[] array, int start, int end)
        {
            if (start >= end)
            {
                return new T[] { array[start] };
            }

            var mid = (end + start) / 2;
            T[] left = RunSequentialInternal(array, start, mid);
            T[] right = RunSequentialInternal(array, mid + 1, end);
            var resultArr = Merge(left, right);

            return resultArr;
        }

        public static T[] RunParallel(T[] array)
        {
            return RunParallelInternal(array, 0, array.Length - 1);
        }

        private static T[] RunParallelInternal(T[] array, int start, int end)
        {
            if (start >= end)
            {
                return new T[] { array[start] };
            }

            var mid = (end + start) / 2;
            var left = new T[mid];
            var right = new T[array.Length - mid];

            Parallel.Invoke(
                () => left = RunSequentialInternal(array, start, mid),
                () => right = RunSequentialInternal(array, mid + 1, end)
            );
            var resultArr = Merge(left, right);

            return resultArr;
        }

        private static T[] Merge<T>(T[] left, T[] right)
            where T : IComparable<T>
        {
            var mergedArr = new T[left.Length + right.Length];

            int leftIndex = 0;
            int rightIndex = 0;
            int mergedIndex = 0;

            while (leftIndex < left.Length && rightIndex < right.Length)
            {
                if (left[leftIndex].CompareTo(right[rightIndex]) < 0)
                {
                    mergedArr[mergedIndex++] = left[leftIndex++];
                }
                else
                {
                    mergedArr[mergedIndex++] = right[rightIndex++];
                }
            }

            while (leftIndex < left.Length)
            {
                mergedArr[mergedIndex++] = left[leftIndex++];
            }

            while (rightIndex < right.Length)
            {
                mergedArr[mergedIndex++] = right[rightIndex++];
            }

            return mergedArr;
        }
    }
}
