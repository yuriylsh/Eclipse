using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Solutions
{
    public class Utils
    {
        public static IEnumerable<string> LoadInput(string name)
        {
            var path = Path.Combine("data", name);
            return File.ReadLines(path);
        }

        public static IEnumerable<IReadOnlyCollection<string>> GetGrouppedLines(IEnumerable<string> input)
        {
            var currentGroup = new List<string>();
            foreach (var line in input)
            {
                if (string.IsNullOrEmpty(line))
                {
                    yield return currentGroup;
                    currentGroup = new List<string>();
                }
                else
                {
                    currentGroup.Add(line);
                }
            }

            // assuming the input does not have an empty line at the end
            // otherwise this would be an extra empty group
            yield return currentGroup;
        }
    }
}