using System;
using static Solutions.IntcodeComputer;

namespace Solutions
{
    public static class Day02
    {
        public static int Part1(string program, int noun = 12, int verb = 2)
        {
            var input = Parse(program);
            Span<int> memory = stackalloc int[input.Length];
            Clone(input, memory);
            SetNounAndVerb(noun, verb, memory);
            IntcodeComputer.Run(memory);
            return memory[0];
        }
        
        public static (int noun, int verb) Part2(string program)
        {
            var input = Parse(program);
            Span<int> memory = stackalloc int[input.Length];
            for (var noun = 0; noun <= 99; noun++)
            {
                for (var verb = 0; verb <= 99; verb++)
                {
                    Clone(input, memory);
                    SetNounAndVerb(noun, verb, memory);
                    IntcodeComputer.Run(memory);
                    if (memory[0] == 19690720)
                    {
                        return (noun, verb);
                    }
                }
            }

            return (-1, -1);
        }
        
        public static string Run(string program)
        {
            var input = Parse(program);
            IntcodeComputer.Run(input);
            return string.Join(',', input);
        }

        
    }
}