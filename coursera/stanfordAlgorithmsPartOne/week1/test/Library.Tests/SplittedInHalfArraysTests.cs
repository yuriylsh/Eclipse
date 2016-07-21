using System;
using Inversions;
using Xunit;

namespace Tests
{
    public class SplittedInHalfArraysTests
    {
        [Fact]
        public void SplittingInHalvesNullArrayThrowsNREException()
        {
            Assert.Throws<NullReferenceException>(() => new SplittedInHalfArrays(null));
        }

        [Fact]
        public void SplittingInHalvesEmptyArrayThrowsIndexOutOfRangeException()
        {
            Assert.Throws<IndexOutOfRangeException>(() => new SplittedInHalfArrays(new int[0]));
        }

        [Fact]
        public void SplittingInHalvesArrayWithOneItemSetsLeftToArrayWithItemAndRightToEmptyArray()
        {
            var halves = new SplittedInHalfArrays(new[] {10});
            Assert.True(AreArraysEqual(halves.Left, new[] {10}));
            Assert.True(AreArraysEqual(halves.Right, new int[0]));
        }

        [Fact]
        public void SplittingInHalvesArrayWithTwoItemsSetsLeftToFirstItemAndRightToSecond()
        {
            var halves = new SplittedInHalfArrays(new[] {10, 20});

            Assert.True(AreArraysEqual(halves.Left, new [] {10}));
            Assert.True(AreArraysEqual(halves.Right, new [] {20}));
        }

        [Fact]
        public void SplittingInHalvesArrayWithOddNumberOfItemsProducesCorrectHalves()
        {
            var halves = new SplittedInHalfArrays(new[] {10, 20, 30, 40, 50});
            Assert.True(AreArraysEqual(halves.Left, new[] {10, 20, 30}));
            Assert.True(AreArraysEqual(halves.Right, new[] {40, 50}));
        }

        [Fact]
        public void SplittingInHalvesArrayWithEvenNumberOfItemsProducesCorrectHalves()
        {
            var halves = new SplittedInHalfArrays(new[] {10, 20, 30, 40});
            Assert.True(AreArraysEqual(halves.Left, new[] {10, 20, 30}));
            Assert.True(AreArraysEqual(halves.Right, new[] {40}));
        }

        private bool AreArraysEqual(int[] firstArray, int[] secondArray)
        {
            if (firstArray.Length != secondArray.Length)
            {
                return false;
            }

            for (int i = 0; i < firstArray.Length; i++)
            {
                if (firstArray[i] != secondArray[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}