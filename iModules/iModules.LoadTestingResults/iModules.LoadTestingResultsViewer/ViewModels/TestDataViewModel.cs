using System.Collections.Generic;

namespace iModules.LoadTestingResultsViewer.ViewModels
{
    public class TestDataViewModel
    {
        public IEnumerable<ChartData> Charts { get; set; }
    }

    public class ChartData
    {
        public IEnumerable<Column> Columns{ get; set; }

        public IEnumerable<object[]> Rows { get; set; }

        public string Title { get; set; }

        public string HAxisTitle { get; set; } = "Pages";

        public string VAxisTitle { get; set; } = "Time, seconds";
    }

    public class Column
    {
        public Column(string dataType, string name)
        {
            DataType = dataType;
            Name = name;
        }

        public string DataType { get; set; }
        public string Name { get; set; }
    }
}