using FluentAssertions;
using Solutions.Cracking_the_Coding_Interview;
using Xunit;

namespace Tests
{
    public class Strings_MakingAnagramsTests
    {
        [Theory]
        [InlineData("cde", "abc", 4)]
        [InlineData("a", "a", 0)]
        public void CountDifferences_GivenTestSamples_ReturnsCorrectCount(string a, string b, int correctCount)
        {
            int differences = Strings_MakingAnagrams.CountDifferences(a, b);

            differences.Should().Be(correctCount);
        }
    }

    public class StringCharactersTests
    {
        [Fact]
        public void UniqueCharacters_GivenAAA_ReturnsUniqueCharactersA()
        {
            var subject = new StringCharacters("aaa");

            var uniqueCharacters = subject.UniqueCharacters;

            uniqueCharacters.Should().Equal('a');
        }

        [Fact]
        public void UniqueCharacters_GivenABA_ReturnsUniqueCharactersAB()
        {
            var subject = new StringCharacters("aba");

            var uniqueCharacters = subject.UniqueCharacters;

            uniqueCharacters.Should().Equal('a', 'b');
        }

        [Theory]
        [InlineData('a', 2)]
        [InlineData('b', 1)]
        [InlineData('c', 0)]
        public void GetCount_GivenABAThenAskedForCount_ReturnsCorrectCount(char countOf, int expectedCount)
        {
            var subject = new StringCharacters("aba");

            var count = subject.GetCount(countOf);

            count.Should().Be(expectedCount);
        }
    }
}