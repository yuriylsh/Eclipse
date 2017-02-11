using System.Collections.Generic;

namespace iModules.LoadTestingResultsViewer
{
    public class ComparisonGrid
    {
        public IEnumerable<ComparisonGridHeader> Headers { get; set; }

        public IEnumerable<ComparisonGridRow> Rows { get; set; }
    }

    public class ComparisonGridHeader
    {
        public string Header { get; set; }
    }

    public class ComparisonGridRow
    {
        public IEnumerable<ComparisonGridCell> Cells { get; set; }
    }

    public class ComparisonGridCell
    {
        public string Value { get; set; }
        public string IncreaseValue { get; set; }
        public string DecreaseValue { get; set; }
    }
}