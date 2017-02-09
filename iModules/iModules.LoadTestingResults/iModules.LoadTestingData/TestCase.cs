namespace iModules.LoadTestingData
{
    public class TestCase
    {
        public string Name { get; set; }

        public int Passed { get; set; }

        public int Failed { get; set; }

        public int Runs { get; set; }

        public double Minimum { get; set; }

        public double Avarage { get; set; }

        public double Percentile90 { get; set; }

        public double Maximum { get; set; }
    }
}