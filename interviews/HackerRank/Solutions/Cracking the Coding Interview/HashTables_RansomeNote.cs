using System;
using System.Collections.Generic;
using System.Linq;

namespace Solutions.Cracking_the_Coding_Interview
{
    public class HashTables_RansomeNote
    {
        public static bool CanConstructNote(string[] magazine, string[] note)
        {
            var magazineWords = new Magazine(magazine);
            return note.All(noteWord => magazineWords.ConsumeWord(noteWord));
        }

        public class Magazine
        {
            private readonly Dictionary<int, int> _words;

            public Magazine(string[] words)
            {
                object dumb = new Object();
                _words = words.ToLookup(word => word.GetHashCode(), word => dumb)
                    .ToDictionary(lookup => lookup.Key, lookup => lookup.Count());
            }

            public bool ConsumeWord(string word)
            {
                var key = word.GetHashCode();
                int countInMagazine;
                if (_words.TryGetValue(key, out countInMagazine))
                {
                    if (countInMagazine == 0) return false;
                    _words[key] = countInMagazine - 1;
                    return true;
                }
                return false;
            }
        }
    }
}