using System.IO;
using FluentAssertions;
using Solutions;
using Xunit;

namespace Tests
{
    public class Day05Tests
    {
        [Fact]
        public void Part1_InputProgram_ProducesCorrectDiagnosticsCode()
        {
            var result = Day05.Part1(new FileInfo("Inputs/day05_part1_input.txt"));

            result.Should().Equal(0, 0, 0, 0, 0, 0, 0, 0, 0, 223);
        }
    }
}