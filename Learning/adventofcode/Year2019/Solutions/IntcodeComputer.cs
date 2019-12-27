using System;
using System.Linq;

namespace Solutions
{
    public class IntcodeComputer
    {
        public static string Run(string program)
        {
            var input = Parse(program);
            Run(input);
            return string.Join(',', input);
        }

        public static void Run(Span<int> program)
        {
            for (var i = 0; i < program.Length; i++)
            {
                var current = program[i];
                if (current == HaltOpcode) break;
                if (current == AddOpcode)
                {
                    i += Add(program, i);
                    continue;
                }
                i += Multiply(program, i);
            }
        }


        private static int Add(Span<int> program, int index)
        {
            var targetIndex = program[index + 3];
            var parameter1Index = program[index + 1];
            var parameter2Index = program[index + 2];
            program[targetIndex] = program[parameter1Index] + program[parameter2Index];
            return 3;
        }
        
        private static int Multiply(Span<int> program, int index)
        {
            var targetIndex = program[index + 3];
            var parameter1Index = program[index + 1];
            var parameter2Index = program[index + 2];
            program[targetIndex] = program[parameter1Index] * program[parameter2Index];
            return 3;
        }


        public static int[] Parse(string program) => program.Split(',').Select(int.Parse).ToArray();

        public static void Clone(int[] program, Span<int> target) => program.AsSpan().CopyTo(target);

        public static void SetNounAndVerb(int noun, int verb, Span<int> memory)
        {
            memory[1] = noun;
            memory[2] = verb;
        }

        private const int HaltOpcode = 99;
        private const int AddOpcode = 1;
        private const int MultiplyOpcode = 2;
    }
}