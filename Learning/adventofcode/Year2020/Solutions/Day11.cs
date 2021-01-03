using System;
using System.Buffers;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using FluentAssertions;
using Xunit;

namespace Solutions
{
    public class Day11
    {
        [Fact]
        public void Part1_LoadsData()
        {
            var input = Utils.LoadInput("Day11_Part1_SampleInput.txt");

            var result = Day11Solution.Load(input);

            result.Columns.Should().Be(10);
            result.Rows.Should().Be(10);
            result.Neighbours(2, 2).Should().Be("###..###");
            result.Neighbours(0, 3).Should().Be("#.###");
            result.Neighbours(0, 0).ToArray().Should().BeEquivalentTo("##.");
            result.Neighbours(0, 9).ToArray().Should().BeEquivalentTo("###");
            result.Neighbours(9, 0).ToArray().Should().BeEquivalentTo("#..");
            result.Neighbours(9, 9).ToArray().Should().BeEquivalentTo("##.");
        }
        
        [Fact]
        public void Part1_SampleData_Simulates()
        {
            var input = Utils.LoadInput("Day11_Part1_ActualSampleInput.txt");

            var result = Day11Solution.Simulate(input, x => x.Neighbours, 4);

            result.ToString().Should().Be(@"#.#L.L#.##
#LLL#LL.L#
L.#.L..#..
#L##.##.L#
#.#L.LL.LL
#.#L#L#.##
..L.L.....
#L#L##L#L#
#.LLLLLL.L
#.#L#L#.##");
        }
        
        [Fact]
        public void Part1_Input_Simulates()
        {
            var input = Utils.LoadInput("Day11_Part1_Input.txt");

            var result = Day11Solution.Simulate(input, x => x.Neighbours, 4);

            result.Data.SelectMany(x => x).Count(x => x == '#').Should().Be(2126);
        }
        
        [Fact]
        public void Part2_SampleAll_FindsNeighbours()
        {
            var input = new [] {".......#.","...#.....",".#.......",".........","..#L....#","....#....",".........","#........","...#....."};

            var result = Day11Solution.Load(input).VisibleNeighbours(4, 3);

            result.ToArray().Should().BeEquivalentTo(Enumerable.Repeat('#', 8));
        }
        
        [Fact]
        public void Part2_SampleOne_FindsNeighbours()
        {
            var input = new [] {".............",".L.L.#.#.#.#.","............."};

            var result = Day11Solution.Load(input).VisibleNeighbours(1, 1);

            result.ToArray().Should().BeEquivalentTo("L");
        }
        
        [Fact]
        public void Part2_SampleNone_FindsNeighbours()
        {
            var input = new [] {".##.##.","#.#.#.#","##...##","...L...","##...##","#.#.#.#",".##.##."};

            var result = Day11Solution.Load(input).VisibleNeighbours(3, 3);

            result.Should().BeEmpty();
        }
        
        
        [Fact]
        public void Part2_Input_Simulates()
        {
            var input = Utils.LoadInput("Day11_Part1_Input.txt");

            var result = Day11Solution.Simulate(input, x => x.VisibleNeighbours, 5);

            result.Data.SelectMany(x => x).Count(x => x == '#').Should().Be(123);
        }
        
    }

    public class Day11Solution
    {
        public static Matrix Load(IEnumerable<string> input)
        {
            var result =  new Matrix(input.ToArray());
            return result;
        }

        public static Matrix Simulate(IEnumerable<string> input, Func<Matrix, Func<int, int, string>> getNeighbours, int tolerance)
        {
            var matrix = Load(input);
            var temp = new Matrix(matrix.Data[..]);

            var (original, simulated) = (matrix, temp);
            int swapsCount = 1;

            while (swapsCount > 0)
            {
                (original, simulated) = (simulated, original);
                swapsCount = 0;
                for (var row = 0; row < matrix.Rows; row++)
                {
                    for (var column = 0; column < matrix.Columns; column++)
                    {
                        var current = original.Data[row][column];
                        var neighbours = getNeighbours(original)(row, column);
                        switch (current)
                        {
                            case 'L' when neighbours.All(x => x != '#'):
                                swapsCount += 1;
                                simulated.Data[row][column] = '#';
                                break;
                            case '#' when neighbours.Count(x => x == '#') >= tolerance:
                                swapsCount += 1;
                                simulated.Data[row][column] = 'L';
                                break;
                            default:
                                simulated.Data[row][column] = current;
                                break;
                        }
                    }
                }
            }

            return simulated;
        }
    }

