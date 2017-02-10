using System;
using System.Threading.Tasks;
using iModules.LoadTestingData;
using iModules.LoadTestingResultsViewer.ViewModels;

namespace iModules.LoadTestingResultsViewer
{
    public static class DetailsViewBuilder
    {
        public static async Task<DetailsViewModel> Build(Guid id, LoadTestRepository repository)
        {
            var metadata = await repository.GetTestMetadataAsync(id);
            if(metadata == null) return new DetailsViewModel();
            var vm = DetailsViewModel.FromMetadata(metadata);
            var testCases = await repository.GetTestCasesAsync(metadata.LoadTestRunId);
            vm.TotalFailedTests = testCases.TotalFailed;
            vm.TotalPassedTests = testCases.TotalPassed;
            vm.TestCases = testCases.Cases;
            vm.PageTimings = await repository.GetPageTimingsAsync(metadata.LoadTestRunId);
            vm.LoadTestCounters = await repository.GetLoadTestCountersAsync(metadata.LoadTestRunId);

            return vm;
        }
    }
}