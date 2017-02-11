using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iModules.LoadTestingData;
using iModules.LoadTestingResultsViewer.ViewModels;

namespace iModules.LoadTestingResultsViewer
{
    public partial class TestResultDataBuilder
    {
        public static async Task<TestDataViewModel> BuildAsync(Guid[] ids, string[] names, LoadTestRepository repository)
        {
            var idToLoadTestRunIdMap = await repository.GetLoadTestRunIdsAsync(ids);
            var charts = await BuildCharts(ids, names, idToLoadTestRunIdMap, repository);
            var counters = BuildCountersData(ids, names, idToLoadTestRunIdMap, repository);
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

        private static async Task<object> BuildCountersData(Guid[] ids, string[] names, IDictionary<Guid, int> idToLoadTestRunIdMap,
            LoadTestRepository repository)
        {
            IEnumerable<LoadTestCounter>[] testCounters = await GetCountersAsync(ids, idToLoadTestRunIdMap, repository);

            var headers = new List<ComparisonGridHeader>(names.Select(name => new ComparisonGridHeader {Header = name}));
            headers.Insert(0, new ComparisonGridHeader{Header = "Counter"});

            var seedCounters = testCounters[0];
            var rows = seedCounters.ToDictionary(counter => counter.CounterName, counter => new List<ComparisonGridCell>(ids.Length));

            var result = new
            {
                headers = new[] { new {header = "Counter"}}.Concat(names.Select(name => new { header = name})),
                rows2 = new []
                {
                    new
                    {
                        cells = new object[]
                        {
                            new ComparisonGridCell{Value = testCounters[0].},
                            new ComparisonGridCell{Value = "1"},
                            new ComparisonGridCell{Value = "1.2", DecreaseValue = "20"},
                        }
                    }
                }
                rows = new[]
                {
                    new
                    {
                        cells = new object[]
                        {
                            new ComparisonGridCell{Value = "Avg. Disk Queue Length"},
                            new ComparisonGridCell{Value = "1"},
                            new ComparisonGridCell{Value = "1.2", DecreaseValue = "20"},
                        }
                    },
                    new
                    {
                        cells = new object[]
                        {
                            new ComparisonGridCell{Value = "Avg. Transaction Time"},
                            new ComparisonGridCell{Value = "4"},
                            new ComparisonGridCell{Value = "2", IncreaseValue = "50"},
                        }
                    },
                }
            };

            return result;
        }

        private static async Task<IEnumerable<LoadTestCounter>[]> GetCountersAsync(Guid[] ids, IDictionary<Guid, int> idToLoadTestRunIdMap, LoadTestRepository repository)
        {
            var result = new IEnumerable<LoadTestCounter>[ids.Length];
            for (int i = 0; i < ids.Length; i++)
            {
                int loadTestRunId = idToLoadTestRunIdMap[ids[i]];
                result[i] = await repository.GetLoadTestCountersAsync(loadTestRunId);
            }
            return result;
        }
    }
}