using System;
using System.Collections.Generic;
using System.Linq;

namespace Modules.LoadTestingData
{
    internal class TestCases: ITestCases
    {
        private readonly List<TestCase> testCases = new List<TestCase>();

        public int TotalFailed { get; private set; }

        public int TotalPassed { get; private set; }

        public IEnumerable<TestCase> Cases => testCases;

        public void AppendCounter(string counterName, string scenarioName, string testCaseName, int count)
        {
            if (IsPassedCounter(counterName)) AppendPassedCounter(scenarioName, testCaseName, count);
            if (IsFailedCounter(counterName)) AppendFailedCounter(scenarioName, testCaseName, count);
        }

        private bool IsPassedCounter(string counterName) => string.Equals(counterName, "Passed Tests", StringComparison.OrdinalIgnoreCase);

        private void AppendPassedCounter(string scenarioName, string testCaseName, int count)
        {
            if (IsTotalScenario(scenarioName)) TotalPassed = count;
            else GetOrCreateTestCaseByName(testCaseName).Passed = count;
        }

        private bool IsFailedCounter(string counterName) => string.Equals(counterName, "Failed Tests", StringComparison.OrdinalIgnoreCase);

        private void AppendFailedCounter(string scenarioName, string testCaseName, int count)
        {
            if (IsTotalScenario(scenarioName)) TotalFailed = count;
            else GetOrCreateTestCaseByName(testCaseName).Failed = count;
        }

        private bool IsTotalScenario(string scenarioName) => string.Equals(scenarioName, "_Total", StringComparison.OrdinalIgnoreCase);

        public void AppendTestCaseRuns(string testCaseName, int runs, double minimum, double average, double percentile90, double maximum)
        {
            var testCase = GetOrCreateTestCaseByName(testCaseName);
            testCase.Runs = runs;
            testCase.Minimum = minimum;
            testCase.Avarage = average;
            testCase.Percentile90 = percentile90;
            testCase.Maximum = maximum;
        }

        private TestCase GetOrCreateTestCaseByName(string testCaseName)
        {
            var existing = testCases.FirstOrDefault(test => test.Name.Equals(testCaseName, StringComparison.OrdinalIgnoreCase));
            if (existing != null) return existing;

            var testCase = new TestCase {Name = testCaseName};
            testCases.Add(testCase);
            return testCase;
        }
    }
}