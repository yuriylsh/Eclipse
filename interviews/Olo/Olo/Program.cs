using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Olo
{
    static class Program
    {
        private static readonly IEqualityComparer<Order> OrderComparer = new OrdersByToppingsComparer();

        static void Main()
        {
            // skipping all error handling and edge cases for brevity
            var orders = GetOrders().Result;
            var rankedOrders = RankOrders(orders);
            DisplayTop(rankedOrders, 20);

            async Task<Order[]> GetOrders() => JsonConvert.DeserializeObject<Order[]>(
                await new HttpClient().GetStringAsync("http://files.olo.com/pizzas.json"));
        }

        private static IReadOnlyDictionary<Order, int> RankOrders(IEnumerable<Order> orders)
        {
            var result = new Dictionary<Order, int>(OrderComparer);
            foreach (var order in orders)
            {
                if (result.TryGetValue(order, out int currentRank))
                {
                    result[order] = currentRank + 1;
                }
                else
                {
                    result[order] = 1;
                }
            }
            return result;
        }

        private static void DisplayTop(IReadOnlyDictionary<Order, int> rankedOrders, int topCount)
        {
            var top = rankedOrders.OrderByDescending(GetOccurancesCount).Take(topCount);
            var rank = 0;
            foreach (var (order, count) in top)
            {
                Console.WriteLine($"Combination #{++rank} ({count} {(count > 1 ? "entries" : "entry")}):");
                foreach (var topping in order.Toppings)
                {
                    Console.WriteLine("\t" + topping);
                }
            }
        }

        private static int GetOccurancesCount(KeyValuePair<Order, int> kvp) => kvp.Value;

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
