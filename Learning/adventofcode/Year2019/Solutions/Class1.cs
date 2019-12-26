using System.IO;
using System.Linq;

namespace Solutions
{
    public static class Day1_Part1
    {
        public static int GetFuel(int mass) => mass / 3 - 2;

        public static int GetFuelTotal(FileInfo input) =>
            File.ReadLines(input.FullName).Select(int.Parse).Select(GetFuel).Sum();
    }
}