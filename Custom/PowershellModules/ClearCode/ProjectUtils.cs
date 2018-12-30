using System;
using System.IO;
using System.Linq;

namespace ClearCode
{
    public class ProjectUtils
    {
        private readonly Action<string> _logError;

        public ProjectUtils(Action<string> logError)
        {
            _logError = logError;
        }

        public string GetProjectFile(string directory)
        {
            var foundProjects = Directory.GetFiles(directory, "*.csproj", SearchOption.TopDirectoryOnly);

            switch (foundProjects.Length)
            {
                case 1:
                    return foundProjects[0];
                case 0:
                    _logError("Expected one .csproj file in the current directory. Found none.");
                    break;
                default:
                    var multipleFiles = string.Join(", ", foundProjects.Select(x => "'" + x + "'"));
                    _logError(
                        $"Expected only one .csproj file in the current directory. Found {foundProjects.Length} project files instead: {multipleFiles}");
                    break;
            }
            
            return null;
        }
    }
}
