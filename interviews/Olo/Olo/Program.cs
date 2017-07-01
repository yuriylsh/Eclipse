using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static System.Console;

namespace Olo
{
    internal static class Program
    {
        private static void Main()
        {
            // skipping all error handling and edge cases for brevity
            var orders = GetOrders().Result;
            var toppingsCount = CountToppings(orders);
            DisplayTop(toppingsCount, 20);

            async Task<Order[]> GetOrders() => JsonConvert.DeserializeObject<Order[]>(
                await new HttpClient().GetStringAsync("http://files.olo.com/pizzas.json"));
        }

        private static IReadOnlyDictionary<Order, int> CountToppings(IEnumerable<Order> orders)
        {
            var topppingsCount = new Dictionary<Order, int>(new OrdersByToppingsComparer());
            foreach (var order in orders)
            {
                if (topppingsCount.TryGetValue(order, out int currentCount))
                {
                    topppingsCount[order] = currentCount + 1;
                }
                else
                {
                    topppingsCount[order] = 1;
                }
            }
            return topppingsCount;
        }

        private static void DisplayTop(IReadOnlyDictionary<Order, int> toppingsCount, int limit)
        {
            var top = toppingsCount.OrderByDescending(GetOccurancesCount).Take(limit);
            var rank = 0;
            foreach (var (order, count) in top)
            {
                WriteLine($"Combination ranked #{++rank} ({count} {(count > 1 ? "entries" : "entry")}):");
                foreach (var topping in order.Toppings)
                {
                    WriteLine("\t" + topping);
                }
            }

            int GetOccurancesCount(KeyValuePair<Order, int> kvp) => kvp.Value;
        }

        public static void Deconstruct(this KeyValuePair<Order, int> kvp, out Order order, out int count)
        {
            order = kvp.Key;
            count = kvp.Value;
        }
    }

    internal class Order
    {
        private string[] _toppingsSorted;

        public string Key {get; private set;}

        public string[] Toppings
        {
            get => _toppingsSorted;
            set
            {
                _toppingsSorted = value.ToArray();
                Array.Sort(_toppingsSorted, StringComparer.OrdinalIgnoreCase);
                Key = string.Concat(_toppingsSorted);
            }
        }
    }

    internal class OrdersByToppingsComparer : IEqualityComparer<Order>
    {
        public bool Equals(Order x, Order y) => StringComparer.OrdinalIgnoreCase.Equals(x.Key, y.Key);

        public int GetHashCode(Order order) => order.Key.GetHashCode();
    }
}
