using System.Collections.Generic;
using FluentAssertions;
using Solutions.Shared;
using Xunit;

namespace Tests
{
    public class Day06Tests
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(999, 1)]
        public void Run_JumpIfFalse_CorrectlyRuns(int inputValue, int expectedOutput)
        {
            var program = IntcodeComputer.Parse("3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9");
            var input = new Queue<int>();
            input.Enqueue(inputValue);
            int output = -1;
            
            IntcodeComputer.Run(program, input,  output: x => output = x);

            output.Should().Be(expectedOutput);
        }
        
        [Theory]
        [InlineData(0, 0)]
        [InlineData(999, 1)]
        public void Run_JumpIfTrue_CorrectlyRuns(int inputValue, int expectedOutput)
        {
            var program = IntcodeComputer.Parse("3,3,1105,-1,9,1101,0,0,12,4,12,99,1");
            var input = new Queue<int>();
            input.Enqueue(inputValue);
            int output = -1;
            
            IntcodeComputer.Run(program, input,  output: x => output = x);

            output.Should().Be(expectedOutput);
        }
    }
}