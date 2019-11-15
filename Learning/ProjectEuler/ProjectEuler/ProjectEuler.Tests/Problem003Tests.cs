using System.Linq;
using FluentAssertions;
using ProjectEuler.Solutions;
using Xunit;

namespace ProjectEuler.Tests
{
    public class Problem003Tests
    {
        [Fact]
        public void MaxPrimeMultiple_ProblemData_ReturnsMaxPrimeMultiple()
        {
            var result = Problem003.MaxPrimeMultiple(600_851_475_143);

            result.Should().Be(6857);
        }
        
        [Fact]
        public void GetPrimes_ReturnsPrimesUpToGivenNumber()
        {
            var result = Problem003.GetPrimes(30);

            result.Should().Equal(2, 3, 5, 7, 11, 13, 17, 19, 23, 29);
        }

        [Fact]
        public void GetPrimeMultiples_DescriptionData_ReturnsPrimeMultiples()
        {
            var result = Problem003.GetPrimeMultiples(13_195);

            result.Should().Equal(5, 7, 13, 29);
        }
        
        [Fact]
        public void GetPrimeMultiples_ProblemData_ReturnsPrimeMultiples()
        {
            var result = Problem003.GetPrimeMultiples(600_851_475_143);

            result.Should().Equal(71, 839, 1471, 6857);
        }
    }
}