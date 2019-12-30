﻿using FluentAssertions;
using Solutions;
using Xunit;

namespace Tests
{
    public class Day07Tests
    {
        [Theory]
        [InlineData("3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0", 43210)]
        // [InlineData("3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5,23,23,1,24,23,23,4,23,99,0,0", 54321)]
        // [InlineData("3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0", 65210)]
        public void Task1_SampleData_ReturnsCorrectThrusterSignal(string program, int expected)
        {
            var result = Day07.Task1(program);

            result.Should().Be(expected);
        }
    }
}