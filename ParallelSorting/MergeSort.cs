using System;
using System.Collections.Generic;
using System.Text;

namespace ParallelSorting
{
    public class MergeSort
    {
        public static int[] Sort(ref int[] array)
        {
            if (array.Length > 1)
            {
                var mid = array.Length / 2;

                var left = new int[mid];
                for (int i = 0; i <= left.Length - 1; i++)
                {
                    left[i] = array[i];
                }

                var right = new int[array.Length - mid];
                for (int i = mid; i <= array.Length - 1; i++)
                {
                    right[i - mid] = array[i];
                }

                Sort(ref left);
                Sort(ref right);

                array = Merge(ref left, ref right);
            }

            return array;
        }

        private static int[] Merge(ref int[] left, ref int[] right)
        {
            var newArray = new int[left.Length + right.Length];
            int indexLeft = 0;
            int indexRight = 0;
            int indexResult = 0;

            while (indexLeft < left.Length && indexRight < right.Length)
            {
                if (left[indexLeft] < right[indexRight])
                {
                    newArray[indexResult] = left[indexLeft];
                    indexLeft += 1;
                }
                else
                {
                    newArray[indexResult] = right[indexRight];
                    indexRight += 1;
                }
                indexResult += 1;
            }

            while (indexLeft < left.Length)
            {
                newArray[indexResult] = left[indexLeft];
                indexLeft += 1;
                indexResult += 1;
            }

            while (indexRight < right.Length)
            {
                newArray[indexResult] = right[indexRight];
                indexRight += 1;
                indexResult += 1;
            }

            return newArray;
        }
    }
}
