using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FluentAssertions;
using Xunit;

namespace Solutions
{
    public class Day07
    {
        [Fact]
        public void Part1_SampleData_FindsCorrectColors()
        {
            var input = Utils.LoadInput("Day07_Part1_SampleInput.txt");

            var result = Day07Solution.GetContainingColors("shiny gold", input);

            result.Should().BeEquivalentTo("bright white", "light red", "dark orange", "muted yellow");
        }

        [Fact]
        public void Part1_InputData_FindsCorrectColors()
        {
            var input = Utils.LoadInput("Day07_Part1_Input.txt");

            var result = Day07Solution.GetContainingColors("shiny gold", input);

            result.Should().HaveCount(139);
        }
        
        [Fact]
        public void Part2_SampleData_FindsCorrectNumber()
        {
            var input = Utils.LoadInput("Day07_Part2_SampleInput.txt");

            var result = Day07Solution.CountContainingColors("shiny gold", input);

            result.Should().Be(126);
        }
        
        
        [Fact]
        public void Part2_InputData_FindsCorrectNumber()
        {
            var input = Utils.LoadInput("Day07_Part1_Input.txt");

            var result = Day07Solution.CountContainingColors("shiny gold", input);

            result.Should().Be(58175);
        }
    }

    public class Day07Solution
    {
        private static readonly Regex NestedColorsRegex = new Regex(@"\d+\s(\w+\s\w+)", RegexOptions.Compiled);
        private static readonly Regex NestedColorsWithNumberRegex = new Regex(@"(\d+)\s(\w+\s\w+)", RegexOptions.Compiled);
        public static HashSet<string> GetContainingColors(string color, IEnumerable<string> input)
        {
            var colors = new Dictionary<string, HashSet<string>>(input.Select(Parse));
            var result = new HashSet<string>();
            FindColors(color, colors, result);
            return result;
        }
        
        public static int CountContainingColors(string color, IEnumerable<string> input)
        {
            var colors = new Dictionary<string, HashSet<NestedColor>>(input.Select(ParseWithNumbers));
            return CountColors(color, colors);
        }

        private static KeyValuePair<string, HashSet<string>> Parse(string input)
        {
            var parts =  input.Split(" bags contain ", 2, StringSplitOptions.None);	
            return new KeyValuePair<string, HashSet<string>>(parts[0], new HashSet<string>(NestedColorsRegex.Matches(parts[1]).Select(x => x.Groups[1].Value)));
        }
        
        private static KeyValuePair<string, HashSet<NestedColor>> ParseWithNumbers(string input)
        {
            var parts = input.Split(" bags contain ", 2, StringSplitOptions.None);
            return new KeyValuePair<string, HashSet<NestedColor>>(parts[0], new HashSet<NestedColor>(NestedColorsWithNumberRegex.Matches(parts[1]).Select(ToColor)));
            static NestedColor ToColor(Match x) => new(x.Groups[2].Value, int.Parse(x.Groups[1].Value));
        }
        
        private static void FindColors(string color, Dictionary<string, HashSet<string>> allColors, HashSet<string> result)
        {
            foreach (var (main, nested) in allColors)
            {
                if(nested.Contains(color) && !result.Contains(main)){
                    result.Add(main);			
                    FindColors(main, allColors, result);
                }
            }
        }
        
        private static int CountColors(string color, Dictionary<string, HashSet<NestedColor>> colors)
        {
            return CountColorsRecursive(new NestedColor(color, 1)) - 1;

            int CountColorsRecursive(NestedColor target)
            {
                var nested = colors[target.Name];
                int total = target.Count;
                return nested.Count == 0
                    ? total
                    : total += nested.Sum(nestedColor => target.Count * CountColorsRecursive(nestedColor));
            }
        }
    }
    
    public record NestedColor(string Name, int Count);
}