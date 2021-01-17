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

            result.Should().Be(6386593869035L);
        }

        [Theory]
        [InlineData("000000000000000000000000000000X1001X", 42, "26,27,58,59")]
        [InlineData("00000000000000000000000000000000X0XX", 26, "16,17,18,19,24,25,26,27")]
        public void Part2_DecodeMemory_ReturnsCorrectMemoryAddresses(string mask, int address, string expectedAddresses)
        {
            var expected = expectedAddresses.Split(',').Select(int.Parse);

            var result = Day14Solution.DecodeMemory(address, mask);

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Part2_SampleInput_CorrectMemory()
        {
            var input = new[]
            {
                "mask = 000000000000000000000000000000X1001X",
                "mem[42] = 100",
                "mask = 00000000000000000000000000000000X0XX",
                "mem[26] = 1"
            };

            var result = Day14Solution.Interpret2(input);

            result.Should().Be(208L);
        }
        
        [Fact]
        public void Part2_Input_CorrectMemory()
        {
            var input = Utils.LoadInput("Day14_Part1_Input.txt");

            var result = Day14Solution.Interpret2(input);

            result.Should().Be(4288986482164L);
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
        }
        
        private static long ZeroBit(long value, int i) => value & ~(1L << i);
        private static long OneBit(long value, int i) => value | (1L << i);
        private static bool IsBitSet(long x, int index) => ((1L << index) & x) > 0;

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
        
        public static long Interpret2(IEnumerable<string> input)
        {
            var commands = Parse(input);
            var memory = new Dictionary<long, long>();
            foreach (var command in commands)
            {
                var mask = command.Mask;
                foreach (var mem in command.Ops)
                {
                    foreach (var address in DecodeMemory(mem.location, mask))
                    {
                        memory[address] = mem.value;
                    }
                }
            }

            return memory.Sum(x => x.Value);
        }

        public static IEnumerable<long> DecodeMemory(long address, string mask)
        {
            var size = mask.Length;
            var maskedAddress = string.Create(size, (mask, address), (span, context) =>
            {
                var length = span.Length;
                for (var i = 0; i < length; i++)
                {
                    var index = length - i - 1;
                    span[index] = context.mask[index] switch
                    {
                        '0' => IsBitSet(context.address, i) ? '1' : '0',
                        char ch => ch
                    };
                }
            });
            var result = new List<long>((int)PowerOfTwo(maskedAddress.Count(x => x == 'X')))
            {
                address
            };

            for (var i = 0; i < size; i++)
            {
                var index = size - i - 1;
                switch (maskedAddress[index])
                {
                    case '0':
                        break;
                    case '1':
                        AddForEach(result, PowerOfTwo(i));
                        break;
                    case 'X':
                        var newAddresses = result.Select(x => x + PowerOfTwo(i)).ToArray();
                        result.AddRange(newAddresses);
                        break;
                }
            }

            AddForEach(result, address * -1);
            return result;

            static long PowerOfTwo(int power) => (long) Math.Pow(2, power);

            static void AddForEach(IList<long> target, long value)
            {
                for (int i = 0; i < target.Count; i++)
                {
                    target[i] += value;
                }
            }
        }
    }

    public class MemOperation
    {
        public string Mask { get; init; }
        public List<(int location, long value)> Ops { get; init; }
    }

}