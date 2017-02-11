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
            var loadTestRunIdToPageTimingsMap = await GetPageTimingsAsync(idToLoadTestRunIdMap.Values, repository);
            var chartBuilders = new List<ChartDataBuilder>();

            for(var i = 0; i < ids.Length; i++)
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
            
            return new TestDataViewModel{ Charts = chartBuilders.Select(builder => builder.Build())};
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

    internal class ChartDataBuilder
    {
        private readonly ChartData _chartData;
        private readonly List<Column> _columns = new List<Column>();
        private readonly Dictionary<string, List<object>> _rows = new Dictionary<string, List<object>>(StringComparer.OrdinalIgnoreCase);

        public ChartDataBuilder(string testCaseName)
        {
            _chartData = new ChartData
            {
                Title = testCaseName,
                HAxisTitle = DefaultHorizontalAxisTitle,
                VAxisTitle = DefaultVerticalAxisTitle
            };
            _columns.Add(new Column("string", "Page Uri"));
        }

        public ChartData Build()
        {
            _chartData.Columns = _columns;
            _chartData.Rows = BuildRows().ToArray();
            return _chartData;
        }

        private IEnumerable<object[]> BuildRows()
        {
            foreach (var row in _rows)
            {
                row.Value.Insert(0, row.Key);
                yield return row.Value.ToArray();
            }
        }

        public bool IsForTestCase(string testCaseName)
            => _chartData.Title.Equals(testCaseName, StringComparison.OrdinalIgnoreCase);

        private const string DefaultHorizontalAxisTitle = "Pages";
        private const string DefaultVerticalAxisTitle = "Time, seconds";

        public void AppendDataPoint(string columnName, PageTiming[] timings )
        {
            _columns.Add(new Column("number", columnName));
            AppendRows(timings);
        }

        private void AppendRows(PageTiming[] timings)
        {
            AppendValuesToExistingRowsMatchingTimings(timings);

            AppendNullToExistingRowsUnmatchingTimings(timings);

            AddNewRowsFromUnmatchedTimings(timings);
        }

        private void AppendValuesToExistingRowsMatchingTimings(PageTiming[] timings)
        {
            foreach (var pageTiming in timings)
            {
                var existingRow = _rows.TryGetValue(pageTiming.RequestUri, out List<object> values) ? values : null;
                existingRow?.Add(pageTiming.CumulativeValue);
            }
        }

        private void AppendNullToExistingRowsUnmatchingTimings(PageTiming[] timings)
        {
            foreach (var row in _rows)
            {
                if (!timings.Any(t => t.RequestUri.Equals(row.Key, StringComparison.OrdinalIgnoreCase)))
                {
                    row.Value.Add(null);
                }
            }
        }


        private void AddNewRowsFromUnmatchedTimings(PageTiming[] timings)
        {
            foreach (var pageTiming in timings)
            {
                bool rowExists = _rows.ContainsKey(pageTiming.RequestUri);
                if (!rowExists)
                {
                    var valuesForPreviousRuns = Enumerable.Repeat<object>(null, _columns.Count - 2); //skip 1 column for the page uri, one for current value
                    _rows.Add(pageTiming.RequestUri, new List<object>(valuesForPreviousRuns)
                    {
                        pageTiming.CumulativeValue
                    });
                }
            }
        }
    }
}