using Xunit;

namespace ParallelSorting.Tests
{
    public class QuickSortTests
    {
        [Fact]
        public void SequentialMergeSort_ShouldSortCorrectly()
        {
            var orgArray = new[] { 5, 3, 4, 2, 1 };

            var sortedArray = QuickSort.Sort(ref orgArray);

            Assert.Equal(new[] { 1, 2, 3, 4, 5 }, sortedArray);
        }
    }
}
