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

            var result = Day01.Part1(input);

            result.Should().Be(3299598);
        }
        
        [Fact]
        public void Part2_Input_ReturnsCorrectFuelSum()
        {
            var input = new FileInfo(Part1InputFilePath);

            var result = Day01.Part2(input);

            result.Should().Be(4946546);
        }
        
        [Theory]
        [InlineData(12, 2)]
        [InlineData(14, 2)]
        [InlineData(1969, 966)]
        [InlineData(100756, 50346)]
        public void GetFuelRecursive_Examples_ReturnsCorrectFuel(int mass, int expectedFuel) =>
            Day01.GetFuelRecursive(mass).Should().Be(expectedFuel);
        
        
        [Theory]
        [InlineData(12, 2)]
        [InlineData(14, 2)]
        [InlineData(1969, 654)]
        [InlineData(100756, 33583)]
        public void GetFuel_Examples_ReturnsCorrectFuel(int mass, int expectedFuel) =>
            Day01.GetFuel(mass).Should().Be(expectedFuel);

        [Fact]
        public void Input_ShouldExist()
        {
            var input = new FileInfo(Part1InputFilePath);
            
            input.Exists.Should().BeTrue();
        }

        private const string Part1InputFilePath = "Inputs/day01_part1_input.txt";
    }
}