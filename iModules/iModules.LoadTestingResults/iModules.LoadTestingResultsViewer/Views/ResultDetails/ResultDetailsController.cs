using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iModules.LoadTestingResultsViewer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using iModules.LoadTestingData;

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
            var vm = await DetailsViewBuilder.Build(id, _repository);
            return View(vm);
        }

        [HttpGet("/Details/Errors")]
        public async Task<ActionResult> Errors(int loadTestRunId, string testCaseName)
        {
            var messages = await _repository.GetTestRunMessagesAsync(loadTestRunId, testCaseName);
            var vm = new ErrorsViewModel(messages);
            return View(vm);
        }

        [HttpGet("/GetTestData")]
        public async Task<ActionResult> GetTestData(IEnumerable<Guid> ids)
        {
            return Json(await TestResultDataBuilder.BuildAsync(ids, _repository));
        }
    }
}