using System.Collections.Generic;

namespace iModules.LoadTestingData
{
    public interface ITestCases
    {
        int TotalFailed { get;}

        int TotalPassed { get;}

        IEnumerable<TestCase> Cases { get; }
    }
}