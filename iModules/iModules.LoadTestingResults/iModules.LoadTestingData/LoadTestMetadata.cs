﻿using System;

namespace iModules.LoadTestingData
{
    public class LoadTestMetadata
    {
        public int LoadTestRunId { get; }

        public DateTime UtcStartTime { get;  }

        public DateTime UtcEndTime { get; }

        public int DurationInMinutes { get; }
    }
}