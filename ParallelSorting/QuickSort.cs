using System;
using System.Collections.Generic;
using System.Text;

namespace ParallelSorting
{
    public class QuickSort
    {
        public static int[] Sort(ref int[] array)
        {
            runQuickSort(0, array.Length - 1, ref array);

            return array;
        }

        private static void runQuickSort(int left, int right, ref int[] array)
        {
            if (left < right)
            {
                int divider = divide(left, right, ref array);
                runQuickSort(left, divider - 1, ref array);
                runQuickSort(divider + 1, right, ref array);
            }
        }

        private static int divide(int left, int right, ref int[] array)
        {
            int i = left;
            int j = right - 1;
            int pivot = array[right];

            do
            {
                while (array[i] <= pivot && i < right)
                {
                    i += 1;
                }

                while (array[j] >= pivot && j > left)
                {
                    j -= 1;
                }

                if (i < j)
                {
                    int z = array[i];
                    array[i] = array[j];
                    array[j] = z;
                }
            } while (i < j);

            if (array[i] > pivot)
            {
                int z = array[i];
                array[i] = array[right];
                array[right] = z;
            }
            return i;
        }
    }
}
