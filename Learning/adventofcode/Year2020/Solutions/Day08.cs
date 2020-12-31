using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FluentAssertions;
using Xunit;

namespace Solutions
{
    public class Day08
    {
        [Fact]
        public void Part1_Sample()
        {
            var input = Utils.LoadInput("Day08_Part1_SampleInput.txt");
            var accumulator = new Accumulator();
            
            Day08Solution.Execute(input, accumulator);
            accumulator.Value.Should().Be(5);
        }
        
        [Fact]
        public void Part1_Input()
        {
            var input = Utils.LoadInput("Day08_Part1_Input.txt");
            var accumulator = new Accumulator();
            
            Day08Solution.Execute(input, accumulator);
            accumulator.Value.Should().Be(1723);
        }

        [Fact]
        public void Part2_Sample()
        {
            var input = Utils.LoadInput("Day08_Part1_SampleInput.txt");
            var accumulator = new Accumulator();
            
            Day08Solution.ExecutePart2(input, accumulator);
            accumulator.Value.Should().Be(8);
        }
        
        
        [Fact]
        public void Part2_Input()
        {
            var input = Utils.LoadInput("Day08_Part1_Input.txt");
            var accumulator = new Accumulator();
            
            Day08Solution.ExecutePart2(input, accumulator);
            accumulator.Value.Should().Be(846);
        }
    }

    public class Day08Solution
    {
        public static ExecutionResult Execute(IEnumerable<string> input, Accumulator accumulator)
            => Run(input.Select(Parse).ToArray(), accumulator);
        
        private static ExecutionResult Run(Operation[] program, Accumulator accumulator)
        {
            var executionLog = new bool[program.Length];
            var pointer = 0;
            while (true)
            {
                if (pointer == program.Length) return ExecutionResult.ImmediatelyAfterLastInstruction;
                if (pointer > program.Length) return ExecutionResult.Other;
                var current = program[pointer];
                if (executionLog[pointer]) return ExecutionResult.ReachedLoop;
                executionLog[pointer] = true;
                var newPointerLocation = OperationHandlers[current.Name](current, pointer, accumulator);
                pointer = newPointerLocation;
            }
        }
        
        public static void ExecutePart2(IEnumerable<string> input, Accumulator accumulator)
        {
            var program = input.Select(Parse).ToArray();
            for (var i = 0; i < program.Length; i++)
            {
                var current = program[i];
                if (current is {Name: "jmp"} or {Name: "nop"})
                {
                    var modified = current.Name == "jmp" ? current with {Name = "nop"} : current with {Name = "jmp"};
                    var newProgram = ModifyProgram(program, modified, i);
                    var newAcc = new Accumulator();
                    var result = Run(newProgram, newAcc);
                    if (result == ExecutionResult.ImmediatelyAfterLastInstruction)
                    {
                        accumulator.SetValue(newAcc.Value);
                        return;
                    }
                }
            }
        }

        private static Operation[] ModifyProgram(Operation[] original, Operation modifiedOperation, int index)
        {
            var result = new Operation[original.Length];
            Array.Copy(original, result, original.Length);
            result[index] = modifiedOperation;
            return result;
        }

        private static readonly Regex OperandRegex = new(@"(\w{3})\s([+-]\d+)", RegexOptions.Compiled);
        private static Operation Parse(string input)
        {
            var match = OperandRegex.Match(input);
            var name = match.Groups[1].Value;
            var operand = int.Parse(match.Groups[2].Value);
            return Operations[name] with {Operand = operand};
        }

        private static readonly Dictionary<string, Operation> Operations = new()
        {
            ["nop"] = new Operation("nop", 0),
            ["acc"] = new Operation("acc", 0),
            ["jmp"] = new Operation("jmp", 0)
        };
        
        private static readonly Dictionary<string, OperationHandler> OperationHandlers = new()
        {
            ["nop"] = (_, pointer, _) => pointer + 1,
            ["acc"] = (op, pointer, acc) =>
            {
                acc.Add(op.Operand);
                return pointer + 1;
            },
            ["jmp"] = (op, pointer, _) => pointer + op.Operand
        };
    }

    public delegate int OperationHandler(Operation operation, int currentPointer, Accumulator accumulator);

    public record Operation(string Name, int Operand);

    public class Accumulator
    {
        public long Value { get; private set; }

        public void Add(int value) => Value += value;

        public void SetValue(long newAccValue) => Value = newAccValue;
    }

    public enum ExecutionResult
    {
        ImmediatelyAfterLastInstruction,
        ReachedLoop,
        Other
    }
}