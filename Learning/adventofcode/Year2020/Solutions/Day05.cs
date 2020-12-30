using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Solutions
{
    public class Day05
    {
        [Fact]
        public void BinarySelect_FBString_CorrectValue()
        {
            var input = "FBFBBFF";

            var selection = Day05Solution.BinarySelect(input, 'F', 'B', 128);

            selection.Should().Be(44);

        }

        [Fact]
        public void BinarySelect_LRString_CorrectValue()
        {
            var input = "RLR";

            var selection = Day05Solution.BinarySelect(input, 'L', 'R', 8);

            selection.Should().Be(5);

        }

        [Fact]
        public void Part1_SampleData_CorrectSeat()
        {
            var seat = Day05Solution.GetSeat("FBFBBFFRLR");

            seat.Row.Should().Be(44);
            seat.Column.Should().Be(5);
            seat.Id.Should().Be(357);
        }
        
        [Fact]
        public void Part1_SampleData_CorrectIds()
        {
            var input = new[] {"BFFFBBFRRR", "FFFBBBFRRR", "BBFFBBFRLL"};

            input.Select(x => Day05Solution.GetSeat(x).Id).Should().Equal(567, 119, 820);
        }
        
        
        [Fact]
        public void Part1_InputData_ParsesCorrectly()
        {
            var input = Utils.LoadInput("Day05_Part1_Input.txt");
            input.Max(x => Day05Solution.GetSeat(x).Id).Should().Be(947);
        }
        
        [Fact]
        public void Part2_InputData_ParsesCorrectly()
        {
            var input = Utils.LoadInput("Day05_Part1_Input.txt");
            var seats = input.Select(x => Day05Solution.GetSeat(x).Id).ToArray();
            var max = seats.Max();
            var min = seats.Min();
            var flags = new bool[max + 1];
            foreach (var seat in seats)
            {
                flags[seat] = true;
            }

            var missing = int.MinValue;
            for (int i = 0; i < max; i++)
            {
                if(i <= min) continue;
                if (flags[i] == false)
                {
                    missing = i;
                    break;
                }
            }
            missing.Should().Be(636);
        }
    }

    public class Day05Solution
    {
        public static int BinarySelect(IEnumerable<char> input, char lower, char upper, int range)
        {
            var start = 0;
            var end = range - 1;
            var last = char.MinValue;
            foreach (var current in input)
            {
                last = current;
                var offset = (end - start + 1) / 2;
                if (current == lower) end -= offset;
                else if (current == upper) start += offset;
            }

            return last == lower ? start : end;
        }

        public static Seat GetSeat(string input)
        {
            var row = BinarySelect(input.Take(7), 'F', 'B', 128);
            var column = BinarySelect(input.Skip(7).Take(3), 'L', 'R', 8);
            return new Seat(row, column);
        }
    }

    public record Seat
    {
        public int Row { get; }
        public int Column { get; }
        public int Id { get; }

        public Seat(int row, int column) => (Row, Column, Id) = (row, column, row * 8 + column);
    }
}