using System.Collections.Generic;

namespace Modules.LoadTestingData
{
    public interface ITestCases
    {
        int TotalFailed { get;}

        int TotalPassed { get;}

        IEnumerable<TestCase> Cases { get; }

    }
}