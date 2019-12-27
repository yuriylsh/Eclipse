﻿using System.Collections.Generic;
using System.Linq;

namespace Solutions
{
    public static class Day02
    {
        public static int Part1(string program, int noun = 12, int verb = 2)
        {
            var input = Parse(program);
            input[1] = noun;
            input[2] = verb;
            Run(input);
            return input[0];
        }
        
        public static (int noun, int verb) Part2(string program)
        {
            for (int noun = 0; noun <= 99; noun++)
            {
                for (var verb = 0; verb <= 99; verb++)
                {
                    var result = Part1(program, noun, verb);
                    if (result == 19690720)
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
            Run(input);
            return string.Join(',', input);
        }

        private static void Run(int[] program)
        {
            foreach (var opcode in GetOpcodes(program))
            {
                opcode.Execute(program);
            }
        }

        private static IEnumerable<Opcode> GetOpcodes(int[] program)
        {
            for (int i = 0; i < program.Length; i++)
            {
                var current = program[i];
                if (current == 99) break;
                if (current == AddOpcode)
                {
                    yield return new Add(program[++i], program[++i], program[++i]);
                    continue;
                }
                yield return new Multiply(program[++i], program[++i], program[++i]);
            }
        }

        private class Add : Opcode
        {
            private readonly int _operand1;
            private readonly int _operand2;
            private readonly int _target;

            public Add(int operand1, int operand2, int target)
            {
                _operand1 = operand1;
                _operand2 = operand2;
                _target = target;
            }

            public void Execute(int[] program)
            {
                program[_target] = program[_operand1] + program[_operand2];
            }
        }
        
        private class Multiply : Opcode
        {
            private readonly int _operand1;
            private readonly int _operand2;
            private readonly int _target;

            public Multiply(int operand1, int operand2, int target)
            {
                _operand1 = operand1;
                _operand2 = operand2;
                _target = target;
            }

            public void Execute(int[] program)
            {
                program[_target] = program[_operand1] * program[_operand2];
            }
        }
        
        private interface Opcode
        {
            void Execute(int[] program);
        }

        private static int[] Parse(string program) => program.Split(',').Select(int.Parse).ToArray();

        private const int HaltOpcode = 99;
        private const int AddOpcode = 1;
        private const int MultiplyOpcode = 2;

        
    }
}