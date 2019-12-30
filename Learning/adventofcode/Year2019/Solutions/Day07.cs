
using System.Collections.Generic;
using System.Linq;

namespace Solutions
{
    public static class Day07
    {
        public static int Task1(string program)
        {
            foreach (var phases in GetPhases())
            {
                var currentPhase = string.Join(',', phases);
            }

            return 33;
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