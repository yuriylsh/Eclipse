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
            
            foreach (var phases in GetPhases())
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

        private static IEnumerable<int[]> GetPhases()
        {
            var orders = new[] {new Order(), new Order(), new Order(), new Order(), new Order()};
            for (var i = 1; i <= 4; i++)
            {
                orders[^i].Parent = orders[^(i + 1)];
            }

            while (true)
            {
                orders[4].Increment();
                var result = orders.Select(GetCurrent).ToArray();
                yield return result;
                if(result.Sum() == 5 * 4) yield break;
            }

            static int GetCurrent(Order x) => x.Current;
        }

        private class Order
        {
            private int _current;
            private Order _parent;

            public int Current => _current;
            
            public void Increment()
            {
                if (_current < 4)
                {
                    _current += 1;
                    return;
                }

                _current = 0;
                _parent?.Increment();
            }

            public Order Parent
            {
                set => _parent = value;
            }
        }
    }
}