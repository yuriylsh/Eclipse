using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using Solutions;
using Solutions.Shared;
using Xunit;

namespace Tests
{
    public class Day08Tests
    {
        [Fact]
        public void Part1_Input_ReturnsCorrectNumber()
        {
            var input = new FileInfo("Inputs/day08_part1_input.txt");

            var result = Day08.Part1(input);
            
            result.Should().Be(1690);
        }

        [Fact]
        public void Canvas_SampleData_LoadsCorrectly()
        {
            var inputFile = new FileInfo("Inputs/day08_part1_sample.txt");
            using var input = inputFile.OpenRead();

            var canvas = Canvas.Load(input, 3, 2);

            canvas.Layers.Should().HaveCount(2);
            canvas.Layers[0].RowsCount.Should().Be(2);
            canvas.Layers[0].GetRow(0).Should().Equal(1, 2, 3);
            canvas.Layers[0].GetRow(1).Should().Equal(4, 5, 6);
            Action getNonExistentRow = () => canvas.Layers[0].GetRow(2).ToArray();
            getNonExistentRow.Should().Throw<ArgumentException>();
            canvas.Layers[1].RowsCount.Should().Be(2);
            canvas.Layers[1].GetRow(0).Should().Equal(7, 8, 9);
            canvas.Layers[1].GetRow(1).Should().Equal(0, 1, 2);
        }
    }
}