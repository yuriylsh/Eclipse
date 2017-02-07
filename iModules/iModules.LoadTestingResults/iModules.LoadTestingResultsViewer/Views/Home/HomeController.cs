using System;
using System.Threading.Tasks;
using iModules.LoadTestingResultsViewer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Modules.LoadTestingData;

namespace iModules.LoadTestingResultsViewer.Views.Home
{
    public class HomeController: Controller
    {
        private readonly ResultIdentifiersRepository _resultIdentifiersRepository;

        public HomeController(ResultIdentifiersRepository resultIdentifiersRepository)
        {
            _resultIdentifiersRepository = resultIdentifiersRepository;
        }

        [HttpGet("/")]
        public async Task<ActionResult> Index()
        {
            return View(new HomeIndexViewModel());
        }

        [HttpGet("/GetInitialResults")]
        public async Task<ActionResult> GetInitialResults(int pageIndex, int pageSize)
        {
            var (results, count) = await _resultIdentifiersRepository.GetInitialResultsAsync(pageIndex, pageSize);
            return Json(new {data = results, itemsCount = count});
        }

        [HttpGet("/GetResults")]
        public async Task<ActionResult> GetResults(int pageIndex, int pageSize)
        {
            var results = await _resultIdentifiersRepository.GetResultsAsync(pageIndex, pageSize);
            return Json(results);
        }

        [HttpPost("/SetResultName")]
        public async Task SetResultName(Guid id, string name)
        {
            await _resultIdentifiersRepository.SetResultName(id, name);
        }
    }
}