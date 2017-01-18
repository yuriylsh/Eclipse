using Xunit;
using Library;

namespace Tests
{

    public class FSharpInversionsCounterTests
    {
        [Fact]
        public void LibraryEchoWhenCalledReturnsPassedObjectAsString()
        {
            //var echoedObject = InversionsCounter.echo("this is a string to echo");
            Assert.Equal("this is a string to echo", "");
        }
    }
}