    public class Matrix
    {
        private readonly int columns;
        private readonly int rows;

        public Matrix(string[] data)
        {
            Data = new char[data.Length][];
            for (int i = 0; i < data.Length; i++)
            {
                Data[i] = data[i].ToCharArray();
            }
            columns = data[0].Length;
            rows = data.Length;
        }
        public Matrix(char[][] data)
        {
            Data = new char[data.Length][];
            for (int i = 0; i < data.Length; i++)
            {
                Data[i] = data[i][..];
            }
            columns = data[0].Length;
            rows = data.Length;
        }

        public int Columns => columns;
        public int Rows => rows;
        
        public char[][] Data { get; }

        public override string ToString() => String.Join(Environment.NewLine, Data.Select(x => new string(x)));

        public string Neighbours(int row, int column)
        {
            var rentedBuffer = ArrayPool<(bool, (int, int))>.Shared.Rent(8);
            var current = (row - 1, column - 1);
            rentedBuffer[0] = (IsValid(current, rows, columns),current);
            current = (row - 1, column);
            rentedBuffer[1] = (IsValid(current, rows, columns),current);
            current = (row - 1, column +1);
            rentedBuffer[2] = (IsValid(current, rows, columns),current);
            current = (row, column - 1);
            rentedBuffer[3] = (IsValid(current, rows, columns),current);
            current = (row, column + 1);
            rentedBuffer[4] = (IsValid(current, rows, columns),current);
            current = (row + 1, column - 1);
            rentedBuffer[5] = (IsValid(current, rows, columns),current);
            current = (row + 1, column);
            rentedBuffer[6] = (IsValid(current, rows, columns),current);
            current = (row + 1, column +1);
            rentedBuffer[7] = (IsValid(current, rows, columns),current);

            var length = rentedBuffer.Take(8).Count(x => x.Item1);
            var result =  string.Create(length, (rentedBuffer, Data), static (resultSpan, ctx) =>
            {
                var (rentedBuffer, input) = ctx;
                var resultIndex = 0;
                for (var i = 0; i < 8; i++)
                {
                    var (isValid, (x, y)) = rentedBuffer[i];
                    if (isValid)
                    {
                        resultSpan[resultIndex] = input[x][y];
                        resultIndex++;
                    }
                }
            });
            ArrayPool<(bool, (int, int))>.Shared.Return(rentedBuffer);
            return result;
            
            static bool IsValid((int row, int column) x, int rows1, int columns1) => x switch
            {
                (>=0, >=0) when x.Item1 < rows1 && x.Item2 < columns1 => true,
                _ => false
            };
        }

        public string VisibleNeighbours(int row, int column)
        {
            var result = new StringBuilder(8);
            foreach (var move in AllMoves)
            {
                var moveResult = Move(move, row, column).Select(x => Data[x.Item1][x.Item2]).FirstOrDefault(x => x == '#' || x == 'L');
                if (moveResult != default) result.Append(moveResult);
            }

            return result.ToString();
        }
        
        
        private static readonly Func<(int, int), (int, int)>[] AllMoves = new [] {(-1, -1),(-1, 0),(-1, 1),(0, -1),(0, 1),(1, -1),(1, 0),(1, 1)}
            .Select(x =>
            {
                Func<(int, int), (int, int)> result = y => (x.Item1 + y.Item1, x.Item2 + y.Item2);
                return result;
            }).ToArray();

        private IEnumerable<(int row, int column)> Move(Func<(int, int), (int, int)> move, int originalRow, int originalColumn)
        {
            while(true)
            {
                (originalRow, originalColumn) = move((originalRow, originalColumn));
                if(originalRow >= 0 && originalRow <rows && originalColumn >= 0 && originalColumn < columns)
                    yield return (originalRow, originalColumn);
                else
                    break;
            }
        }
    }
}