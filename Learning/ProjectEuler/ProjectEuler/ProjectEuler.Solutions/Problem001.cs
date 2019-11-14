using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler.Solutions
{
/*
If we list all the natural numbers below 10 that are multiples of 3 or 5, we get 3, 5, 6 and 9. The sum of these multiples is 23.
Find the sum of all the multiples of 3 or 5 below 1000.
*/
    public class Problem001
    {
        public static int Solve(IEnumerable<int> input) =>
            input.Sum(x => x switch
            {
                var by3 when x % 3 == 0 => by3,
                var by5 when x % 5 == 0 => by5,
                _ => 0
            });
    }
}