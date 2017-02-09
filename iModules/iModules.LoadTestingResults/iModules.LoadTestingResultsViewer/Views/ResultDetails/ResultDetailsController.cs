using System;
using System.Threading.Tasks;
using iModules.LoadTestingResultsViewer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Modules.LoadTestingData;

namespace iModules.LoadTestingResultsViewer
{
    public class ResultDetailsController: Controller
    {
        private readonly LoadTestRepository _repository;

        public ResultDetailsController(LoadTestRepository _repository)
        {
            this._repository = _repository;
        }

        [HttpGet("/Details/{id:Guid}")]
        public async Task<ActionResult> DetailsIndex(Guid id)
        {
            ViewBag.Title = "Details for " + id;

            var metadata = await _repository.GetTestMetadataAsync(id);
            var vm = DetailsViewModel.FromMetadata(metadata);
            var testCases = await _repository.GetTestCasesAsync(metadata.LoadTestRunId);
            vm.TotalFailedTests = testCases.TotalFailed;
            vm.TotalPassedTests = testCases.TotalPassed;
            vm.TestCases = testCases.Cases;
            return View(vm);
        }

        [HttpGet("/Details/Errors")]
        public async Task<ActionResult> Errors(int loadTestRunId, string testCaseName)
        {
            var messages = await _repository.GetTestRunMessages(loadTestRunId, testCaseName);
            var vm = new ErrorsViewModel(messages);
            return View(vm);
        }
    }
}