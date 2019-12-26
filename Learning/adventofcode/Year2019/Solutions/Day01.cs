using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Solutions
{
    public static class Day01
    {
        public static int Part1(FileInfo input) => GetInputFromFile(input).Select(GetFuel).Sum();

        public static int Part2(FileInfo input) => GetInputFromFile(input).Select(GetFuelRecursive).Sum();

        public static int GetFuel(int mass) => mass / 3 - 2;

        public static int GetFuelRecursive(int mass)
        {
            var fuelForMass = GetFuel(mass);
            if (fuelForMass <= 0) return 0;
            return fuelForMass +  GetFuelRecursive(fuelForMass);
        }

        private static IEnumerable<int> GetInputFromFile(FileInfo input)
            => File.ReadLines(input.FullName).Select(int.Parse);
    }
}