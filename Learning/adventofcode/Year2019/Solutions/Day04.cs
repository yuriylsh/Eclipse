using System;

namespace Solutions
{
    public static class Day04
    {
        public static bool MeetsCirteria(int number)
        {
            var a = ToArray(number);
            return true;
        }

        private static int[] ToArray(int x)
        {
            var length = (int) Math.Log10(x) + 1;
            var result = new int[length];
            for (var i = 1; i <= length; i++)
            {
                result[^i] = x % 10;
                x /= 10;
            }

            return result;
        }
    }
}