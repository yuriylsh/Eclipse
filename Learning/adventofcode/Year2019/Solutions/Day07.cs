using System;
using System.Collections.Generic;
using System.Linq;
using Solutions.Shared;

namespace Solutions
{
    public static class Day07
    {
        public static (int, int[]) Task1(string program)
        {
            var initialMemory = IntcodeComputer.Parse(program);
            var memory = new int[initialMemory.Length];
            var max = 0;
            int[] maxPhases = null;
            
            foreach (var phases in GetPhases(new[] {0, 1, 2, 3, 4}))
            {
                initialMemory.AsSpan().CopyTo(memory);
                var current = Run(memory, phases);
                if (current > max)
                {
                    max = current;
                    maxPhases = phases;
                }
            }

            return (max, maxPhases);
        }

        public static int Run(int[] memory, int[] phases)
        {
            var input = new Queue<int>();
            var runResult = 0;
            for (var i = 0; i < 5; i++)
            {
                input.Enqueue(phases[i]);
                input.Enqueue(runResult);
                IntcodeComputer.Run(memory, input, x => runResult = x);
            }
            return runResult;
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