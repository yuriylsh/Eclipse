using System;
using System.Collections.Generic;
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

            result.Should().HaveCount(3);
            result.Dequeue().Read(program).Should().Be(3);
            result.Dequeue().Read(program).Should().Be(2);
            result.Dequeue().Read(program).Should().Be(1);
        }
        
        
        [Fact]
        public void GetParameters_GivenCompoundMultiply_ReturnsCorrectParameters()
        {
            var program = IntcodeComputer.Parse("102,-99,5,4,0,-1");

            var result = IntcodeComputer.GetParameters( 1, new[]{1}, IntcodeComputer.MultiplyOpcode);

            result.Should().HaveCount(3);
            result.Dequeue().Read(program).Should().Be(-99);
            result.Dequeue().Read(program).Should().Be(-1);
            result.Dequeue().Read(program).Should().Be(0);
        }
        
        [Fact]
        public void Run_ProgramWithInputOpcode_CorrectlyRuns()
        {
            var program = IntcodeComputer.Parse("03,4,1101,22,33,6,0,99");
            var input = new Queue<int>();
            input.Enqueue(1000);
            IntcodeComputer.Run(program, input);

            program.Should().Equal(3,4,1101,22,1000,6,1022,99);
        }
        
        [Fact]
        public void Run_ProgramWithCompoundAdd_CorrectlyRuns()
        {
            var program = IntcodeComputer.Parse("1101,100,-1,4,0");
            
            IntcodeComputer.Run(program);

            program.Should().Equal(1101,100,-1,4,99);
        }
    }
}