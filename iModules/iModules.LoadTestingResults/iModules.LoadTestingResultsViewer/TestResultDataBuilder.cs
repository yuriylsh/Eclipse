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
        public static async Task<TestDataViewModel> BuildAsync(Guid[] ids, string[] names, LoadTestRepository repository)
        {
            var idToLoadTestRunIdMap = await repository.GetLoadTestRunIdsAsync(ids);
            var charts = await BuildCharts(ids, names, idToLoadTestRunIdMap, repository);
            var counters = await CountersComparisonGridBuilder.BuildCountersData(ids, names, idToLoadTestRunIdMap, repository);
            return new TestDataViewModel{ Charts = charts, Counters = counters};
        }

        private static async Task<IEnumerable<ChartData>> BuildCharts(Guid[] ids, string[] names, IDictionary<Guid, int> idToLoadTestRunIdMap, LoadTestRepository repository)
        {
            var loadTestRunIdToPageTimingsMap = await GetPageTimingsAsync(idToLoadTestRunIdMap.Values, repository);
            var chartBuilders = new List<ChartDataBuilder>();

            for (var i = 0; i < ids.Length; i++)
            {
                var runId = ids[i];
                var runName = names[i];
                var loadTestRunId = idToLoadTestRunIdMap[runId];
                var pageTimings = loadTestRunIdToPageTimingsMap[loadTestRunId];
                var pagesByTestCase = pageTimings.GroupBy(pageTiming => pageTiming.TestCaseName);
                foreach (var testCase in pagesByTestCase)
                {
                    var chartBuilder = GetChartBuilderForTestCase(chartBuilders, testCase.Key);
                    chartBuilder.AppendDataPoint(runName, testCase.ToArray());
                }
            }

            return chartBuilders.Select(builder => builder.Build());
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

        private static ChartDataBuilder GetChartBuilderForTestCase(ICollection<ChartDataBuilder> existingCharts, string testCaseName)
        {
            var existingChartBuilder = existingCharts.FirstOrDefault(chart => chart.IsForTestCase(testCaseName));
            return existingChartBuilder ?? CreateBuilderForTestCase(existingCharts, testCaseName);
        }

        private static ChartDataBuilder CreateBuilderForTestCase(ICollection<ChartDataBuilder> existingCharts, string testCaseName)
        {
            var newChartBuilder = new ChartDataBuilder(testCaseName);
            existingCharts.Add(newChartBuilder);
            return newChartBuilder;
        }

       
    }
}