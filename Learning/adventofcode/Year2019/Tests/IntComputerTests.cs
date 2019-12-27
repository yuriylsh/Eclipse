using System;
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

            var result = IntcodeComputer.GetParameters(program, 3, Array.Empty<int>(), IntcodeComputer.AddOpcode);

            result.Should().HaveCount(3);
            result.Dequeue()().Should().Be(3);
            result.Dequeue()().Should().Be(2);
            result.Dequeue()().Should().Be(2);
        }
        
        
        [Fact]
        public void GetParameters_GivenCompoundMultiply_ReturnsCorrectParameters()
        {
            var program = IntcodeComputer.Parse("102,-99,5,4,0,-1");

            var result = IntcodeComputer.GetParameters(program, 1, new[]{1}, IntcodeComputer.MultiplyOpcode);

            result.Should().HaveCount(3);
            result.Dequeue()().Should().Be(-99);
            result.Dequeue()().Should().Be(-1);
            result.Dequeue()().Should().Be(4);
        }
    }
}