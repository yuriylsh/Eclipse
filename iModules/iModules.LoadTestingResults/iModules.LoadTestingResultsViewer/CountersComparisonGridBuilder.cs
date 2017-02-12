using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iModules.LoadTestingData;

namespace iModules.LoadTestingResultsViewer
{
    public static class CountersComparisonGridBuilder
    {
        public static async Task<ComparisonGrid> BuildCountersData(
            Guid[] ids, 
            string[] names, 
            IDictionary<Guid, int> idToLoadTestRunIdMap,
            LoadTestRepository repository)
        {
            IEnumerable<LoadTestCounter>[] testCounters = await GetCountersAsync(ids, idToLoadTestRunIdMap, repository);

            var headers = new List<ComparisonGridHeader>(names.Select(name => new ComparisonGridHeader { Header = name }));
            headers.Insert(0, new ComparisonGridHeader { Header = "Counter" });
            headers.Insert(0, new ComparisonGridHeader { Header = "Category" });

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
                    if (rows.TryGetValue(counter.GetFullCounterName(), out List<ComparisonGridCell> cells))
                    {
                        if (isBaseline)
                        {
                            cells.Add(new ComparisonGridCell { Value = counter.CumulativeValue.ToString("0.####") });
                        }
                        else
                        {
                            cells.Add(GetNonBaselineCounterValue(counter, testCounters[0].First(baseline => baseline.GetFullCounterName() == counter.GetFullCounterName())));
                        }
                    }
                }
            }

            foreach (var counter in seedCounters)
            {
                var row = rows[counter.GetFullCounterName()];
                row.Insert(0, new ComparisonGridCell { Value = counter.CounterName });
                row.Insert(0, new ComparisonGridCell { Value = counter.CategoryName });
            }
            
            var result = new ComparisonGrid
            {
                Headers = headers,
                Rows = seedCounters
                    .Select(counter => counter.GetFullCounterName())
                    .Select(counterName => new ComparisonGridRow { Cells = rows[counterName] })
            };

            return result;
        }

        private static ComparisonGridCell GetNonBaselineCounterValue(LoadTestCounter counter, LoadTestCounter baseline)
        {
            var cell = new ComparisonGridCell { Value = counter.CumulativeValue.ToString("0.####") };
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