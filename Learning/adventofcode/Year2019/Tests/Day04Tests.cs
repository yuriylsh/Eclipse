using FluentAssertions;
using Solutions;
using Xunit;

namespace Tests
{
    public class Day04Tests
    {
        [Fact]
        public void MeetsCriteria_111111_ReturnsTrue()
        {
            Day04.MeetsCirteria(111111).Should().BeTrue();
        }
        
        [Fact]
        public void MeetsCriteria_223450_ReturnsFalse()
        {
            Day04.MeetsCirteria(223450).Should().BeFalse();
        }
    }
}