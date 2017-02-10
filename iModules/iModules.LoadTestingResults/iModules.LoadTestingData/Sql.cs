namespace iModules.LoadTestingData
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

        public static string PageTimings = @"
SELECT testcase.TestCaseName, 
       request.RequestUri, 
       instance.CumulativeValue	   
FROM       LoadTestRun AS run
INNER JOIN LoadTestPerformanceCounterCategory AS category 
    ON run.LoadTestRunId = category.LoadTestRunId
INNER JOIN LoadTestPerformanceCounter AS counter 
    ON category.LoadTestRunId = counter.LoadTestRunId
    AND category.CounterCategoryId = counter.CounterCategoryId
INNER JOIN LoadTestPerformanceCounterInstance AS instance 
    ON counter.CounterId = instance.CounterId
    AND counter.LoadTestRunId = instance.LoadTestRunId
LEFT JOIN WebLoadTestRequestMap AS request
    ON request.LoadTestRunId = instance.LoadTestRunId
    AND request.RequestId = instance.LoadTestItemId
LEFT JOIN LoadTestCase As testcase
    ON request.LoadTestRunId = testcase.LoadTestRunId
    AND request.TestCaseId = testcase.TestCaseId
LEFT JOIN LoadTestScenario As scenario
    ON testcase.LoadTestRunId = scenario.LoadTestRunId
    AND testcase.ScenarioId = scenario.ScenarioId
WHERE category.CategoryName = 'LoadTest:Page' and instance.CumulativeValue IS NOT NULL
	  AND run.LoadTestRunId = @loadTestRunId
	  AND scenario.ScenarioName IS NOT NULL
	  AND CounterName = 'Avg. Page Time'
ORDER BY TestCaseName, request.RequestId";

        public static string LoadTestCounters = @"
SELECT category.CategoryName, counter.CounterName, instance.CumulativeValue, counter.HigherIsBetter
FROM LoadTestRun as run
INNER JOIN LoadTestPerformanceCounterCategory AS category 
    ON run.LoadTestRunId = category.LoadTestRunId
INNER JOIN LoadTestPerformanceCounter AS counter 
    ON category.LoadTestRunId = counter.LoadTestRunId
    AND category.CounterCategoryId = counter.CounterCategoryId
INNER JOIN LoadTestPerformanceCounterInstance AS instance 
    ON counter.CounterId = instance.CounterId
    AND counter.LoadTestRunId = instance.LoadTestRunId
WHERE instance.cumulativeValue IS NOT NULL
	  AND category.LoadTestRunId = @loadTestRunId
	  AND instance.InstanceName = '_Total'";

        public static string RunIdToLoadTestRunId = @"
SELECT CAST([RunId] AS uniqueidentifier ) as [RunId], [LoadTestRunId]
  FROM [LoadTest2010].[dbo].[LoadTestRun]
 WHERE [RunId] IN @runIds";
    }
}
