using System.IO;
using System.Threading.Tasks;
using FluentAssertions;
using Solutions.Shared;
using Xunit;

namespace Tests
{
    public class Day08Tests
    {
        [Fact]
        public async Task Canvas_SampleData_LoadsCorrectly()
        {
            using var input = new MemoryStream();
            input.Write(new byte[]{1,2,3,4,5,6,7,8,9,0,1,2});
            input.Seek(0, SeekOrigin.Begin);

            var canvas = await Canvas.Load(input, 3, 2);

            canvas.Layers.Should().HaveCount(2);
        }
    }
}