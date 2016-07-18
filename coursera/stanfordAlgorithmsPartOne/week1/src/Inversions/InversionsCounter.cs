using System;

namespace Inversions
{
    public class InversionsCounter
    {
        public long Count(int[] input)
        {
            if (input?.Length < 2)
            {
                return 0;
            }
            return CountInversionsAndSortInput(input).NumberOfInversions;
        }

        private CountedInversionsAndSortedInput CountInversionsAndSortInput(int[] input)
        {
            if (input.Length == 1)
            {
                return new Inversions.CountedInversionsAndSortedInput(0, input);
            }
            var halves = new SplittedInHalfArrays(input);
            var leftInversionsAndSortedInput = CountInversionsAndSortInput(halves.Left);
            var rightInversionsAndSortedInput = CountInversionsAndSortInput(halves.Right);
            var splitInversionsAndSortedResult = MergeHalvesAndCountSplitInversions(leftInversionsAndSortedInput.SortedInput, rightInversionsAndSortedInput.SortedInput);
            long totalInversions =
                leftInversionsAndSortedInput.NumberOfInversions
                + rightInversionsAndSortedInput.NumberOfInversions
                + splitInversionsAndSortedResult.NumberOfInversions;
            return new CountedInversionsAndSortedInput(totalInversions, splitInversionsAndSortedResult.SortedInput);
        }

        private CountedInversionsAndSortedInput MergeHalvesAndCountSplitInversions(int[] left, int[] right)
        {
            var sortedInput = new SortedInput(left, right);
            long numberOfSplitInversions = 0;
            int rightIndex = 0;
            for (int leftIndex = 0; leftIndex < left.Length; leftIndex++)
            {
                int currentLeft = left[leftIndex];
                bool rightHasItems = rightIndex < right.Length;
                if (!rightHasItems)
                {
                    sortedInput.Add(currentLeft);
                }
                else
                {
                    int numberOfUnmergedItemsInLeft = left.Length - leftIndex;
                    AddRightItemsToMergeResultWhenInverted(right, ref rightIndex, sortedInput, currentLeft, numberOfUnmergedItemsInLeft, ref numberOfSplitInversions);
                }
            }
            CopyRestOfRightToMergedResult(right, rightIndex, sortedInput);
            return new CountedInversionsAndSortedInput(numberOfSplitInversions, sortedInput.Result);
        }

        private void CopyRestOfRightToMergedResult(int[] right, int rightIndex, SortedInput mergedResult)
        {
            while (rightIndex < right.Length)
            {
                mergedResult.Add(right[rightIndex]);
                rightIndex++;
            }
        }

        private void AddRightItemsToMergeResultWhenInverted(int[] right, ref int rightIndex, SortedInput mergedResult, int currentLeft, int numberOfUnmergedItemsInLeft, ref long numberOfSplitInversions)
        {
            int currentRight = right[rightIndex];
            if (currentLeft < currentRight)
            {
                mergedResult.Add(currentLeft);
            }
            else
            {
                numberOfSplitInversions += numberOfUnmergedItemsInLeft;
                mergedResult.Add(currentRight);
                rightIndex++;
                bool rightHasItems = rightIndex < right.Length;
                if (rightHasItems)
                {
                    AddRightItemsToMergeResultWhenInverted(right, ref rightIndex, mergedResult, currentLeft, numberOfUnmergedItemsInLeft, ref numberOfSplitInversions);
                }
                else
                {
                    mergedResult.Add(currentLeft);
                }
            }
        }

        private sealed class SortedInput
        {
            private int[] sortedInput;
            private int index;

            public int[] Result => sortedInput;

            public SortedInput(int[] left, int[] right)
            {
                sortedInput = new int[left.Length + right.Length];
                index = 0;
            }

            public void Add(int item)
            {
                sortedInput[index] = item;
                index++;
            }
        }
    }
}
