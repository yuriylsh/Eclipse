namespace Inversions
{
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
}