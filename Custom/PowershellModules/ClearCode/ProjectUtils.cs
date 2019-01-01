using System;
using System.IO;
using System.Linq;

namespace ClearCode
{
    public class ProjectUtils
    {
        private readonly IClearCodeConfiguration _configuration;
        private readonly Action<string> _logError;

        public ProjectUtils(IClearCodeConfiguration configuration, Action<string> logError)
        {
            _configuration = configuration;
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
                    _logError($"Expected one .csproj file in the '{directory}' directory. Found none.");
                    break;
                default:
                    var multipleFiles = string.Join(", ", foundProjects.Select(x => "'" + x + "'"));
                    _logError(
                        $"Expected only one .csproj file in the '{directory}' directory. Found {foundProjects.Length} project files instead: {multipleFiles}");
                    break;
            }
            
            return null;
        }

        public (string[] directories, string[] files) FindEntriesToClear(string project)
        {
            var projectDir = new FileInfo(project).DirectoryName;
            return (FindDirectoriesToClear(projectDir), FindFilesToClear(projectDir));
        }

        private string[] FindDirectoriesToClear(string projectDir) =>
            _configuration.ToRemoveDirectories
                .Select(x => new DirectoryInfo(Path.Combine(projectDir, x)))
                .Where(x => x.Exists)
                .Select(x => x.FullName)
                .ToArray();

        private string[] FindFilesToClear(string projectDir) =>
            _configuration.ToRemoveFiles
                .Select(x => new FileInfo(Path.Combine(projectDir, x)))
                .Where(x => x.Exists)
                .Select(x => x.FullName)
                .ToArray();
    }
}
