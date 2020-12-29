using System.Collections.Generic;
using System.IO;

namespace Solutions
{
    public class Utils
    {
        public static IEnumerable<string> LoadInput(string name)
        {
            var path = Path.Combine("data", name);
            return File.ReadLines(path);
        }
    }
}