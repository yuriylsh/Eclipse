using System;
using System.Threading.Tasks;
using iModules.LoadTestingData;
using iModules.LoadTestingResultsViewer.ViewModels;

namespace iModules.LoadTestingResultsViewer
{
    public class TestResultDataBuilder
    {
        public static async Task<TestDataViewModel> BuildAsync(Guid id, LoadTestRepository repository)
        {
            await Task.CompletedTask;
            return new TestDataViewModel();
        }
    }
}