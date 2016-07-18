namespace Inversions
{
    internal class CountedInversionsAndSortedInput
    {
        public CountedInversionsAndSortedInput()
        {
        }

        public CountedInversionsAndSortedInput(long numberOfInversions, int[] sortedInput)
        {
            NumberOfInversions = numberOfInversions;
            SortedInput = sortedInput;
        }

        public long NumberOfInversions;
        public int[] SortedInput;
    }
}