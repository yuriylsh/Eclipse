using System;

namespace Inversions
{
    public class InversionsCounter
    {
        public int Count(int[] input)
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
            var splitInversionsAndSortedResult = MergeHalvesAndCountSplitInversions(halves.Left, halves.Right);
            int totalInversions =
                leftInversionsAndSortedInput.NumberOfInversions
                + rightInversionsAndSortedInput.NumberOfInversions
                + splitInversionsAndSortedResult.NumberOfInversions;
            return new CountedInversionsAndSortedInput(totalInversions, splitInversionsAndSortedResult.SortedInput);
        }

        private CountedInversionsAndSortedInput MergeHalvesAndCountSplitInversions(int[] left, int[] right)
        {
            throw new NotImplementedException();
        }
    }
}
