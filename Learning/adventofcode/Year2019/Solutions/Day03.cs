using System;
using System.Collections.Generic;
using System.Linq;

namespace Solutions
{
    public static class Day03
    {
        public static int Part1(string wire1, string wire2)
        {
            HashSet<(int x, int y)> crossings = Coordinates(wire1);
            crossings.IntersectWith(Coordinates(wire2));
            return crossings.Select(coordinate => Math.Abs(coordinate.x) + Math.Abs(coordinate.y)).Min();
        }

        public static HashSet<(int, int)> Coordinates(string wire)
        {
            var offsets = wire.Split(',').Select(s => s switch
            {
                var up when up[0] == 'U' => Enumerable.Repeat((0, 1), int.Parse(up.AsSpan(1))),
                var down when down[0] == 'D' => Enumerable.Repeat((0, -1), int.Parse(down.AsSpan(1))),
                var right when right[0] == 'R' => Enumerable.Repeat((1, 0), int.Parse(right.AsSpan(1))),
                var left when left[0] == 'L' => Enumerable.Repeat((-1, 0), int.Parse(left.AsSpan(1))),
                var unknown => throw new Exception("Unknown direction: " + unknown)
            }).SelectMany(segmentOffsets => segmentOffsets);
            
            var result = new HashSet<(int, int)>();
            var x = 0;
            var y = 0;
            
            foreach (var (deltaX, deltaY) in offsets)
            {
                result.Add((x += deltaX, y += deltaY));
            }

            return result;
        }
    }
}