using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
            
            Day08Solution.Execute(input, accumulator, x => x.Executed);
        }
        
        
    }

    public class Day08Solution
    {
        public static void Execute(IEnumerable<string> input, Accumulator accumulator, Predicate<Operation> haltBefore)
        {
            var program = input.Select(Parse).ToArray();
            var pointer = 0;
            while (true)
            {
                var current = program[pointer];
                if (haltBefore(current)) break;
                switch (current.Name)
                {
                    case "acc":
                        accumulator.Add(current.Operand);
                        pointer += 1;
                        break;
                    case "nop":
                        pointer += 1;
                        break;
                    case "jmp":
                        pointer += current.Operand;
                        break;
                }
                
            }

        }

        private static readonly Regex OperandRegex = new Regex(@"(\w{3})\s([+-]\d+)", RegexOptions.Compiled);
        private static Operation Parse(string input, int lineNumber)
        {
            var match = OperandRegex.Match(input);
            var name = match.Groups[1].Value;
            var operand = int.Parse(match.Groups[2].Value);
            return Operations[name] with {Operand = operand, Line = lineNumber};
        }

        private static readonly Dictionary<string, Operation> Operations = new()
        {
            ["nop"] = new Operation("nop", 0, -1, false),
            ["acc"] = new Operation("acc", 0, -1, false),
            ["jmp"] = new Operation("jmp", 0, -1, false)
        };
    }

    public record Operation(string Name, int Operand, int Line, bool Executed);

    public class Accumulator
    {
        public long Value { get; private set; }

        public void Add(int value) => Value += value;
    }
}