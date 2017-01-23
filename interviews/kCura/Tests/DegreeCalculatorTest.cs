using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Solution;

namespace Tests
{
    [TestFixture]
    class DegreeCalculatorTest
    {
        private static readonly CityData Chicago = new CityData
        {
            City = "Chicago",
            State = "Illinois",
            Interstates = new[] {94, 90, 88, 57, 55}
        };

        [Test]
        public void DegreeCalculator_GivenTestInput_ProducesTestOutput()
        {
            var cities = new[]
            {
                new CityData
                {
                    City = "Seattle",
                    State = "Washington",
                    Interstates = new[] {5, 90}
                },
                new CityData
                {
                    City = "Chicago",
                    State = "Illinois",
                    Interstates = new[] {94, 90, 88, 57, 55}
                },
                new CityData
                {
                    City = "San Jose",
                    State = "California",
                    Interstates = new[] {5, 80}
                }
            };

            var subject = new CitiesByDegree(cities, Chicago);
            var result = subject.Select(ProduceOutputLine).ToArray();

            result.Should().Equal(
                "2 San Jose, California", 
                "1 Seattle, Washington", 
                "0 Chicago, Illinois");
        }

        [Test]
        public void DegreeCalculator_GivenOnlyRoot_ProducesCorrectOutput()
        {
            var cities = new[]
            {
                new CityData
                {
                    City = "Chicago",
                    State = "Illinois",
                    Interstates = new[] {94, 90, 88, 57, 55}
                }
            };

            var subject = new CitiesByDegree(cities, Chicago);
            var result = subject.Select(ProduceOutputLine).ToArray();

            result.Should().Equal(
                "0 Chicago, Illinois");
        }

        [Test]
        public void DegreeCalculator_GivenCitiesWithTheSameDegree_ShouldBeProperlySorted()
        {
            var cities = new[]
            {

                new CityData
                {
                    City = "Chicago",
                    State = "Illinois",
                    Interstates = new[] {94, 90, 88, 57, 55}
                },
                new CityData
                {
                    City = "San Jose",
                    State = "Zalifornia",
                    Interstates = new[] {88}
                },
                new CityData
                {
                    City = "Seattle",
                    State = "Washington",
                    Interstates = new[] {88}
                },
                new CityData
                {
                    City = "Seattly",
                    State = "Washington",
                    Interstates = new[] {88}
                },
                new CityData
                {
                    City = "NotConnected1",
                    State = "A",
                    Interstates = new[] {int.MaxValue}
                },
                new CityData
                {
                    City = "NotConnected2",
                    State = "B",
                    Interstates = new[] {int.MaxValue}
                },
                new CityData
                {
                    City = "NotConnected2",
                    State = "C",
                    Interstates = new[] {int.MaxValue}
                }
            };

            var subject = new CitiesByDegree(cities, Chicago);
            var result = subject.Select(ProduceOutputLine).ToArray();

            result.Should().Equal(
                "1 San Jose, Zalifornia",
                "1 Seattle, Washington",
                "1 Seattly, Washington",
                "0 Chicago, Illinois",
                "-1 NotConnected1, A",
                "-1 NotConnected2, B",
                "-1 NotConnected2, C");
        }

        [Test]
        public void DegreeCalculator_GivenCityConnectedTo2CitiesWithDifferentDegrees_ShouldBeProperlySorted()
        {
            var cities = new[]
            {

                new CityData
                {
                    City = "Chicago",
                    State = "Illinois",
                    Interstates = new[] {94, 90, 88, 57, 55}
                },
                new CityData
                {
                    City = "San Jose",
                    State = "Zalifornia",
                    Interstates = new[] {88, 1234}
                },
                new CityData
                {
                    City = "Seattle",
                    State = "Washington",
                    Interstates = new[] {1234, 5678}
                },
                new CityData
                {
                    City = "ACity",
                    State = "AState",
                    Interstates = new[] {94, 1234}
                }
            };

            var subject = new CitiesByDegree(cities, Chicago);
            var result = subject.Select(ProduceOutputLine).ToArray();

            result.Should().Equal(
                "2 Seattle, Washington",
                "1 ACity, AState",
                "1 San Jose, Zalifornia",
                "0 Chicago, Illinois");
        }

        private static string ProduceOutputLine(CityDegree cityDegree)
        {
            return $"{cityDegree.Degree} {cityDegree.CityData.City}, {cityDegree.CityData.State}";
        }
    }
}
