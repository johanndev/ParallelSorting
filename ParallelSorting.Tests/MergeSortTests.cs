using Xunit;

namespace ParallelSorting.Tests
{
    public class MergeSortTests
    {

        [Fact]
        public void Sequential_ShouldSortCorrectly()
        {
            var orgArray = new[] { 5, 3, 4, 2, 1 };

            var sortedArray = MergeSort<int>.RunSequential(orgArray);

            Assert.Equal(new[] { 1, 2, 3, 4, 5 }, sortedArray);
        }

        [Fact]
        public void Parallel_ShouldSortCorrectly()
        {
            var orgArray = new[] { 5, 3, 4, 2, 1 };

            var sortedArray = MergeSort<int>.RunParallel(orgArray);

            Assert.Equal(new[] { 1, 2, 3, 4, 5 }, sortedArray);
        }
    }
}
