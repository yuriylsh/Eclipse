using System;
using System.Collections.Generic;
using System.Linq;

namespace Solutions.Shared
{
    public class IntcodeComputer
    {
        public enum HaltReason
        {
            Done,
            WaitingForInput
        }
        
        public static (HaltReason reason, int pointer) Run(int[] program, int pointer = 0, Queue<int> input = null, Action<int> output = null)
        {
            input ??= EmptyInput;
            output ??= NoopOutput;
            while (pointer < program.Length)
            {
                var (opcode, paramModes) = GetOpcode(program, pointer);
                if (opcode == HaltOpcode) break;
                var parameters = GetParameters(pointer + 1, paramModes, opcode);
                int pointerIncrement = parameters.Count + 1;
                pointer += pointerIncrement;
                switch (opcode)
                {
                    case AddOpcode:
                        Add(program, parameters);
                        break;
                    case MultiplyOpcode:
                        Multiply(program, parameters);
                        break;
                    case InputOpcode:
                        if (input.Count == 0)
                        {
                            pointer -= pointerIncrement;
                            return (HaltReason.WaitingForInput, pointer);
                        }
                        Input(program, parameters, input);
                        break;
                    case OutputOpcode:
                        Output(program, parameters, output);
                        break;
                    case JumpIfTrueOpcode:
                        pointer = JumpIfTrue(program, parameters) ?? pointer;
                        break;
                    case JumpIfFalseOpcode:
                        pointer = JumpIfFalse(program, parameters) ?? pointer;
                        break;
                    case EqualsOpcode:
                        EqualsHandler(program, parameters);
                        break; 
                    case LessThanOpcode:
                        LessThan(program, parameters);
                        break;
                    default:
                        throw new ArgumentException($"Unknown opcode {opcode}");
                }
            }

            return (HaltReason.Done, pointer);
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
        
        public static IReadOnlyList<IParameter> GetParameters(Index index, int[] modes, int opcode)
        {
            return opcode switch
            {
                AddOpcode => GetModeBasedParameters(3, index, modes),
                MultiplyOpcode => GetModeBasedParameters(3, index, modes),
                InputOpcode => new IParameter[] {new PositionParameter(index)},
                OutputOpcode => GetModeBasedParameters(1, index, modes),
                JumpIfFalseOpcode => GetModeBasedParameters(2, index, modes),
                JumpIfTrueOpcode => GetModeBasedParameters(2, index, modes),
                EqualsOpcode => GetModeBasedParameters(3, index, modes),
                LessThanOpcode => GetModeBasedParameters(3, index, modes),
                _ => Array.Empty<IParameter>()
            };
        }
        
        private static IReadOnlyList<IParameter> GetModeBasedParameters(int count, Index index, int[] modes)
        {
            Span<int> normalizedModes = stackalloc int[count];
            var result = new IParameter[count];
            modes.AsSpan().CopyTo(normalizedModes);
            for (var i = 0; i < normalizedModes.Length; i++)
            {
                Index paramIndex = index.Value + i;
                result[i] = normalizedModes[i] switch
                {
                    0 => new PositionParameter(paramIndex),
                    _ => new ImmediateParameter(paramIndex)
                };
            }
            return result;
        }

        private static void Add(Span<int> program, IReadOnlyList<IParameter> parameters) => 
            parameters[2].Write(program, parameters[0].Read(program) + parameters[1].Read(program));

        private static void Multiply(Span<int> program, IReadOnlyList<IParameter> parameters) =>
            parameters[2].Write(program, parameters[0].Read(program) * parameters[1].Read(program));

        private static void Input(Span<int> program, IReadOnlyList<IParameter> parameters, Queue<int> input) =>
            parameters[0].Write(program, input.Dequeue());

        private static void Output(Span<int> program, IReadOnlyList<IParameter> parameters, Action<int> output) =>
            output(parameters[0].Read(program));

        private static int? JumpIfTrue(int[] program, IReadOnlyList<IParameter> parameters) =>
            parameters[0].Read(program) != 0 ? parameters[1].Read(program) : (int?)null;

        private static int? JumpIfFalse(int[] program, IReadOnlyList<IParameter> parameters) =>
            parameters[0].Read(program) == 0 ? parameters[1].Read(program) : (int?)null;

        private static void LessThan(int[] program, IReadOnlyList<IParameter> parameters) =>
            parameters[2].Write(program, parameters[0].Read(program) < parameters[1].Read(program) ? 1 : 0);

        private static void EqualsHandler(int[] program, IReadOnlyList<IParameter> parameters) =>
            parameters[2].Write(program, parameters[0].Read(program) == parameters[1].Read(program) ? 1 : 0);

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

        public const int JumpIfTrueOpcode = 5;

        public const int JumpIfFalseOpcode = 6;
        
        public const int LessThanOpcode = 7; 
        
        public const int EqualsOpcode = 8; 

        private const int MaxTwoDigitsNumber = 99;
        
        private static readonly Queue<int> EmptyInput = new Queue<int>(0);
        public static void NoopOutput(int _){}
    }
}