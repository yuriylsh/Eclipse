using System;
using System.Collections.Generic;
using System.Linq;

namespace Solutions
{
    public static class Day04
    {
        public static int Part1(int start, int end) => Enumerable.Range(start, end - start + 1).Count(MeetsCriteria);

        public static bool MeetsCriteria(int number)
        {
            var input = ToArray(number);
            var criteria = new Predicate<int[]>[] {HasTwoConsecutiveEqualDigits, NonDecreasing};
            return MeetsCriteria(input, criteria);
        }

        private static bool MeetsCriteria(int[] number, IEnumerable<Predicate<int[]>> predicates) 
            => predicates.All(x => x(number));

        private static bool HasTwoConsecutiveEqualDigits(int[] number)
        {
            for (var i = 0; i < number.Length - 1; i++)
            {
                if (number[i] == number[i + 1]) return true;
            }

            return false;
        }

        private static bool NonDecreasing(int[] number)
        {
            for (int i = 0; i < number.Length - 1; i++)
            {
                if (number[i] > number[i + 1]) return false;
            }

            return true;
        }
            
        private static int[] ToArray(int x)
        {
            var length = (int) Math.Log10(x) + 1;
            var result = new int[length];
            for (var i = length - 1; i >=0; i--)
            {
                result[i] = x % 10;
                x /= 10;
            }

            return result;
        }
    }
}