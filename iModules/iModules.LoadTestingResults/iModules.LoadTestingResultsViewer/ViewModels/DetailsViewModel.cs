﻿using System;
using System.Collections.Generic;
using System.Net;
using iModules.LoadTestingData;

namespace iModules.LoadTestingResultsViewer.ViewModels
{
    public class DetailsViewModel
    {
        public int LoadTestRunId { get; set; }

        public string Start { get; private set; }

        public string End { get; private set; }

        public int Duration { get; private set; }

        public int TotalPassedTests { get; set; }

        public int TotalFailedTests { get; set; }

        public IEnumerable<TestCase> TestCases { get; set; } = Array.Empty<TestCase>();

        public IEnumerable<PageTiming> PageTimings { get; set; } = Array.Empty<PageTiming>();

        public IEnumerable<LoadTestCounter> LoadTestCounters { get; set; } = Array.Empty<LoadTestCounter>();

        public static DetailsViewModel FromMetadata(LoadTestMetadata metadata)
        {
            return new DetailsViewModel
            {
                LoadTestRunId =  metadata.LoadTestRunId,
                Start = metadata.UtcStartTime.ToLocalTime().ToString("f"),
                End = metadata.UtcEndTime.ToLocalTime().ToString("f"),
                Duration = metadata.DurationInMinutes
            };
        }

        public string GetErrorsLink(TestCase testCase, int loadTestRunId) 
            =>  $"/Details/Errors?loadTestRunId={loadTestRunId}&testCaseName={WebUtility.UrlEncode(testCase.Name)}";
    }
}