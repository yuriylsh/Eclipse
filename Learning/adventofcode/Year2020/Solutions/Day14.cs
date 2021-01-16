using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FluentAssertions;
using Xunit;

namespace Solutions
{
    public class Day14
    {
        [Fact]
        public void Part1_SampleInput_CorrectMemory()
        {
            var input = new []{"mask = XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X", "mem[8] = 11", "mem[7] = 101", "mem[8] = 0"};

            var result = Day14Solution.Interpret(input);

            result.Should().Be(165);
        }
        
        [Fact]
        public void Part1_Input_CorrectMemory()
        {
            var input = Utils.LoadInput("Day14_Part1_Input.txt");

            var result = Day14Solution.Interpret(input);

            result.Should().Be(1L);
        }
    }

    public class Day14Solution
    {
        public static long Interpret(IEnumerable<string> input)
        {
            var commands = Parse(input);
            var memory = new Dictionary<int, long>();
            foreach (var command in commands)
            {
                var mask = command.Mask;
                foreach (var mem in command.Ops)
                {
                    memory[mem.location] = ApplyMask(mask, mem.value);
                }
            }

            return memory.Sum(x => x.Value);
        }

        private static long ApplyMask(string mask, long memValue)
        {
            var size = mask.Length;
            for (var i = 0; i < size; i++)
            {
                memValue = mask[size - i - 1] switch
                {
                    '0' => ZeroBit(memValue, i),
                    '1' => OneBit(memValue, i),
                    _ => memValue
                };
            }

            return memValue;

            static long ZeroBit(long value, int i) => value & ~(1L << i);
            static long OneBit(long value, int i) => value | (1L << i);
        }

        private static readonly Regex MemRegex = new Regex(@"mem\[(\d+)\]\s*=\s*(\d+)", RegexOptions.Compiled); 
        private static IEnumerable<MemOperation> Parse(IEnumerable<string> input)
        {
            List<MemOperation> result = new ();
            
            foreach (var line in input)
            {
                var isMaskLine = line[1] == 'a';
                if (isMaskLine)
                {
                    result.Add(new MemOperation
                    {
                        Mask = line.Substring(7),
                        Ops = new List<(int location, long value)>()
                    });
                }
                else
                {
                    var match = MemRegex.Match(line);
                    result[^1].Ops.Add((int.Parse(match.Groups[1].Value), long.Parse(match.Groups[2].Value)));
                }
            }

            return result;
        }
    }

    public class MemOperation
    {
        public string Mask { get; init; }
        public List<(int location, long value)> Ops { get; init; }
    }

}