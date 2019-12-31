using System;
using System.Collections.Generic;
using System.Linq;
using Solutions.Shared;

namespace Solutions
{
    public static class Day07
    {
        public static (int, int[]) Task1(string program) => Task(program, new[] {0, 1, 2, 3, 4}, Run1);
        public static (int, int[]) Task2(string program) => Task(program, new[] {5, 6, 7, 8, 9}, Run2);

        private static (int, int[]) Task(string program, int[] possibilities, Func<int[], int[], int> run)
        {
            var initialMemory = IntcodeComputer.Parse(program);
            var memory = new int[initialMemory.Length];
            var max = 0;
            int[] maxPhases = null;
            
            foreach (var phases in GetPhases(possibilities))
            {
                initialMemory.AsSpan().CopyTo(memory);
                var current = run(memory, phases);
                if (current > max)
                {
                    max = current;
                    maxPhases = phases;
                }
            }

            return (max, maxPhases);
        }
        
        public static int Run1(int[] memory, int[] phases)
        {
            var runResult = 0;
            for (var i = 0; i < 5; i++)
            {
                var program = new Program(memory);
                program.Input.Enqueue(phases[i]);
                program.Input.Enqueue(runResult);
                program.Run();
                runResult = program.Output[^1];
            }
            return runResult;
        }

        public static int Run2(int[] memory, int[] phases)
        {
            static Program CreateAmplifier(int[] memory, int phase)
            {
                var programMemory = new int[memory.Length];
                Array.Copy(memory, programMemory, memory.Length);
                var program = new Program(programMemory);
                program.Input.Enqueue(phase);
                return program;
            }
            
            var amplifiers = phases.Select(x => CreateAmplifier(memory, x)).ToArray();
            amplifiers[0].Input.Enqueue(0);

            var circuit = new ProgramCircuit(amplifiers);
            circuit.Run();
            
            return amplifiers[^1].Output[^1];
        }

        public static IEnumerable<int[]> GetPhases(int[] possibilities)
        {
            if (possibilities.Length == 1)
            {
                yield return possibilities;
                yield break;
            };
            foreach (var current in possibilities)
            {
                var other = possibilities.Where(x => x != current).ToArray();
                foreach (var otherPermutation in GetPhases(other))
                {
                    var currentResult = new int[possibilities.Length];
                    currentResult[0] = current;
                    otherPermutation.AsSpan().CopyTo(currentResult.AsSpan(1));
                    yield return currentResult;
                }
            }
        }
    }
}