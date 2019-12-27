using System;

namespace Solutions.Shared
{
    public static class Utils
    {
        public static int[] ToArray(int x)
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