using System;
using Xunit;

namespace Tests
{
    public class AssertionData
    {
        public AssertionData(string filePath, int result)
        {
            FilePath = filePath;
            Result = result;
        }

        public string FilePath { get; }
        public int Result { get; }
        public static implicit operator AssertionData(string str)
        {
            var parts = str.Split(',');
            return new AssertionData(parts[0], int.Parse(parts[1]));
        }
    }

    public class InversionsTest
    {
        [Theory]
        [InlineData("path1,5")]
        public void Test1(string assertionDataString) 
        {
            AssertionData assertionData = assertionDataString;
            var counter = new InversionsCounter();
            Assert.Equal(assertionData.FilePath.Length, assertionData.Result);
        }
    }
}
