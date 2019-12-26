using System.IO;
using FluentAssertions;
using Solutions;
using Xunit;

namespace Tests
{
    public class Day01Tests
    {
        
        [Fact]
        public void Part1_Input_ReturnsCorrectFuelSum()
        {
            var input = new FileInfo(Part1InputFilePath);

            var result = Day1_Part1.GetFuelTotal(input);

            result.Should().Be(3299598);
        }
        
        [Theory]
        [InlineData(12, 2)]
        [InlineData(14, 2)]
        [InlineData(1969, 654)]
        [InlineData(100756, 33583)]
        public void Part1_Examples_ReturnsCorrectFuel(int mass, int expectedFuel) =>
            Day1_Part1.GetFuel(mass).Should().Be(expectedFuel);

        [Fact]
        public void Part1_Input_ShouldExist()
        {
            var input = new FileInfo(Part1InputFilePath);
            
            input.Exists.Should().BeTrue();
        }

        private const string Part1InputFilePath = "Inputs/day01_part1_input.txt";
    }
}