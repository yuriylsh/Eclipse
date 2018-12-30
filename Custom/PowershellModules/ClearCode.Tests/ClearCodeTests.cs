using System.IO;
using FluentAssertions;
using Xunit;

namespace ClearCode.Tests
{
    public class ClearCodeTests
    {
        DirectoryInfo _projectDirectory;

        public ClearCodeTests()
        {
            _projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent;
        }

        [Fact]
        public void FindProjectFiles_OneProject_ReturnsPathToProjectFile()
        {
            //var subject = new ProjectUtils(_ => {});
            //subject.GetProjectFile(_projectDirectory).Should().Be(Path.Combine(_projectDirectory, "ClearCode.Tests.csproj"))

        }
    }
}
