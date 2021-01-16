using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Solutions
{
    public class Day10
    {
        [Fact]
        public void Part1_SmallSample()
        {
            IEnumerable<string> input = new[] {"16", "10", "15", "5", "1", "11", "7", "19", "6", "12", "4"};

            var result = Day10Solution.GetDiffs(input);
            result.Should().BeEquivalentTo(Enumerable.Repeat(1, 7).Concat(Enumerable.Repeat(3, 5)));
        }

        [Fact]
        public void Part1_BiggerSample()
        {
            IEnumerable<string> input = new[] {"28","33","18","42","31","14","46","20","48","47","24","23","49","45","19","38","39","11","1","32","25","35","8","17","7","9","4","2","34","10","3"};

            var result = Day10Solution.GetDiffs(input);
            result.Should().BeEquivalentTo(Enumerable.Repeat(1, 22).Concat(Enumerable.Repeat(3, 10)));
        }
        
        [Fact]
        public void Part1_Input()
        {
            IEnumerable<string> input = Utils.LoadInput("Day10_Part1_Input.txt");

            var result = Day10Solution.GetDiffs(input);
            result.Count(x => x == 1).Should().Be(69);
            result.Count(x => x == 3).Should().Be(34);
        }

        [Fact]
        public void Part2_SmallSample()
        {
            IEnumerable<string> input = new[] {"16", "10", "15", "5", "1", "11", "7", "19", "6", "12", "4"};

            var result = Day10Solution.GetWaysToConnect(input);
            result.Should().Be(8);
        }
        
        [Fact]
        public void Part2_BiggerSample()
        {
            IEnumerable<string> input = new[] {"28","33","18","42","31","14","46","20","48","47","24","23","49","45","19","38","39","11","1","32","25","35","8","17","7","9","4","2","34","10","3"};

            var result = Day10Solution.GetWaysToConnect(input);
            //result.Should().Be(10976L);
            result.Should().Be(19208L);
        }
        
        [Fact]
        public void Part2_Input()
        {
            IEnumerable<string> input = Utils.LoadInput("Day10_Part1_Input.txt");

            var result = Day10Solution.GetWaysToConnect(input);
            result.Should().Be(6044831973376);
        }
    }

    public class Day10Solution
    {
        public static int[] GetDiffs(IEnumerable<string> input)
        {
            var connectors = input.Select(int.Parse).ToArray();
            Array.Sort(connectors);
            var diff = new int[connectors.Length + 1];
            for(var i = 1; i < connectors.Length; i++){
                diff[i] = connectors[i] - connectors[i - 1];
            }

            diff[0] = connectors[0];
            diff[^1] = 3;
            return diff;
        }

        public static long GetWaysToConnect(IEnumerable<string> input)
        {
            var connectors = new [] {0}.Concat(input.Select(int.Parse)).ToArray();
            Array.Sort(connectors);
            Dictionary<int, long> memoized = new();
            return CountRecursively(0);
            
            long CountRecursively(int index)
            {
                if (index == connectors.Length - 1)
                    return 1;
                if (memoized.TryGetValue(index, out var previouslyCalculated))
                    return previouslyCalculated;
                var current = connectors[index];
                var answer = 0L;
                for (var i = index + 1; i < connectors.Length && i < index + 4 ; i++)
                {
                    if (connectors[i] - current <= 3) answer += CountRecursively(i);
                }

                memoized[index] = answer;
                return answer;
            }
        }
    }
}