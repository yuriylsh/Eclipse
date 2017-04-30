using System;
using System.Collections.Generic;
using System.Linq;

namespace Solutions.Cracking_the_Coding_Interview
{
    public class Strings_MakingAnagrams
    {
        public static int CountDifferences(string a, string b)
        {
            var aCount = new StringCharacters(a);
            var bCount = new StringCharacters(b);
            var allCharacters = new HashSet<char>(aCount.UniqueCharacters.Concat(bCount.UniqueCharacters));
            return allCharacters.Sum(c => Math.Abs(aCount.GetCount(c) - bCount.GetCount(c)));
        }
    }

    public class StringCharacters
    {
        private readonly IDictionary<char, int> _charactersCount;

        public StringCharacters(string s) => _charactersCount = s.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());

        public IEnumerable<char> UniqueCharacters => _charactersCount.Keys;

        public int GetCount(char c) => _charactersCount.TryGetValue(c, out int count) ? count: 0;
    }
}
