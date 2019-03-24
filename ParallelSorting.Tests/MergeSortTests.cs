using System;
using Xunit;

namespace ParallelSorting.Tests
{
    public class MergeSortTests
    {
        [Fact]
        public void SequentialMergeSort_ShouldSortCorrectly()
        {
            var orgArray = new[] { 5, 3, 4, 2, 1 };

            var sortedArray = MergeSort.Sort(ref orgArray);

            Assert.Equal(new[] { 1, 2, 3, 4, 5 }, sortedArray);
        }
    }
}
