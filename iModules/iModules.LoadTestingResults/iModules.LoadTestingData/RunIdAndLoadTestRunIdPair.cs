using System;

namespace iModules.LoadTestingData
{
    internal class RunIdAndLoadTestRunIdPair
    {
        public Guid RunId { get; set; }

        public int LoadTestRunId { get; set; }
    }
}