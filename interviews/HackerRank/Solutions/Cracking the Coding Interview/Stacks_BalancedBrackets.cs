using System;
using System.Collections.Generic;

namespace Solutions.Cracking_the_Coding_Interview
{
    public static class Brackets
    {
        private static readonly HashSet<char> OpeningBrackets = new HashSet<char>();

        static Brackets()
        {
            OpeningBrackets.Add('(');
            OpeningBrackets.Add('[');
            OpeningBrackets.Add('{');
        }


        public static bool IsBalanced(string brackets)
        {
            if (IsOddNumberOfBrackets(brackets)) return false;
            return IsBalancedEven(brackets);
        }

        private static bool IsOddNumberOfBrackets(string brackets) => brackets.Length % 2 != 0;

        private static bool IsBalancedEven(string brackets)
        {
            int halfBracketsCount = brackets.Length / 2;
            var openBrackets = new Stack<char>(halfBracketsCount);
            foreach (char bracket in brackets)
            {
                var isBalanced = OpeningBrackets.Contains(bracket) 
                    ? ProcessOpeningBracket(bracket, openBrackets, halfBracketsCount) 
                    : ProcessClosingBracket(bracket, openBrackets);

                if (!isBalanced) return false;
            }

            return openBrackets.Count == 0;
        }

        private static bool ProcessOpeningBracket(char openBracket, Stack<char> openBrackets, int halfBracketsCount)
        {
            openBrackets.Push(openBracket);
            return openBrackets.Count <= halfBracketsCount;
        }

        private static bool ProcessClosingBracket(char closeBracket, Stack<char> openBrackets)
        {
            if (openBrackets.Count == 0) return false;
            var lastOpenBracket = openBrackets.Pop();
            return DoesOpenBracketMatchClosingBracket(lastOpenBracket, closeBracket);
        }

        private static bool DoesOpenBracketMatchClosingBracket(char openBracket, char closeBracket)
        {
            switch (openBracket)
            {
                case '(':
                    return closeBracket == ')';
                case '[':
                    return closeBracket == ']';
                case '{':
                    return closeBracket == '}';
                default:
                    throw new ArgumentException(
                        "The following constraint was violated: Each character in the sequence will be a bracket (i.e., {, }, (, ), [, and ]).");
            }
        }
    }
}