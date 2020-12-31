using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Solutions
{
    public class Day09
    {
        [Fact]
        public void Part1_Sample()
        {
            var input = Utils.LoadInput("Day09_Part1_SampleInput.txt");

            var result = Day09Solution.GetFirstInvalid(input, 5, 5);

            result.Should().Be(127);
        }
        
        [Fact]
        public void Part1_Input()
        {
            var input = Utils.LoadInput("Day09_Part1_Input.txt");

            var result = Day09Solution.GetFirstInvalid(input, 25, 25);

            result.Should().Be(22406676L);
        }
        
        [Fact]
        public void Part2_Sample()
        {
            var input = Utils.LoadInput("Day09_Part1_SampleInput.txt");

            var result = Day09Solution.GetEncryptionWeakness(input, 5, 5);

            result.Should().Be(62);
        }
        
        [Fact]
        public void Part2_Input()
        {
            var input = Utils.LoadInput("Day09_Part1_Input.txt");

            var result = Day09Solution.GetEncryptionWeakness(input, 25, 25);

            result.Should().Be(2942387L);
        }
    }

    public class Day09Solution
    {
        public static long? GetFirstInvalid(IEnumerable<string> input, int preambleSize, int windowSize)
            => GetFirstInvalid(input.Select(long.Parse).ToArray(), preambleSize, windowSize);
        
        private static long? GetFirstInvalid(long[] items, int preambleSize, int windowSize)
        {
            for (var i = preambleSize; i < items.Length; i++)
            {
                var current = items[i];
                var window = items.AsSpan((i - windowSize)..i);
                if (!HasTwoSum(window, current)) return current;
            }

            return null;
        }
        
        private static bool HasTwoSum(Span<long> input, long target)
        {
            var flags = new HashSet<long>();
            foreach (var x in input)
            {
                var complement = target - x;
                if(flags.Contains(complement))
                {
                    return true;
                }

                flags.Add(x);
            }
            return false;
        }

        public static long GetEncryptionWeakness(IEnumerable<string> input, int preambleSize, int windowSize)
        {
            var items = input.Select(long.Parse).ToArray();
            var invalid = GetFirstInvalid(items, preambleSize, windowSize);
            return FindWeakness(items, invalid.Value);
        }

        private static long FindWeakness(long[] items, long invalid)
        {
            for (int i = 0; i < items.Length; i++)
            {
                var current = items[i];
                var sum = current;
                var next = i + 1;
                while (true)
                {
                    sum += items[next];
                    if (sum == invalid)
                    {
                        var segment = new ArraySegment<long>(items, i, next - i + 1);
                        return segment.Min() + segment.Max();
                    }
                    if (sum > invalid) break;
                    next++;
                }
            }

            return -1;
        }
    }
}