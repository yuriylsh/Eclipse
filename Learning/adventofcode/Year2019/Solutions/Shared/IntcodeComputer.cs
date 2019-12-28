﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Solutions.Shared
{
    public class IntcodeComputer
    {
        public static void Run(int[] program, Queue<int> input = null, Action<int> output = null)
        {
            input ??= EmptyInput;
            output ??= NoopOutput;
            for (var i = 0; i < program.Length; i++)
            {
                var (opcode, paramModes) = GetOpcode(program, i);
                if (opcode == HaltOpcode) break;
                var parameters = GetParameters(i + 1, paramModes, opcode);
                i += parameters.Count;
                switch (opcode)
                {
                    case AddOpcode:
                        Add(program, parameters);
                        break;
                    case MultiplyOpcode:
                        Multiply(program, parameters);
                        break;
                    case InputOpcode:
                        Input(program, parameters, input);
                        break;
                    case OutputOpcode:
                        Output(program, parameters, output);
                        break;
                    default:
                        throw new ArgumentException($"Unknown opcode {opcode}");
                }
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
        
        public static Queue<IParameter> GetParameters(Index index, int[] modes, int opcode)
        {
            var result = new Queue<IParameter>();
            switch (opcode)
            {
                case AddOpcode:
                    TwoReadsOneWriteParameters(index, modes, result);
                    break;
                case MultiplyOpcode:
                    TwoReadsOneWriteParameters(index, modes, result);
                    break;
                case InputOpcode:
                    result.Enqueue(new PositionParameter(index));
                    break;
                case OutputOpcode:
                    result.Enqueue(new ImmediateParameter(index));
                    break;
            }

            return result;
        }

        private static void TwoReadsOneWriteParameters(Index index, int[] modes, Queue<IParameter> parameters)
        {
            Span<int> normalizedModes = stackalloc int[3];
            modes.AsSpan().CopyTo(normalizedModes);
            for (var i = 0; i < normalizedModes.Length; i++)
            {
                Index paramIndex = index.Value + i;
                parameters.Enqueue(normalizedModes[i] switch
                {
                    0 => new PositionParameter(paramIndex),
                    _ => new ImmediateParameter(paramIndex)
                });
            }
        }

        private static void Add(Span<int> program, Queue<IParameter> parameters)
        {
            var parameter1 = parameters.Dequeue();
            var parameter2 = parameters.Dequeue();
            parameters.Dequeue().Write(program, parameter1.Read(program) + parameter2.Read(program));
        }
        
        private static void Multiply(Span<int> program, Queue<IParameter> parameters)
        {
            var parameter1 = parameters.Dequeue();
            var parameter2 = parameters.Dequeue();
            parameters.Dequeue().Write(program, parameter1.Read(program) * parameter2.Read(program));
        }
        
        private static void Input(Span<int> program, Queue<IParameter> parameters, Queue<int> input) =>
            parameters.Dequeue().Write(program, input.Dequeue());

        private static void Output(Span<int> program, Queue<IParameter> parameters, Action<int> output) =>
            output(parameters.Dequeue().Read(program));

        public static int[] Parse(string program) => program.Split(',').Select(int.Parse).ToArray();

        public static void SetNounAndVerb(int noun, int verb, Span<int> memory)
        {
            memory[1] = noun;
            memory[2] = verb;
        }

        private const int HaltOpcode = 99;

        public const int AddOpcode = 1;

        public const int MultiplyOpcode = 2;
        
        public const int InputOpcode = 3;

        public const int OutputOpcode = 4;

        private const int MaxTwoDigitsNumber = 99;
        
        private static readonly Queue<int> EmptyInput = new Queue<int>(0);
        static void NoopOutput(int _){}
    }

}