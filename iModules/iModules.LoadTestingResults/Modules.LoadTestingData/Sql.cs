namespace Modules.LoadTestingData
{
    internal static class Sql
    {
        public static string ResultRepositoryGetResultsFormat = @"
  SELECT CAST(SUBSTRING(trx_results, 54, 36) AS [uniqueidentifier]) AS [Id], [date_added] AS [Date], [name] AS [Name]
    FROM [EncompassPerformanceResults].[dbo].[load_test_results]
ORDER BY [date_added] DESC OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY";

        public static string ResultRepositorySetName = @"UPDATE [EncompassPerformanceResults].[dbo].[load_test_results] SET [name] = @newName WHERE SUBSTRING([trx_results], 54, 36) = @id";

        public static string ResultRepositoryGetInitialResultsFormat
            => ResultRepositoryGetResultsFormat + "; SELECT COUNT(*) FROM[EncompassPerformanceResults].[dbo].[load_test_results]";

        public static string LoadTestRunMetadata = @"
SELECT [LoadTestRunId], [StartTime] AS [UtcStartTime], [EndTime] AS [UtcEndTime], ([RunDuration]/60) AS [DurationInMinutes]
  FROM [LoadTest2010].[dbo].[LoadTestRun]
 WHERE [RunId] = @runId";

        public static string PassingFailingTests = @"
SELECT [CounterName], [ScenarioName], [TestCaseName], [CumulativeValue]
  FROM [LoadTest2010].[dbo].[LoadTestTestCaseSummary]
 WHERE [LoadTestRunId] = @loadTestRunId
	   AND [CounterName] IN ('Passed Tests', 'Failed Tests')";

        public static string TestsCaseTiming = @"
SELECT [TestCaseName], [TestsRun], [Minimum], [Average], [Percentile90], [Maximum]
  FROM [LoadTest2010].[dbo].[LoadTestTestResults]
 WHERE [LoadTestRunId] = @loadTestRunId";

        public static string TestCaseErrors = @"
SELECT requestmap.RequestUri,
	   message.SubType,
	   message.MessageText,
	   message.StackTrace
  FROM LoadTestMessage as message
	   LEFT OUTER JOIN LoadTestCase AS testcase
	   				   ON message.LoadTestRunId = testcase.LoadTestRunId
	   				   AND message.TestCaseId = testcase.TestCaseId
	   LEFT OUTER JOIN WebLoadTestRequestMap AS requestmap
	   	               ON message.LoadTestRunId = requestmap.LoadTestRunId
	   				   AND message.RequestId = requestmap.RequestId
 WHERE message.LoadTestRunId = @loadTestRunId
	   AND testcase.TestCaseName = @testCaseName
ORDER BY RequestUri, SubType";
    }
}
