using System;
using System.Threading.Tasks;

namespace ParallelSorting
{
    public class QuickSort<T> where T : IComparable<T>
    {
        public static T[] RunSequential<T>(T[] array)
            where T : IComparable<T>
        {
            return InternalSequential(array, 0, array.Length - 1);
        }

        public static T[] RunParallel<T>(T[] array)
            where T : IComparable<T>
        {
            return InternalParallel(array, 0, array.Length - 1);
        }

        private static T[] InternalSequential<T>(T[] array, int left, int right)
            where T : IComparable<T>
        {
            if (left >= right)
            {
                return array;
            }

            SwapElements(array, left, (left + right) / 2);
            int last = left;
            for (int current = left + 1; current <= right; current++)
            {
                if (array[current].CompareTo(array[left]) < 0)
                {
                    last++;
                    SwapElements(array, last, current);
                }
            }

            SwapElements(array, left, last);

            InternalSequential(array, left, last - 1);
            InternalSequential(array, last + 1, right);

            return array;
        }

        private static T[] InternalParallel<T>(T[] array, int left, int right)
            where T : IComparable<T>
        {
            if (left >= right)
            {
                return array;
            }

            SwapElements(array, left, (left + right) / 2);
            int last = left;
            for (int current = left + 1; current <= right; current++)
            {
                if (array[current].CompareTo(array[left]) < 0)
                {
                    last++;
                    SwapElements(array, last, current);
                }
            }

            SwapElements(array, left, last);

            Parallel.Invoke(
                () => InternalParallel(array, left, last - 1),
                () => InternalParallel(array, last + 1, right)
            );

            return array;
        }

        private static void SwapElements<T>(T[] array, int left, int right)
            where T : IComparable<T>
        {
            T temp = array[left];
            array[left] = array[right];
            array[right] = temp;
        }
    }
}
