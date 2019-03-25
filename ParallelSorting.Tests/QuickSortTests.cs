using Xunit;

namespace ParallelSorting.Tests
{
    public class QuickSortTests
    {
        [Fact]
        public void Sequential_ShouldSortCorrectly()
        {
            var orgArray = new[] { 5, 3, 4, 2, 1 };

            var sortedArray = QuickSort<int>.RunSequential(orgArray);

            Assert.Equal(new[] { 1, 2, 3, 4, 5 }, sortedArray);
        }

        [Fact]
        public void Parallel_ShouldSortCorrectly()
        {
            var orgArray = new[] { 5, 3, 4, 2, 1 };

            var sortedArray = QuickSort<int>.RunParallel(orgArray);

            Assert.Equal(new[] { 1, 2, 3, 4, 5 }, sortedArray);
        }
    }
}
