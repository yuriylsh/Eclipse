using System;
using System.Linq;
using FluentAssertions;
using ProjectEuler.Solutions;
using Xunit;

namespace ProjectEuler.Tests
{
    public class Problem001Tests
    {
        [Fact]
        public void DescriptionData_CalculatesSumOfMultiplesOf3And5()
        {
            var descriptionData = Enumerable.Range(1, 9).ToArray();

            var result = Problem001.Solve(descriptionData);

            result.Should().Be(23);
        }
        
        [Fact]
        public void ProblemData_CalculatesSumOfMultiplesOf3And5()
        {
            var descriptionData = Enumerable.Range(1, 999).ToArray();

            var result = Problem001.Solve(descriptionData);

            result.Should().Be(233168);
        }
    }
}