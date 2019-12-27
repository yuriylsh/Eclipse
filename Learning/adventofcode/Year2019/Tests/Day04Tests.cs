using FluentAssertions;
using Solutions;
using Xunit;

namespace Tests
{
    public class Day04Tests
    {
        [Fact]
        public void Part1_GivenRange_ReturnsCorrectCount() => Day04.Part1(387638, 919123).Should().Be(466);
        
        [Fact]
        public void Part2_GivenRange_ReturnsCorrectCount() => Day04.Part2(387638, 919123).Should().Be(292);

        [Fact]
        public void MeetsPart1Criteria_111111_ReturnsTrue() => Day04.MeetsPart1Criteria(111111).Should().BeTrue();

        [Fact]
        public void MeetsPart1Criteria_223450_ReturnsFalseDecreases() => Day04.MeetsPart1Criteria(223450).Should().BeFalse();

        [Fact]
        public void MeetsPart1Criteria_123789_ReturnsFalseNoDouble() => Day04.MeetsPart1Criteria(123789).Should().BeFalse();

        [Theory]
        [InlineData(112233, true)]
        [InlineData(123444, false)]
        [InlineData(111122, true)]
        [InlineData(223450, false)]
        [InlineData(123789, false)]
        public void MettsPart2Criteria_ReturnsCorrectAnswer(int number, bool expectedResult) =>
            Day04.MeetsPart2Criteria(number).Should().Be(expectedResult);

    }
}