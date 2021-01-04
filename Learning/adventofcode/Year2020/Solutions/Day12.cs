using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FluentAssertions;
using Xunit;

namespace Solutions
{
    public class Day12
    {
        [Fact]
        public void Part1_SampleInput_CorrectCoordinates()
        {
            var input = new[] {"F10", "N3", "F7", "R90", "F11"};

            var coords = Day12Solution.Navigate(input);

            coords.Should().Be((-17, -8));
        }

        [Fact]
        public void Part1_Input_CorrectCoordinates()
        {
            var input = Utils.LoadInput("Day12_Part1_Input.txt");

            var coords = Day12Solution.Navigate(input);

            coords.Should().Be((29, -386));
        }

        [Fact]
        public void Part2_SampleInput_CorrectCoordinates()
        {
            var input = new[] {"F10", "N3", "F7", "R90", "F11"};

            var coords = Day12Solution.Navigate2(input);

            coords.Should().Be((-214, -72));
        }
        
        [Fact]
        public void Part2_leInput_CorrectCoordinates()
        {
            var input = Utils.LoadInput("Day12_Part1_Input.txt");


            var coords = Day12Solution.Navigate2(input);

            coords.Should().Be((-25317, -4084));
        }
    }

    public class Day12Solution
    {
        private static readonly Regex Parser = new(@"([NSEWLRF])(\d+)", RegexOptions.Compiled);

        public static (int x, int y) Navigate(IEnumerable<string> input)
        {
            var commands = input.Select(x => Parser.Match(x))
                .Select(x => (x.Groups[1].Value[0], int.Parse(x.Groups[2].Value)));
            var result = commands.Aggregate(new NavigationState(180, 0, 0),
                (context, command) => Commands[command.Item1](context, command.Item2));
            return (result.x, result.y);
        }

        public static (int x, int y) Navigate2(IEnumerable<string> input)
        {
            var commands = input.Select(x => Parser.Match(x))
                .Select(x => (x.Groups[1].Value[0], int.Parse(x.Groups[2].Value)));

            var state = new NavState(new Waypoint(-10, 1), new Waypoint(0, 0));


            foreach (var (code, arg) in commands)
            {
                Func<NavState, int, NavState> command = code == 'L' || code == 'R'
                    ? Rotations[$"{code}{arg}"]
                    : Commands2[code];

                state = command(state, arg);
            }
            
            
            return (state.ship.x, state.ship.y);
        }

        private static readonly Dictionary<char, Func<NavigationState, int, NavigationState>> Commands = new()
        {
            ['F'] = Forward,
            ['S'] = South,
            ['N'] = North,
            ['E'] = East,
            ['W'] = West,
            ['L'] = Left,
            ['R'] = Right
        };
        
        private static readonly Dictionary<char, Func<NavState, int, NavState>> Commands2 = new()
        {
            ['F'] = Forward2,
            ['S'] = South2,
            ['N'] = North2,
            ['E'] = East2,
            ['W'] = West2
        };
        
        private static readonly Dictionary<string, Func<NavState, int, NavState>> Rotations = new()
        {
            ["R90"] = (state, _) => state with
            {
                waypoint = state.waypoint with {x = state.waypoint.y * -1, y = state.waypoint.x}
            },
            ["L270"] = (state, _) => state with
            {
                waypoint = state.waypoint with {x = state.waypoint.y * -1, y = state.waypoint.x}
            },
            ["R180"] = (state, _) => state with
            {
                waypoint = state.waypoint with {x = state.waypoint.x * - 1, y = state.waypoint.y * -1}
            },
            ["L180"] = (state, _) => state with
            {
                waypoint = state.waypoint with {x = state.waypoint.x * - 1, y = state.waypoint.y * -1}
            },
            ["R270"] = (state, _) => state with
            {
                waypoint = state.waypoint with {x = state.waypoint.y, y = state.waypoint.x * -1}
            },
            ["L90"] = (state, _) => state with
            {
                waypoint = state.waypoint with {x = state.waypoint.y, y = state.waypoint.x * -1}
            }
        };

        private static NavigationState Forward(NavigationState state, int arg) => state.direction switch
        {
            0 or 360 => state with {direction = 0, x = state.x + arg},
            90 => state with {y = state.y + arg},
            180 => state with {x = state.x - arg},
            270 => state with {y = state.y - arg},
            _ => throw new Exception($"Unknown direction {state.direction}")
        };
        
        private static NavState Forward2(NavState state, int arg) =>
            state with {ship = state.ship with
            {
                x = state.ship.x + state.waypoint.x * arg,
                y = state.ship.y + state.waypoint.y * arg
            }};

        private static NavigationState Right(NavigationState state, int arg)
        {
            var newDirection = state.direction + arg;
            return state with { direction = newDirection > 360 ? newDirection - 360 : newDirection};
        }

        private static NavigationState Left(NavigationState state, int arg)
        {
            var newDirection = state.direction - arg;
            return state with {direction = newDirection < 0 ? 360 + newDirection : newDirection};
        }

        private static NavigationState North(NavigationState state, int arg) => state with {y = state.y + arg};
        
        private static NavState North2(NavState state, int arg) => 
            state with { waypoint = state.waypoint with {y = state.waypoint.y + arg}};

        private static NavigationState South(NavigationState state, int arg) => state with {y = state.y - arg};
        
        private static NavState South2(NavState state, int arg) => 
            state with { waypoint = state.waypoint with {y = state.waypoint.y - arg}};

        private static NavigationState East(NavigationState state, int arg) => state with {x = state.x - arg};
        
        private static NavState East2(NavState state, int arg) => 
            state with { waypoint = state.waypoint with {x = state.waypoint.x - arg}};

        private static NavigationState West(NavigationState state, int arg) => state with {x = state.x + arg};
        
        private static NavState West2(NavState state, int arg) => 
            state with { waypoint = state.waypoint with {x = state.waypoint.x + arg}};

    }

    record NavigationState(int direction, int x, int y);

    record Waypoint (int x, int y);

    record NavState(Waypoint waypoint, Waypoint ship);
}