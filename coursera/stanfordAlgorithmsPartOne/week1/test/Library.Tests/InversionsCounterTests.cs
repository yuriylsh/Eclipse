using System;
using System.IO;
using Inversions;
using Xunit;

namespace Tests
{
    public class AssertionData
    {
        public AssertionData(string filePath, long numberOfInversions)
        {
            FilePath = filePath;
            NumberOfInversions = numberOfInversions;
        }

        public string FilePath { get; }
        public long NumberOfInversions { get; }
        public static implicit operator AssertionData(string str)
        {
            var parts = str.Split(',');
            return new AssertionData(parts[0], long.Parse(parts[1]));
        }
    }

    public class InversionsCounterTest
    {
        [Theory]
        [InlineData("singleNumber.txt,0")]
        [InlineData("exampleFromLecture.txt,3")]
        [InlineData("reverseSorted.txt,21")]
        [InlineData("assignment_week_1_input.txt,2407905288")]
        public void Test1(string assertionDataString)
        {
            AssertionData assertionData = assertionDataString;
            var counter = new InversionsCounter();
            long numberOfInversions = counter.Count(GetInputNumbers(assertionData.FilePath));
            Assert.Equal(assertionData.NumberOfInversions, numberOfInversions);
        }

        private int[] GetInputNumbers(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            int[] result = new int[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                result[i] = int.Parse(lines[i]);
            }
            return result;
        }
    }
}
