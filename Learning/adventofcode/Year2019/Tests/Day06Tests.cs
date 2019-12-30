using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using Solutions;
using Solutions.Shared;
using Xunit;

namespace Tests
{
    public class Day06Tests
    {
        [Fact]
        public void Part1_Input_ReturnsCorrectOrbitsTotal()
        {
            var input = new FileInfo("Inputs/day06_part1_input.txt");

            var result = Day06.Part1(input);

            result.Should().Be(150150);
        }
        
        [Fact]
        public void Part2_Input_ReturnsCorrectJumps()
        {
            var input = new FileInfo("Inputs/day06_part1_input.txt");

            var result = Day06.Part2(input);

            result.Should().Be(352);
        }
        
        [Fact]
        public void Part2_Sample_ReturnsCorrectJumps()
        {
            var input = new FileInfo("Inputs/day06_part1_sample2.txt");

            var result = Day06.Part2(input);

            result.Should().Be(4);
        }


        [Fact]
        public void Part2_Input_ReturnsCorrectNumber()
        {
            var input = File.ReadAllLines("Inputs/day06_part1_input.txt");
            var centerOfMass = Tree.Build(input);
        }


        [Fact]
        public void TraverseDepthFirstPreorder_SumDepth_CorrectlyTraverses()
        {
            var input = File.ReadAllLines("Inputs/day06_part1_sample.txt");
            var root = Tree.Build(input);
            int totlaDepth = 0;
            
            Tree.DepthFirstPreorder(root, x => totlaDepth += x.Depth);

            totlaDepth.Should().Be(42);
        }
        
        [Fact]
        public void TraverseDepthFirstPreorder_CollectLabels_CorrectlyTraverses()
        {
            var input = File.ReadAllLines("Inputs/day06_part1_sample.txt");
            var root = Tree.Build(input);
            string result = "";
            
            Tree.DepthFirstPreorder(root, x => result += x.Label);

            result.Should().Be("COMBCDEFJKLIGH");
        }

        [Fact]
        public void BuildTree_Sample_BuildsCorrectTree()
        {
            var input = File.ReadAllLines("Inputs/day06_part1_sample.txt");

            var tree = Tree.Build(input);

            tree.Label.Should().Be("COM");
            tree.Depth.Should().Be(0);
            tree.Children.Select(x => x.Label).Should().Equal("B");
            tree.Children.Select(x => x.Depth).Should().Equal(1);
            tree.Children.First().Children.Select(x => x.Label).Should().Equal("C", "G");
            tree.Children.First().Children.Select(x => x.Depth).Should().Equal(2, 2);
        }

        [Fact]
        public void LowestCommonAncestor_Sample2Dadta_ReturnsCorrectNode()
        {
            var input = File.ReadAllLines("Inputs/day06_part1_sample2.txt");
            var root = Tree.Build(input);
            Node GetByLabel(string label) => Tree.GetByLabel(root, label);

            var result = Tree.LowestCommonAncestor(GetByLabel("YOU"), GetByLabel("I"));

            result.Label.Should().Be("D");
        }
    }
}