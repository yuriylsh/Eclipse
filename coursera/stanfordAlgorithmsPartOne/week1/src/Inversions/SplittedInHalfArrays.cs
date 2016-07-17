namespace Inversions
{
    public class SplittedInHalfArrays
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
            int resultLength = subEndIndex - subStartIndex + 1;
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