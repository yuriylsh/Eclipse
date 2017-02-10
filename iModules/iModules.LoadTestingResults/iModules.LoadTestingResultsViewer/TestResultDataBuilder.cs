using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iModules.LoadTestingData;
using iModules.LoadTestingResultsViewer.ViewModels;

namespace iModules.LoadTestingResultsViewer
{
    public class TestResultDataBuilder
    {
        public static async Task<TestDataViewModel> BuildAsync(IEnumerable<Guid> ids, LoadTestRepository repository)
        {
            var idToLoadTestRunIdMap = await repository.GetLoadTestRunIdsAsync(ids);
            var loadTestRunIdToPageTimingsMap = await GetPageTimingsAsync(idToLoadTestRunIdMap.Values, repository);

            foreach (var runId in idToLoadTestRunIdMap.Keys)
            {
                
            }
            
            var chart = new ChartData
            {
                Columns = new[]
                {
                    new Column("string", "Page"),
                    new Column("number", "test Result 1"),
                    new Column("number", "test Result 2")
                },
                Rows = new[]
                {
                    new object[] {"EventCenter_GET", 7.23, 8.15},
                    new object[] {"EventCenter_POST", 12.6, 22.9},
                },
                Title = "EventRegistion",
                HAxixTitle = "Pages",
                VAxisTitle = "Time, seconds"
            };

            await Task.CompletedTask;
            return new TestDataViewModel{ Charts = new []{chart}};
        }

        private static async Task<Dictionary<int, IEnumerable<PageTiming>>> GetPageTimingsAsync(IEnumerable<int> loadTestRunIds, LoadTestRepository repository)
        {
            var tasks = loadTestRunIds.Select(async loadTestRunId =>
            {
                var pageTimings = await repository.GetPageTimingsAsync(loadTestRunId);
                return (LoadTestRunId: loadTestRunId, PageTimings: pageTimings);
            });
            var result = await Task.WhenAll(tasks);
            return result.ToDictionary(x => x.LoadTestRunId, x => x.PageTimings);
        }
    }
}