using System;
using System.Collections.Generic;
using System.Linq;

namespace Solutions
{
    public static class Day03
    {
        public static int Part1(string wire1, string wire2)
        {
            var crossings = Coordinates(wire1);
            crossings.IntersectWith(Coordinates(wire2));
            return crossings.Select(coordinate => Math.Abs(coordinate.x) + Math.Abs(coordinate.y)).Min();
        }
        
        public static int Part2(string wire1, string wire2)
        {
            
            var coordinates1 = Coordinates(wire1);
            var coordinates2 = Coordinates(wire2);
            coordinates1.IntersectWith(coordinates2);
            coordinates2.IntersectWith(coordinates1);
            var result = int.MaxValue;
            foreach (var first in coordinates1)
            {
                var second = coordinates2.First(x => CoordinatesOnlyComparer.Instance.Equals(first, x));
                var totalSteps = first.steps + second.steps;
                result = totalSteps < result ? totalSteps : result;
            }

            return result;
        }

        public static HashSet<(int x, int y, int steps)> Coordinates(string wire)
        {
            var offsets = wire.Split(',').Select(s => s switch
            {
                var up when up[0] == 'U' => Enumerable.Repeat((0, 1), int.Parse(up.AsSpan(1))),
                var down when down[0] == 'D' => Enumerable.Repeat((0, -1), int.Parse(down.AsSpan(1))),
                var right when right[0] == 'R' => Enumerable.Repeat((1, 0), int.Parse(right.AsSpan(1))),
                var left when left[0] == 'L' => Enumerable.Repeat((-1, 0), int.Parse(left.AsSpan(1))),
                var unknown => throw new Exception("Unknown direction: " + unknown)
            }).SelectMany(segmentOffsets => segmentOffsets);
            
            var result = new HashSet<(int, int, int)>(CoordinatesOnlyComparer.Instance);
            var x = 0;
            var y = 0;
            var steps = 0;
            
            foreach (var (deltaX, deltaY) in offsets)
            {
                result.Add((x += deltaX, y += deltaY, ++steps));
            }

            return result;
        }

        private class CoordinatesOnlyComparer : IEqualityComparer<(int x, int y, int steps)>
        {
            public static readonly CoordinatesOnlyComparer Instance = new CoordinatesOnlyComparer();
            
            public bool Equals((int x, int y, int steps) a, (int x, int y, int steps) b)
                => (a.x, a.y).Equals((b.x, b.y));

            public int GetHashCode((int x, int y, int steps) obj) => (obj.x, obj.y).GetHashCode();
        }
    }
}