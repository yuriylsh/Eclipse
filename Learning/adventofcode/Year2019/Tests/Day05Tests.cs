﻿using System.Collections.Generic;
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
        public void Part2_InputProgram_ProducesCorrectDiagnosticsCode()
        {
            var result = Day05.Part2(new FileInfo("Inputs/day05_part1_input.txt"));

            result.Should().Equal(2808771);
        }
        
        [Fact]
        public void Run_SampleInputOutput_CorrectlyRuns()
        {
            var program = new Program(IntcodeComputer.Parse("3,0,4,0,99"));
            var inputValue = 987;
            program.Input.Enqueue(inputValue);

            program.Run();

            program.Output.Should().Equal(inputValue);
        }

        [Theory]
        [InlineData(7, 999)]
        [InlineData(8, 1000)]
        [InlineData(9, 1001)]
        public void Run_Sample_CorrectlyRuns(int inputValue, int expectedOutput)
        {
            var program = new Program(IntcodeComputer.Parse("3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106, 0, 36, 98, 0, 0, 1002, 21, 125, 20, 4, 20, 1105, 1, 46, 104,999, 1105, 1, 46, 1101, 1000, 1, 20, 4, 20, 1105, 1, 46, 98, 99"));
            program.Input.Enqueue(inputValue);

            program.Run();

            program.Output.Should().Equal(expectedOutput);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(999, 1)]
        public void Run_JumpIfFalse_CorrectlyRuns(int inputValue, int expectedOutput)
        {
            var program = new Program(IntcodeComputer.Parse("3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9"));
            program.Input.Enqueue(inputValue);
            
            program.Run();

            program.Output.Should().Equal(expectedOutput);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(999, 1)]
        public void Run_JumpIfTrue_CorrectlyRuns(int inputValue, int expectedOutput)
        {
            var program = new Program(IntcodeComputer.Parse("3,3,1105,-1,9,1101,0,0,12,4,12,99,1"));
            program.Input.Enqueue(inputValue);
            
            program.Run();

            program.Output.Should().Equal(expectedOutput);
        }

        [Theory]
        [InlineData("3,9,8,9,10,9,4,9,99,-1,8", 8, 1)]
        [InlineData("3,9,8,9,10,9,4,9,99,-1,8", 999, 0)]
        [InlineData("3,3,1108,-1,8,3,4,3,99", 8, 1)]
        [InlineData("3,3,1108,-1,8,3,4,3,99", 999, 0)]
        public void Run_EqualTo_CorrectlyRuns(string code, int inputValue, int expectedOutput)
        {
            var program = new Program(IntcodeComputer.Parse(code));
            program.Input.Enqueue(inputValue);
            
            program.Run();

            program.Output.Should().Equal(expectedOutput);
        }

        [Theory]
        [InlineData("3,9,7,9,10,9,4,9,99,-1,8", 5, 1)]
        [InlineData("3,9,7,9,10,9,4,9,99,-1,8", 8, 0)]
        [InlineData("3,3,1107,-1,8,3,4,3,99", 5, 1)]
        [InlineData("3,3,1107,-1,8,3,4,3,99", 8, 0)]
        public void Run_LessThan_CorrectlyRuns(string code, int inputValue, int expectedOutput)
        {
            var program = new Program(IntcodeComputer.Parse(code));
            program.Input.Enqueue(inputValue);
            
            program.Run();

            program.Output.Should().Equal(expectedOutput);
        }
    }
}