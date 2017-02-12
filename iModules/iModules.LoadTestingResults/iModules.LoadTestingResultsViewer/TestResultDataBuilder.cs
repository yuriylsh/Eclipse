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
            var counters = await BuildCountersData(ids, names, idToLoadTestRunIdMap, repository);
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

        private static async Task<ComparisonGrid> BuildCountersData(Guid[] ids, string[] names, IDictionary<Guid, int> idToLoadTestRunIdMap,
            LoadTestRepository repository)
        {
            IEnumerable<LoadTestCounter>[] testCounters = await GetCountersAsync(ids, idToLoadTestRunIdMap, repository);

            var headers = new List<ComparisonGridHeader>(names.Select(name => new ComparisonGridHeader {Header = name}));
            headers.Insert(0, new ComparisonGridHeader{Header = "Counter"});

            var seedCounters = testCounters[0];
            var rows = new Dictionary<string, List<ComparisonGridCell>>();
            foreach (var counter in seedCounters)
            {
                rows[counter.GetFullCounterName()] = new List<ComparisonGridCell>(ids.Length);
            }

            for (int i = 0; i < ids.Length; i++)
            {
                bool isBaseline = i == 0;
                foreach (var counter in testCounters[i])
                {
                    if(rows.TryGetValue(counter.GetFullCounterName(), out List<ComparisonGridCell> cells))
                    {
                        if (isBaseline)
                        {
                            cells.Add(new ComparisonGridCell {Value = counter.CumulativeValue.ToString("0.####")});
                        }
                        else
                        {
                            cells.Add(GetNonBaselineCounterValue(counter, testCounters[0].First(baseline => baseline.GetFullCounterName() == counter.GetFullCounterName())));
                        }
                    }
                }
            }

            foreach (var row in rows)
            {
                row.Value.Insert(0, new ComparisonGridCell {Value = row.Key});
            }

            var result = new ComparisonGrid
            {
                Headers = headers,
                Rows = seedCounters
                        .Select(counter => counter.GetFullCounterName())
                        .Select(counterName => new ComparisonGridRow { Cells = rows[counterName]})
            };

            return result;
        }

        private static ComparisonGridCell GetNonBaselineCounterValue(LoadTestCounter counter, LoadTestCounter baseline)
        {
            var cell = new ComparisonGridCell {Value = counter.CumulativeValue.ToString("0.####")};
            bool valueIsGreaterThanBaseline = counter.CumulativeValue > baseline.CumulativeValue;
            if (counter.HigherIsBetter)
            {
                if (valueIsGreaterThanBaseline)
                    cell.IncreaseValue = GetAbsolutePercentageValue(counter.CumulativeValue, baseline.CumulativeValue);
                else
                    cell.DecreaseValue = GetAbsolutePercentageValue(counter.CumulativeValue, baseline.CumulativeValue);
            }
            else
            {
                if (valueIsGreaterThanBaseline)
                    cell.DecreaseValue = GetAbsolutePercentageValue(counter.CumulativeValue, baseline.CumulativeValue);
                else
                    cell.IncreaseValue = GetAbsolutePercentageValue(counter.CumulativeValue, baseline.CumulativeValue);
            }
            return cell;
        }

        private static string GetAbsolutePercentageValue(double x, double y)
        {
            if (Math.Abs(x - y) < 0.0000001) return null;
            var ratio = x / y;
            var percentage = ratio > 1 ? ratio - 1 : 1 - ratio;
            return percentage.ToString("P");
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