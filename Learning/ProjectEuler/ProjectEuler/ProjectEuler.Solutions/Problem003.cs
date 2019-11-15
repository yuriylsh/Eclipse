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

            for (int i = 1; i < n; i++)
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

        public static IEnumerable<int> GetPrimeMultiples(long n) => GetPrimes(GetMidPoint(n)).Where(prime => n % prime == 0);

        private static int GetMidPoint(long n) => (int) Math.Sqrt(n);

        public static int MaxPrimeMultiple(long n)
        {
            // Since any prime factor from a number is less than the number, if we start from 2 and scale it up from that,
            // we can assume that the first number that give a 0 modulus is a prime factor of the number.
            // After that, we can divide the original number by its first prime factor, leaving a smaller number,
            // then check if it is still divisible by the same factor. When it stops we increment the number until we find the next factor and so on.
            // Finally, the exit condition is when we divide the number by itself, meaning that it was the last prime factor left.
            // By doing this, we just print the last factor giving us the answer to the problem.
            for (var i = 2; i * i < n; i++)
            {
                if (n % i != 0) continue;
                while (n % i == 0 && n != i) n /= i;
            }
            return (int)n;
        }
    }
}