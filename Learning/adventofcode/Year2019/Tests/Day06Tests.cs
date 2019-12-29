using System.Collections.Generic;
using FluentAssertions;
using Solutions.Shared;
using Xunit;

namespace Tests
{
    public class Day06Tests
    {

        [Theory]
        [InlineData(7, 999)]
        [InlineData(8, 1000)]
        [InlineData(9, 1001)]
        public void Run_Sample_CorrectlyRuns(int inputValue, int expectedOutput)
        {
            var program = IntcodeComputer.Parse("3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106, 0, 36, 98, 0, 0, 1002, 21, 125, 20, 4, 20, 1105, 1, 46, 104,999, 1105, 1, 46, 1101, 1000, 1, 20, 4, 20, 1105, 1, 46, 98, 99");
            var input = new Queue<int>();
            input.Enqueue(inputValue);
            int output = -1;

            IntcodeComputer.Run(program, input, output: x => output = x);

            output.Should().Be(expectedOutput);
        }

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

        [Theory]
        [InlineData("3,9,8,9,10,9,4,9,99,-1,8", 8, 1)]
        [InlineData("3,9,8,9,10,9,4,9,99,-1,8", 999, 0)]
        [InlineData("3,3,1108,-1,8,3,4,3,99", 8, 1)]
        [InlineData("3,3,1108,-1,8,3,4,3,99", 999, 0)]
        public void Run_EqualTo_CorrectlyRuns(string code, int inputValue, int expectedOutput)
        {
            var program = IntcodeComputer.Parse(code);
            var input = new Queue<int>();
            input.Enqueue(inputValue);
            int output = -1;
            
            IntcodeComputer.Run(program, input,  output: x => output = x);

            output.Should().Be(expectedOutput);
        }

        [Theory]
        [InlineData("3,9,7,9,10,9,4,9,99,-1,8", 5, 1)]
        [InlineData("3,9,7,9,10,9,4,9,99,-1,8", 8, 0)]
        [InlineData("3,3,1107,-1,8,3,4,3,99", 5, 1)]
        [InlineData("3,3,1107,-1,8,3,4,3,99", 8, 0)]
        public void Run_LessThan_CorrectlyRuns(string code, int inputValue, int expectedOutput)
        {
            var program = IntcodeComputer.Parse(code);
            var input = new Queue<int>();
            input.Enqueue(inputValue);
            int output = -1;
            
            IntcodeComputer.Run(program, input,  output: x => output = x);

            output.Should().Be(expectedOutput);
        }
    }
}