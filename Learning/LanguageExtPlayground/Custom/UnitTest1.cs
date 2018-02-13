using System;
using System.Collections.Generic;
using FluentAssertions;
using LanguageExt;
using Xunit;

namespace Custom
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Validation<string, bool> val1 = "this is an error for val1";
            Validation<string, int> val2 = "this is an error for val2";
            var final = (val1, val2).Apply((b, i) => 234).Bind<int>(x => 555);
            final.Match(x => throw new Exception($"shoudl not get {x}"), errors => ((IEnumerable<string>)errors).Should().Equal("this is an error for val1", "this is an error for val2"));
        }
    }
}
