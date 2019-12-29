using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using Solutions;
using Solutions.Shared;
using Xunit;

namespace Tests
{
    public class Day05Tests
    {
        [Fact]
        public void Part1_InputProgram_ProducesCorrectDiagnosticsCode()
        {
            var result = Day05.Part1(new FileInfo("Inputs/day05_part1_input.txt"));

            result.Should().Equal(0, 0, 0, 0, 0, 0, 0, 0, 0, 16225258);
        }
        
        [Fact]
        public void Run_SampleInputOutput_CorrectlyRuns()
        {
            var program = IntcodeComputer.Parse("3,0,4,0,99");
            var inputValue = 987;
            var input = new Queue<int>();
            input.Enqueue(inputValue);
            int output = -1;
            
            IntcodeComputer.Run(program, input,  output: x => output = x);

            output.Should().Be(inputValue);
        }
    }
}