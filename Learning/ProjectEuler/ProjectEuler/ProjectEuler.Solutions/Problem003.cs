using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler.Solutions
{
/*
The prime factors of 13195 are 5, 7, 13 and 29.
What is the largest prime factor of the number 600851475143 ?
*/
    public static class Problem003
    {
        public static IEnumerable<int> GetPrimes(int n)
        {
            // https://en.wikipedia.org/wiki/Sieve_of_Eratosthenes
            var sieve = new bool[n];
            var middle = GetMidPoint(n);
            for (var i = 2; i <= middle; i++)
            {
                FlipMultiplesOf(i, sieve);
            }

            for (int i = 0; i < n; i++)
            {
                if (sieve[i] == false) yield return i + 1;
            }
            
            static void FlipMultiplesOf(in int x, bool[] target)
            {
                var toFlip = x + x;
                while (toFlip < target.Length + 1)
                {
                    target[toFlip - 1] = true;
                    toFlip += x;
                }
            }
        }

        public static IEnumerable<int> GetPrimeMultiples(long n)
        {
            return new[] {5, 7, 13, 29};
        }

        private static int GetMidPoint(long n) => (int) Math.Sqrt(n);
    }
}