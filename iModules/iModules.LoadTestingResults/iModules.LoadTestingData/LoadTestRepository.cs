using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Linq;

namespace iModules.LoadTestingData
{
    public class LoadTestRepository
    {
        private readonly string _connectionString;

        public LoadTestRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<LoadTestMetadata> GetTestMetadataAsync(Guid testRunId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(); try
                {
                    return await connection.QuerySingleAsync<LoadTestMetadata>(Sql.LoadTestRunMetadata, new { runId = testRunId });
                }
                catch (InvalidOperationException)
                {
                    return null;
                }
            }
        }

        public async Task<ITestCases> GetTestCasesAsync(int loadTestRunId)
        {
            var result = new TestCases();
            var sql = Sql.PassingFailingTests + ";" + Sql.TestsCaseTiming;
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@loadTestRunId", SqlDbType.Int).Value = loadTestRunId;
                await connection.OpenAsync();
                using (var resultsReader = await command.ExecuteReaderAsync())
                {
                    while (await resultsReader.ReadAsync())
                    {
                        var counterName = (string) resultsReader[0];
                        var scenarioName = (string)resultsReader[1];
                        var testCaseName = (string)resultsReader[2];
                        var count = (float) resultsReader[3];
                        result.AppendCounter(counterName, scenarioName, testCaseName, (int)count);
                    }
                    await resultsReader.NextResultAsync();
                    while (await resultsReader.ReadAsync())
                    {
                        var testCaseName = (string)resultsReader[0];
                        var runs = (int)resultsReader[1];
                        var minimum = (double)resultsReader[2];
                        var average = (double)resultsReader[3];
                        var percentile90 = (double)resultsReader[4];
                        var maximum = (double)resultsReader[5];
                        result.AppendTestCaseRuns(testCaseName, runs, minimum, average, percentile90, maximum);
                    }

                }
            }
            return result;
        }

        public async Task<IEnumerable<PageTiming>> GetPageTimingsAsync(int loadTestRunId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryAsync<PageTiming>(Sql.PageTimings, new { loadTestRunId = loadTestRunId });
            }
        }

        public async Task<IEnumerable<LoadTestCounter>> GetLoadTestCountersAsync(int loadTestRunId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryAsync<LoadTestCounter>(Sql.LoadTestCounters, new { loadTestRunId = loadTestRunId });
            }
        }

        public async Task<IEnumerable<TestRunMessage>> GetTestRunMessagesAsync(int loadTestRunId, string testCaseName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryAsync<TestRunMessage>(Sql.TestCaseErrors, new { loadTestRunId = loadTestRunId, testCaseName = testCaseName });
            }
        }

        public async Task<IDictionary<Guid, int>> GetLoadTestRunIdsAsync(IEnumerable<Guid> runIds)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var map =  await connection.QueryAsync<RunIdAndLoadTestRunIdPair>(Sql.RunIdToLoadTestRunId, new { runIds = runIds });
                return map.ToDictionary<RunIdAndLoadTestRunIdPair, Guid, int>(GetRunId, GetLoadTestRunId);
            }

            Guid GetRunId(RunIdAndLoadTestRunIdPair pair) => pair.RunId;
            int GetLoadTestRunId(RunIdAndLoadTestRunIdPair pair) => pair.LoadTestRunId;
        }
    }
}
;