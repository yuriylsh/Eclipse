/// PP 1.4: In the programming language of your choice, write a program that parses a sentence and replaces each word with the following: 
/// first letter, number of distinct characters between first and last character, and last letter.  
/// For example, Smooth would become S3h.  
/// Words are separated by spaces or non-alphabetic characters and these separators should be maintained in their original form and location in the answer.
/// We are looking for accuracy, efficiency and solution completeness. 
/// Please include this problem description in the comment at the top of your solution.  The problem is designed to take approximately 1-2 hours.

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

/// Yuriy Lyeshchenko: Since there is no way to talk to anybody to clarify requiriments, I'm going to make some assumptions.
/// I will mark the palces where I had to do this assumptions with comments starting with "ASSUMPTION:". See right below for example.

// ASSUMPTION: "program" in the description above means class library.

namespace SentenceParsing
{
    public static class SentenceEncoder
    {
        /// <summary>
        /// Selects "words", which consist of only alphabetic characters (like [A-Za-z] in English).
        /// </summary>
        private static Regex WordRegex = new Regex(@"[^\W\d_]+", RegexOptions.Compiled | RegexOptions.Singleline);

        /// <summary>
        /// Parses a sentence and replaces each word with the following: first letter, number of distinct characters between first and last character, and last letter. 
        /// </summary>
        /// <param name="sentence">Any text input to be encoded.</param> // ASSUMPTION: We are fine with an input that is not a single sentence. For example, not a sententce at all or multiple sentences. And we are not considering right-to-left languages at all...
        /// <returns>The original sentence with each word in it being encoded. For example, Smooth would become S3h. Words are separated by spaces or non-alphabetic characters and these separators are maintained in their original form and location.</returns>
        public static string Encode(string sentence)
        {
            // see the assumption in the param description.
            if (string.IsNullOrEmpty(sentence))
            {
                return sentence;
            }

            return WordRegex.Replace(sentence, match => ProcessWord(match.Value));
        }

        private static string ProcessWord(string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                return word;
            }

            // ASSUMPTION: treat any one-letter word as a word with the first letter, 0 distinct characters in between and no last letter:
            // "A" becomes "A0"
            if (word.Length == 1)
            {
                return word + "0"; 
            }

            char firstLetter = word[0];
            char lastLetter = word[word.Length - 1];

            // ASSUMPTION: treat any two-letter word as a word with the first letter, 0 distinct characters in between and the last letter:
            // "AB" becomes "A0B"
            if (word.Length == 2)
            {
                return BuildEncodedWord(firstLetter, 0, lastLetter);
            }

            int distinctCount = GetDistinctCharCount(word, 1, word.Length - 2);
            return BuildEncodedWord(firstLetter, distinctCount, lastLetter);
        }

        private static string BuildEncodedWord(char firstLetter, int distinctCount, char lastLetter)
        {
            var builder = new StringBuilder();
            builder.Append(firstLetter);
            builder.Append(distinctCount.ToString("0"));
            builder.Append(lastLetter);
            return builder.ToString();
        }

        /// <summary>
        /// Given a string, counts number of distinct charactrs in the string. 
        /// The count starts and ends at specified character positions.
        /// </summary>
        /// <param name="input">A word to count. Must be at least 3 characters long.</param>
        /// <param name="start">The character position to start the count (inclusive).</param>
        /// <param name="end">The character position to end the count (inclusive).</param>
        /// <returns>Number of distinct charaters.</returns>
        private static int GetDistinctCharCount(string input, int start, int end)
        {
            if (input == null || input.Length < 3)
            {
                throw new ArgumentException("The input string must be at least 3 characters long."); // ASSUMPTION: no localization for error messages.
            }
            if (start < 0 || start > end || end >= input.Length)
            {
                throw new ArgumentException("Boundaries invariant is broken."); // ASSUMPTION: no localization for error messages.
            }

            var distinctCharactersSet = new HashSet<char>();
            for (int i = start; i <= end; i++)
            {
                distinctCharactersSet.Add(input[i]); // ASSUMPTION This is O(1) operation so it should meet the "efficiency" requirements.
            }

            return distinctCharactersSet.Count;
        }
    }
}
