using FluentAssertions;
using Solutions;
using Xunit;

namespace Tests
{
    public class Day04Tests
    {
        [Fact]
        public void Part1_GivenRange_ReturnsCorrectCount()
        {
            Day04.Part1(387638, 919123).Should().Be(2);
        }
        
        [Fact]
        public void MeetsCriteria_111111_ReturnsTrue() => Day04.MeetsCriteria(111111).Should().BeTrue();

        [Fact]
        public void MeetsCriteria_223450_ReturnsFalseDecreases() => Day04.MeetsCriteria(223450).Should().BeFalse();

        [Fact]
        public void MeetsCriteria_123789_ReturnsFalseNoDouble() => Day04.MeetsCriteria(123789).Should().BeFalse();
    }
}