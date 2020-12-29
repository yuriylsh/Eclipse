using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;
using FluentAssertions;
using Xunit;

namespace Solutions
{
    public class Day04
    {
        [Fact]
        void Part01_SampleData_ParsesDataCorrectly()
        {
            var input = Utils.LoadInput("Day04_Part1_SampleInput.txt");
            var passports = Day04Solution.ParsePassportData(input).ToArray();

            passports.Should().HaveCount(4);

            passports.Should().Contain(new PassportData{
                EyeColor = "gry", PassportId = "860033327", ExpirationYear = "2020", HairColor = "#fffffd",
                BirthYear = "1937", IssueYear = "2017", CountryId = "147", Height = "183cm"});
        }
        
        
        [Fact]
        void Part01_SampleData_ValidatesDataCorrectly()
        {
            var input = Utils.LoadInput("Day04_Part1_SampleInput.txt");
            var passports = Day04Solution.ParsePassportData(input).ToArray();

            passports.Where(Day04Solution.IsValidPart1).Should().HaveCount(2);
        }
        
        
        [Fact]
        void Part01_InputData_ValidatesDataCorrectly()
        {
            var input = Utils.LoadInput("Day04_Part1_Input.txt");
            var passports = Day04Solution.ParsePassportData(input).ToArray();

            passports.Where(Day04Solution.IsValidPart1).Should().HaveCount(245);
        }
        
        
        [Fact]
        void Part02_SampleData_ValidatesDataCorrectly()
        {
            var input = Utils.LoadInput("Day04_Part2_SampleInput.txt");
            var passports = Day04Solution.ParsePassportData(input).ToArray();

            var valid = passports.Where(Day04Solution.IsValid).ToArray();
            valid.Should().HaveCount(4);
            valid.Select(x => x.PassportId).Should().Equal("087499704", "896056539", "545766238", "093154719");
        }

        [Fact]
        void Part02_SampleDataFromReddit_ValidatesDataCorrectly()
        {
            var input = Utils.LoadInput("Day04_Part2_RedditInput.txt");
            var passports = Day04Solution.ParsePassportData(input).ToArray();

            var test = passports.Where(x => !PassportDataValidator.IsHairColorValid(x.HairColor));
            
            var valid = passports.Where(Day04Solution.IsValid).ToArray();
            valid.Should().HaveCount(158);
        }
        
        [Fact]
        void Part02_InputData_ValidatesDataCorrectly()
        {
            var input = Utils.LoadInput("Day04_Part2_Input.txt");
            var passports = Day04Solution.ParsePassportData(input).ToArray();

            var valid = passports.Where(Day04Solution.IsValid).ToArray();
            valid.Should().HaveCount(133);
        }
    }

    public class Day04Solution
    {
        public static IEnumerable<PassportData> ParsePassportData(IEnumerable<string> input)
        {
            var currentPassport = new PassportData();
            foreach (var line in input)
            {
                if (string.IsNullOrEmpty(line))
                {
                    yield return currentPassport;
                    currentPassport = new PassportData();
                }
                else
                {
                    AppendData(currentPassport, line);
                }
            }

            // assuming the input does not have an empty line at the end
            // otherwise this would be an extra empty passport instance
            yield return currentPassport;
        }

        private static void AppendData(PassportData currentPassport, string line)
        {
            var lineData = Regex.Matches(line, @"(\S+):(\S+)").Select(x => (Key: x.Groups[1].Value, Value:x.Groups[2].Value));
            foreach (var (key, value) in lineData)
            {
                if (Handlers.TryGetValue(key, out var handler)) handler(currentPassport, value);
            }
        }

