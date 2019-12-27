using System;
using System.Collections.Generic;
using System.Linq;

namespace Solutions
{
    public static class Day04
    {
        public static int Part1(int start, int end) => Enumerable.Range(start, end - start + 1).Count(MeetsPart1Criteria);
        
        public static int Part2(int start, int end) => Enumerable.Range(start, end - start + 1).Count(MeetsPart2Criteria);

        public static bool MeetsPart1Criteria(int number)
        {
            var input = ToArray(number);
            var criteria = new Predicate<int[]>[] {HasTwoConsecutiveEqualDigits, NonDecreasing};
            return MeetsCriteria(input, criteria);
        }
        
        public static bool MeetsPart2Criteria(int number)
        {
            var input = ToArray(number);
            var criteria = new Predicate<int[]>[] {HasOnlyTwoConsecutiveEqualDigits, NonDecreasing};
            return MeetsCriteria(input, criteria);
        }

        private static bool MeetsCriteria(int[] number, IEnumerable<Predicate<int[]>> predicates) 
            => predicates.All(x => x(number));

        private static bool HasOnlyTwoConsecutiveEqualDigits(int[] number)
        {
            Span<int> lengths = stackalloc int[number.Length - 1];
            for (var i = 0; i < lengths.Length; i++)
            {
                var currentDigit = number[i];
                ref var target = ref lengths[i];
                target = 1;
                for (var j = i + 1; j < number.Length; j++)
                {
                    if (currentDigit == number[j])
                    {
                        target += 1;
                        i += 1;
                        continue;
                    }
                    break;
                }
            }

            foreach (var length in lengths)
            {
                if (length == 2) return true;
            }

            return false;
        }
        
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