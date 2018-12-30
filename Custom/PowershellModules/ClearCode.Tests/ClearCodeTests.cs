using System.IO;
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
                $"Expected only one .csproj file in the current directory. Found 2 project files instead: '{Path.Combine(root, "project1.csproj")}', '{Path.Combine(root, "project2.csproj")}'";
            
            var result = _subject.GetProjectFile(root);

            result.Should().BeNull();
            _errorMessage.Should().Be(expectedErrorMessage);
        }

        [Fact]
        public void FindProjectFiles_NoProjectsAtRoot_LogsErrorReturnsNull()
        {
            var result = _subject.GetProjectFile(Path.Combine(_projectDirectory, "NoProjectFiles"));

            result.Should().BeNull();
            _errorMessage.Should().Be("Expected one .csproj file in the current directory. Found none.");
        }
    }
}
