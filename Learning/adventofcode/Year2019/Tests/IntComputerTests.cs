using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Solutions.Shared;
using Xunit;

namespace Tests
{
    public class IntComputerTests
    {
        [Fact]
        public void GetOpcode_GivenSimpleAdd_ReturnsCorrectOpcode()
        {
            var program = IntcodeComputer.Parse("3,100,1,0,4,2,99").AsSpan();
            
            var (opcode, paramModes) = IntcodeComputer.GetOpcode(program, 2);
            
            opcode.Should().Be(IntcodeComputer.AddOpcode);
            paramModes.Should().BeEmpty();
        }
        
        [Fact]
        public void GetOpcode_GivenCompoundMultiply_ReturnsCorrectOpcode()
        {
            var program = IntcodeComputer.Parse("3,100,34202,0,4,2,99").AsSpan();
            
            var (opcode, paramModes) = IntcodeComputer.GetOpcode(program, 2);
            
            opcode.Should().Be(IntcodeComputer.MultiplyOpcode);
            paramModes.Should().Equal(2, 4, 3);
        }
        
        [Fact]
        public void GetParameters_GivenSimpleAdd_ReturnsCorrectParameters()
        {
            var program = IntcodeComputer.Parse("3,100,1,0,5,2,99");

            var result = IntcodeComputer.GetParameters( 3, Array.Empty<int>(), IntcodeComputer.AddOpcode);

            result.Select(x => x.Read(program)).Should().Equal(3, 2, 1);
        }
        
        
        [Fact]
        public void GetParameters_GivenCompoundMultiply_ReturnsCorrectParameters()
        {
            var program = IntcodeComputer.Parse("102,-99,5,4,0,-1");

            var result = IntcodeComputer.GetParameters( 1, new[]{1}, IntcodeComputer.MultiplyOpcode);

            result.Select(x => x.Read(program)).Should().Equal(-99, -1, 0);
        }
        
        [Fact]
        public void Run_ProgramWithInputOpcode_CorrectlyRuns()
        {
            var program = IntcodeComputer.Parse("03,4,1101,-901,33,6,0");
            var input = new Queue<int>();
            input.Enqueue(1000);
            IntcodeComputer.Run(program, input);

            program.Should().Equal(3, 4, 1101, -901, 1000, 6, 99);
        }
        
        [Fact]
        public void Run_ProgramWithCompoundAdd_CorrectlyRuns()
        {
            var program = IntcodeComputer.Parse("1101,100,-1,4,0");
            
            IntcodeComputer.Run(program);

            program.Should().Equal(1101,100,-1,4,99);
        }

        [Fact]
        public void Run_ProgramWithOutput_CorrectlyRuns()
        {
            var program = IntcodeComputer.Parse("4,789,99");
            var output = -1;
            
            IntcodeComputer.Run(program, output: x => output = x);

            output.Should().Be(789);
        }
    }
}