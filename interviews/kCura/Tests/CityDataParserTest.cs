using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Solution;

namespace Tests
{
    [TestFixture]
    public class CityDataParserTest
    {
        [Test]
        public void CityDataParser_GivenCommentLine_SkiptCommentLine()
        {
            var parseResult = CityDataParser.Parse(new[] {"#this is a comment"});

            parseResult.Should().BeEmpty();
        }

        [Test]
        public void CityDataParser_GivenLineWithCityData_ReturnsParsedCityData()
        {
            var result = CityDataParser.Parse(new[] {"6|Seattle|Washington|I-5;I-90"});

            result.Should().HaveCount(1);
            var city = result.First();

            city.ShouldBeEquivalentTo(new CityData
            {
                City = "Seattle",
                State = "Washington",
                Interstates = new[] { 5, 90 }
            });
        }

        [Test]
        public void CityDataParser_TestInput_ReturnsParsedCityData()
        {
            var input = TestHelper.GetTestData();

            var result = CityDataParser.Parse(input);

            result.Should().HaveCount(3);
            result[0].ShouldBeEquivalentTo(new CityData
            {
                City = "Seattle",
                State = "Washington",
                Interstates = new[] {5, 90}
            });
            result[1].ShouldBeEquivalentTo(new CityData
            {
                City = "Chicago",
                State = "Illinois",
                Interstates = new[] {94, 90, 88, 57, 55}
            });
            result[2].ShouldBeEquivalentTo(new CityData
            {
                City = "San Jose",
                State = "California",
                Interstates = new[] {5, 80}
            });
        }
    }
}