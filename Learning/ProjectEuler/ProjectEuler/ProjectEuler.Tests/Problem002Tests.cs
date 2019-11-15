using System.Linq;
using FluentAssertions;
using ProjectEuler.Solutions;
using Xunit;

namespace ProjectEuler.Tests
{
    public class Problem002Tests
    {
        [Fact]
        public void GenerateFibonacci_ProducesFiboancciNumbers()
        {
            var result = Problem002.GenerateFibonacci();

            result.Take(10).Should().Equal(1, 2, 3, 5, 8, 13, 21, 34, 55, 89);
        }

        [Fact]
        public void DescriptionData_ReturnsSumEvens()
        {
            var result = Problem002.Calculate(90);

            result.Should().Be(2 + 8 + 34);
        }

        [Fact]
        public void ProblemData_ReturnsSumEvens()
        {
            var result = Problem002.Calculate(4_000_000);

            result.Should().Be(4613732);
        }
    }
}