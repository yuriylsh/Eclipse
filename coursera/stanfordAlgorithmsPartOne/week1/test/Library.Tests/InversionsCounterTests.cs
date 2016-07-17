using System.IO;
using Inversions;
using Xunit;

namespace Tests
{
    public class AssertionData
    {
        public AssertionData(string filePath, int numberOfInversions)
        {
            FilePath = filePath;
            NumberOfInversions = numberOfInversions;
        }

        public string FilePath { get; }
        public int NumberOfInversions { get; }
        public static implicit operator AssertionData(string str)
        {
            var parts = str.Split(',');
            return new AssertionData(parts[0], int.Parse(parts[1]));
        }
    }

    public class InversionsCounterTest
    {
        //[Theory]
        //[InlineData("singleNumber.txt,1")]
        //[InlineData("exampleFromLecture.txt,3")]
        //public void Test1(string assertionDataString) 
        //{
        //    AssertionData assertionData = assertionDataString;
        //    var counter = new InversionsCounter();
        //    Assert.Equal(assertionData.NumberOfInversions, 5/*counter.Count(GetInputNumbers(assertionData.FilePath))*/);
        //}

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
