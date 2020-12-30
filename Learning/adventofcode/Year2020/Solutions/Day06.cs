using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Solutions
{
    public class Day06
    {
        [Fact]
        void Part1_SampleData_CorrectCount()
        {
            var input = Utils.LoadInput("Day06_Part1_SampleInput.txt");

            var result = Day06Solution.GetCounts(input);

            result.Should().Equal(3, 3, 3, 1, 1);
        }
        
        [Fact]
        void Part1_InputData_CorrectCount()
        {
            var input = Utils.LoadInput("Day06_Part1_Input.txt");

            var result = Day06Solution.GetCounts(input);

            result.Sum().Should().Be(6504);
        }
        
        
        [Fact]
        void Part2_SampleData_CorrectCount()
        {
            var input = Utils.LoadInput("Day06_Part1_SampleInput.txt");

            var result = Day06Solution.GetEveryoneCounts(input);

            result.Should().Equal(3, 0, 1, 1, 1);
        }
        
        
        [Fact]
        void Part2_InputData_CorrectCount()
        {
            var input = Utils.LoadInput("Day06_Part1_Input.txt");

            var result = Day06Solution.GetEveryoneCounts(input);

            result.Sum().Should().Be(3351);
        }
    }

    internal class Day06Solution
    {
        private const int Offset = 'a';
        public static IEnumerable<int> GetCounts(IEnumerable<string> input)
        {
            var flags = new bool[26];

            foreach (var group in Utils.GetGrouppedLines(input))
            {
                foreach (var answers in group)
                {
                    foreach (var question in answers)
                    {
                        flags[question - Offset] = true;
                    }
                }
                yield return flags.Count(x => x);
                ResetFlags(flags);
            }

            static void ResetFlags(bool[] flags)
            {
                for (var i = 0; i < flags.Length; i++)
                {
                    flags[i] = false;
                }
            }
        }

        public static IEnumerable<int> GetEveryoneCounts(IEnumerable<string> input)
        {
            var flags = new int[26];

            foreach (var group in Utils.GetGrouppedLines(input))
            {
                foreach (var answers in group)
                {
                    foreach (var question in answers)
                    {
                        flags[question - Offset] += 1;
                    }
                }
                yield return flags.Count(x => x == group.Count);
                ResetFlags(flags);
            }

            static void ResetFlags(int[] flags)
            {
                for (var i = 0; i < flags.Length; i++)
                {
                    flags[i] = 0;
                }
            }
        }
    }
}