namespace iModules.LoadTestingData
{
    public class LoadTestCounter
    {
        public string CategoryName { get; set; }

        public string CounterName { get; set; }

        public double CumulativeValue { get; set; }

        public bool HigherIsBetter { get; set; }

        public string GetFullCounterName() => string.Concat(CategoryName, ": ", CounterName);
    }
}