using System;
using FluentAssertions;
using Solutions.Cracking_the_Coding_Interview;
using Xunit;

namespace Tests
{
    public class Arrays_LeftRotationTests
    {
        [Theory]
        [InlineData(4, new[] { "5", "1", "2", "3", "4" })]
        [InlineData(3, new[] { "4", "5", "1", "2", "3" })]
        [InlineData(2, new[] { "3", "4", "5", "1", "2" })]
        [InlineData(1, new[] { "2", "3", "4", "5", "1" })]
        public void Rotate_SampleArrayDoesSampleRotations_DoesExpectedRotations(int swaps, string[] expected)
        {
            string[] input = { "1", "2", "3", "4", "5" };

            var result = Arrays_LeftRotation.Rotate(input, swaps);

            result.Should().Equal(expected);
        }

        [Theory]
        [InlineData(3, new[] { "4", "1", "2", "3" })]
        [InlineData(2, new[] { "3", "4", "1", "2" })]
        [InlineData(1, new[] { "2", "3", "4", "1" })]
        public void Rotate_EvenElementsCountArrayDoesSampleRotations_DoesExpectedRotations(int swaps, string[] expected)
        {
            string[] input = { "1", "2", "3", "4" };

            var result = Arrays_LeftRotation.Rotate(input, swaps);

            result.Should().Equal(expected);
        }

        [Fact]
        public void Rotate_SingleElementArrayDoesOneRotation_DoesExpectedRotation()
        {
            var input = new[] { "33" };

            var result = Arrays_LeftRotation.Rotate(input, 1);

            result.Should().Equal("33");
        }

        [Fact]
        public void Rotate_TestCase1_DoesExpectedRotations()
        {
            var input = new[] { "41", "73", "89", "7", "10", "1", "59", "58", "84", "77", "77", "97", "58", "1", "86", "58", "26", "10", "86", "51" };

            var result = Arrays_LeftRotation.Rotate(input, 10);

            result.Should().Equal("77", "97", "58", "1", "86", "58", "26", "10", "86", "51", "41", "73", "89", "7", "10", "1", "59", "58", "84", "77");
        }
    }
}
