using System.IO;
using System.Linq;
using Solutions.Shared;

namespace Solutions
{
    public static class Day08
    {
        
        public static int Part1(FileInfo input)
        {
            using var inputStream = input.OpenRead();
            var canvas = Canvas.Load(inputStream, 25, 6);

            var minZeros = int.MaxValue;
            var result = 0;
            foreach (var (zeros, ones, twos) in canvas.Layers.Select(ToDigitCounts))
            {
                if (zeros < minZeros) result = ones + twos;
            }

            return result;

            static (int zeros, int ones, int twos) ToDigitCounts(Layer layer)
            {
                int zeros = 0, ones = 0, twos = 0;
                foreach (var pixel in layer.AllPixels)
                {
                    switch (pixel)
                    {
                        case 0:
                            zeros += 1;
                            break;
                        case 1:
                            ones += 1;
                            break;
                        case 2:
                            twos += 1;
                            break;
                    }
                }
                return (zeros, ones, twos);
            }

        }
    }
}