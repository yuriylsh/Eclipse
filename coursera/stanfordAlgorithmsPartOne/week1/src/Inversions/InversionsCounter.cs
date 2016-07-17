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

    internal class CountedInversionsAndSortedInput
    {
        public CountedInversionsAndSortedInput()
        {

        }

        public CountedInversionsAndSortedInput(int numberOfInversions, int[] sortedInput)
        {
            NumberOfInversions = numberOfInversions;
            SortedInput = sortedInput;
        }

        public int NumberOfInversions;
        public int[] SortedInput;
    }

    internal class SplittedInHalfArrays
    {
        public int[] Left { get; }

        public int[] Right { get; }

        public SplittedInHalfArrays(int[] array)
        {
            if (array.Length == 2)
            {
                Left = new[] { array[0] };
                Right = new[] { array[1] };
            }
            else
            {
                int midPoint = array.Length / 2;
                Left = CreateSubArray(array, 0, midPoint);
                Right = CreateSubArray(array, midPoint + 1, array.Length - 1);
            }
        }

        private int[] CreateSubArray(int[] input, int subStartIndex, int subEndIndex)
        {
            int resultLength = subEndIndex - subStartIndex;
            var result = new int[resultLength];
            for (int inputIndex = subStartIndex; inputIndex <= subEndIndex ; inputIndex++)
            {
                int resultIndex = inputIndex - subStartIndex;
                result[resultIndex] = input[inputIndex];
            }
            return result;
        }
    }
}
