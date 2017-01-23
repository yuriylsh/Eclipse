using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Solution
{
    class Solution
    {
        static void Main(string[] args)
        {
            var cities = CityDataParser.Parse(ReadInput());
            var citiesByDegree = new CitiesByDegree(cities, GetChicago(cities));
            foreach (var line in citiesByDegree)
            {
                Console.WriteLine(ProduceOutputLine(line));
            }
        }

        private static CityData GetChicago(CityData[] cities)
        {
            return cities.First(city =>
                city.City.Equals("Chicago", StringComparison.OrdinalIgnoreCase)
                && city.State.Equals("Illinois", StringComparison.OrdinalIgnoreCase)
            );
        }

        private static IEnumerable<string> ReadInput() => System.IO.File.ReadAllLines("input001.txt");

        //public static IEnumerable<string> ReadInput()
        //{
        //    string inputLine;
        //    while ((inputLine = Console.ReadLine()) != null)
        //    {
        //        yield return inputLine;
        //    }
        //}

        private static string ProduceOutputLine(CityDegree cityDegree) 
            => $"{cityDegree.Degree} {cityDegree.CityData.City}, {cityDegree.CityData.State}";
    }

    public class CityDataParser
    {
        private const char CommentLineMarker = '#';
        private const char DataPartsDelimiter = '|';
        private const char InterstatesDelimiter = ';';
        private const int InterstatePrefixLength = 2; // "I-"

        // Aways verify input in the real world.
        // In the code chalenge I was told to always assume the input being well-formed,
        // thus I am skipping the input validation.
        public static CityData[] Parse(IEnumerable<string> lines) => lines
            .Where(line => !IsComment(line))
            .Select(LineToCityData)
            .ToArray();

        private static bool IsComment(string line) => line[0] == CommentLineMarker;

        private static CityData LineToCityData(string line)
        {
            var dataParts = line.Split(DataPartsDelimiter);
            return new CityData
            {
                City = dataParts[1],
                State = dataParts[2],
                Interstates = ParseInterstates(dataParts[3])
            };
        }

        private static int[] ParseInterstates(string intersates) => intersates
            .Split(InterstatesDelimiter)
            .Select(InterstateNameToNumber)
            .ToArray();

        private static int InterstateNameToNumber(string interstateName) 
            => int.Parse(interstateName.Substring(InterstatePrefixLength));
    }

    public class CityData
    {
        public string City { get; set; }

        public string State { get; set; }

        public int[] Interstates { get; set; }
    }

    public class CitiesByDegree: IEnumerable<CityDegree>
    {
        private const int NoConnectionDegree = -1;
        private const int RootDegree = 0;
        private static readonly IComparer<CityData> SameDegreeComparer = new CityAscThenStateAscComparer();
        private readonly List<CityDegree> _calculatedDegrees = new List<CityDegree>();

        public CitiesByDegree(CityData[] cities, CityData root)
        {
            var rootDegree = new CityDegree { Degree = RootDegree, CityData = root };
            _calculatedDegrees.Add(rootDegree);
            CalculateDegrees(ExcludeAlreadyCalculatedCities(cities).ToList(), RootDegree);
            _calculatedDegrees = SortByDegreeThenCity(_calculatedDegrees, SameDegreeComparer).ToList();
        }

        private void CalculateDegrees(List<CityData> cities, int parentDegree)
        {
            if (cities.Count == 0) return;

            var currentDegree = parentDegree + 1;
            var currentDegreeCities = new List<CityDegree>();
            foreach (var city in cities)
            {
                var calculatedConnectedToCurrent = GetMinDegreeCalculatedConnectedTo(city);
                if (calculatedConnectedToCurrent != null)
                {
                    currentDegreeCities.Add(new CityDegree { Degree = currentDegree, CityData = city});
                }
            }

            _calculatedDegrees.AddRange(currentDegreeCities);

            var noConnectionsFound = currentDegreeCities.Count == 0;
            if (noConnectionsFound)
            {
                var notConnectedToRoot =
                    cities.Select(city => new CityDegree {Degree = NoConnectionDegree, CityData = city});
                _calculatedDegrees.AddRange(notConnectedToRoot);
                return;
            }

            CalculateDegrees(ExcludeAlreadyCalculatedCities(cities).ToList(), currentDegree);
        }

        private IEnumerable<CityData> ExcludeAlreadyCalculatedCities(IEnumerable<CityData> cities)
        {
            foreach (var city in cities)
            {
                bool alreadyCaluculatedContainTheCity =
                    _calculatedDegrees.Any(calculated => SameDegreeComparer.Compare(calculated.CityData, city) == 0);
                if (!alreadyCaluculatedContainTheCity)
                {
                    yield return city;
                }
            }
        }

        private CityDegree GetMinDegreeCalculatedConnectedTo(CityData city)
        {
            var connections = _calculatedDegrees
                .Where(calculated => CitiesHaveConnection(calculated.CityData, city));
            return SortByDegreeThenCity(connections, SameDegreeComparer).FirstOrDefault();
        }

        private bool CitiesHaveConnection(CityData cityA, CityData cityB) 
            => cityA.Interstates.Intersect(cityB.Interstates).Any();


        private static IEnumerable<CityDegree> SortByDegreeThenCity(IEnumerable<CityDegree> cities, IComparer<CityData> sameDegreeComparer) => cities.
            OrderByDescending(cityDegree => cityDegree.Degree)
            .ThenBy(cityDegree => cityDegree.CityData, sameDegreeComparer);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<CityDegree> GetEnumerator() => _calculatedDegrees.GetEnumerator();
    }

    public class CityDegree
    {
        public int Degree { get; set; }

        public CityData CityData { get; set; }
    }

    class CityAscThenStateAscComparer: IComparer<CityData>
    {
        public int Compare(CityData x, CityData y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (ReferenceEquals(null, y)) return 1;
            if (ReferenceEquals(null, x)) return -1;
            var cityComparison = string.Compare(x.City, y.City, StringComparison.Ordinal);
            if (cityComparison != 0) return cityComparison;
            return string.Compare(x.State, y.State, StringComparison.Ordinal);
        }
    }
}