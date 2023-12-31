﻿using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Solutions
{
    public class Day13
    {
        [Fact]
        public void Part1_SampleInput_CorrectBus()
        {
            var timestamp = "939";
            var schedule = "7,13,x,x,59,x,31,19";

            var departure = long.Parse(timestamp);
            var (bus, time) = Day13Solution.GetBus(departure, schedule);
            bus.Should().Be(59);
            time.Should().Be(944);
            ((time - departure) * bus).Should().Be(295L);
        }
        
        
        [Fact]
        public void Part1_Input_CorrectBus()
        {
            var timestamp = "1002462";
            var schedule = "37,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,41,x,x,x,x,x,x,x,x,x,601,x,x,x,x,x,x,x,x,x,x,x,19,x,x,x,x,17,x,x,x,x,x,23,x,x,x,x,x,29,x,443,x,x,x,x,x,x,x,x,x,x,x,x,13";

            var departure = long.Parse(timestamp);
            var (bus, time) = Day13Solution.GetBus(departure, schedule);
            bus.Should().Be(601);
            time.Should().Be(1002468L);
            ((time - departure) * bus).Should().Be(3606L);
        }
        
        [Theory]
        [InlineData("17,x,13,19", 3417L)]
        [InlineData("67,7,59,61", 754018L)]
        [InlineData("67,x,7,59,61", 779210L)]
        [InlineData("67,7,x,59,61", 1261476L)]
        [InlineData("1789,37,47,1889", 1202161486L)]
        public void Part2_SampleInput_CorrectTimestamp(string schedule, long expectedTimestamp)
        {
            var timestamp = Day13Solution.GetTimestamp(schedule);
            timestamp.Should().Be(expectedTimestamp);
        }
        
        [Fact]
        public void Part2_Input_CorrectTimestamp()
        {
            var schedule = "37,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,41,x,x,x,x,x,x,x,x,x,601,x,x,x,x,x,x,x,x,x,x,x,19,x,x,x,x,17,x,x,x,x,x,23,x,x,x,x,x,29,x,443,x,x,x,x,x,x,x,x,x,x,x,x,13";

            var timestamp = Day13Solution.GetTimestamp(schedule);
            timestamp.Should().Be(379786358533423L);
        }
    }

    public class Day13Solution
    {
        public static (int busNumber, long departure) GetBus(long departure, string schedule)
        {
            var busses = schedule.Split(',').Where(x => x != "x").Select(int.Parse).ToDictionary(x => x, _ => -1L);
            foreach (var bus in busses.Keys)
            {
                if (departure % bus == 0) return (bus, departure);
                busses[bus] = departure / bus * bus  + bus;
            }

            KeyValuePair<int, long> min = new KeyValuePair<int, long>(-1, long.MaxValue);
            foreach (var bus in busses)
            {
                if (min.Value > bus.Value) min = bus;
            }

            return (min.Key, min.Value);
        }

        public static long GetTimestamp(string schedule)
        {
            var busses = GetBusses(schedule);
            var N = busses.Aggregate(1L, (acc, x) => acc * x.number);

            var data = busses.Select(GetChineseRemainderTheoremData).ToArray();
            var sum = data.Select(x => x.remainder * x.inverse * x.Ni).Sum();
            return sum % N;

            (int remainder, long Ni, int inverse) GetChineseRemainderTheoremData((int number, int offset) bus)
            {
                var Ni = N / bus.number;
                return (bus.number - bus.offset, Ni, GetInverse(Ni, bus.number));
            }

            static int GetInverse(long Ni, int modulo)
            {
                var multiple = Ni % modulo;
                var inverse = 1;
                while (true)
                {
                    if (inverse * multiple % modulo == 1) return inverse;
                    inverse += 1;
                }
            }

            return 0;
            
        }
        
        

        private static List<(int number, int offset)> GetBusses(string schedule)
        {
            var split = schedule.Split(',');
            List<(int number, int offset)> busses = new();
            for (var i = 0; i < split.Length; i++)
            {
                var current = split[i];
                if (current != "x")
                {
                    busses.Add((int.Parse(current), i));
                }
            }

            return busses;
        }
    }
}