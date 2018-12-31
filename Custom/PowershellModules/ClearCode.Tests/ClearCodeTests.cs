using System.IO;
using System.Reflection.PortableExecutable;
using FluentAssertions;
using Xunit;

namespace ClearCode.Tests
{
    public class ClearCodeTests
    {
        private readonly string _projectDirectory;
        private string _errorMessage;
        private readonly ProjectUtils _subject;

        public ClearCodeTests()
        {
            _projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            _subject = new ProjectUtils(error => _errorMessage = error);
        }

        [Fact]
        public void FindProjectFiles_OneProjectAtRoot_ReturnsPathToProjectFile()
        {
            var result = _subject.GetProjectFile(_projectDirectory);

            result.Should().Be(Path.Combine(_projectDirectory, "ClearCode.Tests.csproj"));
            _errorMessage.Should().BeNull();
        }

        [Fact]
        public void FindProjectFiles_MultipleProjectsAtRoot_LogsErrorReturnsNull()
        {
            var root = Path.Combine(_projectDirectory, "MultipleProjFiles");
            var expectedErrorMessage =
                $"Expected only one .csproj file in the '{root}' directory. Found 2 project files instead: '{Path.Combine(root, "project1.csproj")}', '{Path.Combine(root, "project2.csproj")}'";
            
            var result = _subject.GetProjectFile(root);

            result.Should().BeNull();
            _errorMessage.Should().Be(expectedErrorMessage);
        }

        [Fact]
        public void FindProjectFiles_NoProjectsAtRoot_LogsErrorReturnsNull()
        {
            var directory = Path.Combine(_projectDirectory, "NoProjectFiles");
            var result = _subject.GetProjectFile(directory);

            result.Should().BeNull();
            _errorMessage.Should().Be($"Expected one .csproj file in the '{directory}' directory. Found none.");
        }

        [Fact]
        public void FindEntriesToClear_ReturnsAllObjectsToClear()
        {
            var project = _subject.GetProjectFile(_projectDirectory);

            var (directories, files) = _subject.FindEntriesToClear(project);

            directories.Should().Equal(FullPath("bin"), FullPath("obj"), FullPath("ClientApp\\node_modules"));
            files.Should().Equal(FullPath("ClientApp\\package-lock.json"));
        }

        private string FullPath(string relative)
            => Path.Combine(_projectDirectory, relative);
    }
}
