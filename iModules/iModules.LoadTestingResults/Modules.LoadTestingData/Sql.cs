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
    }
}