        private static Dictionary<string, Action<PassportData, string>> Handlers = new Dictionary<string, Action<PassportData, string>>
        {
            ["byr"] = (data, value) => data.BirthYear = value,
            ["iyr"] = (data, value) => data.IssueYear = value,
            ["eyr"] = (data, value) => data.ExpirationYear = value,
            ["hgt"] = (data, value) => data.Height = value,
            ["hcl"] = (data, value) => data.HairColor = value,
            ["ecl"] = (data, value) => data.EyeColor = value,
            ["pid"] = (data, value) => data.PassportId = value,
            ["cid"] = (data, value) => data.CountryId = value
        };

        public static bool IsValid(PassportData data)
            => PassportDataValidator.IsBirthYearValid (data.BirthYear)  &&
               PassportDataValidator.IsIssueYearValid (data.IssueYear)  &&
               PassportDataValidator.IsExpirationYearValid (data.ExpirationYear)  &&
               PassportDataValidator.IsHeightValid (data.Height)  &&
               PassportDataValidator.IsHairColorValid (data.HairColor)  &&
               PassportDataValidator.IsEyeColorValid (data.EyeColor)  &&
               PassportDataValidator.IsPassportIdValid (data.PassportId) &&
               PassportDataValidator.IsCountryIdValid (data.CountryId);
        
        public static bool IsValidPart1(PassportData data)
            => !string.IsNullOrEmpty(data.BirthYear)  &&
               !string.IsNullOrEmpty(data.IssueYear)  &&
               !string.IsNullOrEmpty(data.ExpirationYear)  &&
               !string.IsNullOrEmpty(data.Height)  &&
               !string.IsNullOrEmpty(data.HairColor)  &&
               !string.IsNullOrEmpty(data.EyeColor)  &&
               !string.IsNullOrEmpty(data.PassportId);
    }

    public static class PassportDataValidator
    {
        public static bool IsBirthYearValid(string value) => FourDigits(value, 1920, 2002);
        public static bool IsIssueYearValid(string value) => FourDigits(value, 2010, 2020);
        public static bool IsExpirationYearValid(string value) => FourDigits(value, 2020, 2030);
        public static bool IsHeightValid(string value)
        {
            return value?.Length > 3 && ParseHeight(value) is not null;

            static (int value, string unit)? ParseHeight(string input)
            {
                var match = Regex.Match(input, @"\b(\d{2,3})(cm|in)");
                if (!match.Success) return null;
                var (number, unit) = (int.Parse(match.Groups[1].Value), match.Groups[2].Value);
                return (number, unit) switch
                {
                    (>=150 and <= 193, "cm") or (>=59 and <=76, "in")  => (number, unit),
                    _ => null
                };
            }
        }

        public static bool IsHairColorValid(string value) => value?.Length == 7 && Regex.IsMatch(value, @"#([0-9]|[a-f]){6}");
        public static bool IsEyeColorValid(string value) => value is "amb" or "blu" or "brn" or "gry" or "grn" or "hzl" or "oth";
        public static bool IsPassportIdValid(string value) => value?.Length == 9 && value.All(char.IsDigit);
        public static bool IsCountryIdValid(string value) => true;

        private static bool FourDigits(string value, int min, int max) => value?.Length == 4 && int.TryParse(value, out var number) && number >= min && number <= max;
    }

    public class PassportData : IEquatable<PassportData>
    { 
        public string BirthYear {get; set;}
        public string IssueYear {get; set;}
        public string ExpirationYear {get; set;}
        public string Height {get; set;}
        public string HairColor {get; set;}
        public string EyeColor {get; set;}
        public string PassportId {get; set;}
        public string CountryId {get; set;}

        public bool Equals(PassportData other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return BirthYear == other.BirthYear && IssueYear == other.IssueYear && ExpirationYear == other.ExpirationYear && Height == other.Height && HairColor == other.HairColor && EyeColor == other.EyeColor && PassportId == other.PassportId && CountryId == other.CountryId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PassportData) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(BirthYear, IssueYear, ExpirationYear, Height, HairColor, EyeColor, PassportId, CountryId);
        }

        public static bool operator ==(PassportData left, PassportData right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PassportData left, PassportData right)
        {
            return !Equals(left, right);
        }
    }
}