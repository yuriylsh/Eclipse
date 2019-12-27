using System;
using System.Collections.Generic;
using System.Linq;

namespace Solutions.Shared
{
    public class IntcodeComputer
    {
        public static void Run(int[] program)
        {
            for (var i = 0; i < program.Length; i++)
            {
                var (opcode, paramModes) = GetOpcode(program, i);
                if (opcode == HaltOpcode) break;
                var parameters = GetParameters(program, i + 1, paramModes, opcode);
                i += parameters.Count;
                if (opcode == AddOpcode)
                {
                    Add(program, parameters);
                    continue;
                }
                Multiply(program, parameters);
            }
        }

        private static readonly Index OpcodeIndex = ^2;
        public static (int opcode, int[] paramMode) GetOpcode(Span<int> program,Index index)
        {
            var current = program[index];
            var currentDigits = Utils.ToArray(current);
            return current > MaxTwoDigitsNumber 
                ? (ToOpcode(currentDigits), GetParams(currentDigits)) 
                : (current, Array.Empty<int>());
            
            static int ToOpcode(int[] current)
            {
                var opcodeDigits = current.AsSpan(OpcodeIndex);
                return opcodeDigits[0] * 10 + opcodeDigits[1];
            }

            static int[] GetParams(int[] digits)
            {
                var rightToLeftParams = digits.AsSpan(..OpcodeIndex);
                var result = new int[rightToLeftParams.Length];
                for (var i = rightToLeftParams.Length -1; i >=0; i--)
                {
                    result[^(i + 1)] = rightToLeftParams[i];
                }

                return result;
            }
        }

        private static void Add(Span<int> program, Queue<Func<int>> parameters)
        {
            var parameter1 = parameters.Dequeue()();
            var parameter2 = parameters.Dequeue()();
            program[parameters.Dequeue()()] = parameter1 + parameter2;
        }
        
        private static void Multiply(Span<int> program, Queue<Func<int>> parameters)
        {
            var parameter1 = parameters.Dequeue()();
            var parameter2 = parameters.Dequeue()();
            program[parameters.Dequeue()()] = parameter1 * parameter2;
        }

        public static int[] Parse(string program) => program.Split(',').Select(int.Parse).ToArray();

        public static void Clone(int[] program, Span<int> target) => program.AsSpan().CopyTo(target);

        public static void SetNounAndVerb(int noun, int verb, Span<int> memory)
        {
            memory[1] = noun;
            memory[2] = verb;
        }

        private const int HaltOpcode = 99;
        public const int AddOpcode = 1;
        public const int MultiplyOpcode = 2;
        private const int MaxTwoDigitsNumber = 99;
        public static Queue<Func<int>> GetParameters(int[] program, Index index, int[] modes, int opcode)
        {
            var result = new Queue<Func<int>>();
            switch (opcode)
            {
                case AddOpcode:
                    Span<int> addModes = stackalloc int[3];
                    addModes[^1] = 1;
                    for (var i = 0; i < addModes.Length; i++)
                    {
                        Index paramIndex = index.Value + i;
                        result.Enqueue(addModes[i] switch
                        {
                            0 => () => program[program[paramIndex]],
                            _ => () => program[paramIndex]
                        });
                    }
                    break;
                case MultiplyOpcode:
                    Span<int> multiplyModes = stackalloc int[3];
                    multiplyModes[^1] = 1;
                    modes.AsSpan().CopyTo(multiplyModes);
                    for (var i = 0; i < multiplyModes.Length; i++)
                    {
                        Index paramIndex = index.Value + i;
                        result.Enqueue(multiplyModes[i] switch
                        {
                            0 => () => program[program[paramIndex]],
                            _ => () => program[paramIndex]
                        });
                    }
                    break;
            }

            return result;
        }
    }
}