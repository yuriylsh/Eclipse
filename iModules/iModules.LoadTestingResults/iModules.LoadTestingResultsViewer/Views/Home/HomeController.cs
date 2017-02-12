using System;
using System.Linq;
using System.Threading.Tasks;
using iModules.LoadTestingResultsViewer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using iModules.LoadTestingData;

namespace iModules.LoadTestingResultsViewer
{
    public class HomeController: Controller
    {
        private readonly ResultIdentifiersRepository _resultIdentifiersRepository;

        public HomeController(ResultIdentifiersRepository resultIdentifiersRepository)
        {
            _resultIdentifiersRepository = resultIdentifiersRepository;
        }

        [HttpGet("/")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet("/GetInitialResults")]
        public async Task<ActionResult> GetInitialResults(int pageIndex, int pageSize)
        {
            var (results, count) = await _resultIdentifiersRepository.GetInitialResultsAsync(pageIndex, pageSize);
            return Json(new {data = results.Select(ResultIdentifierViewModel.FromResultIdentifier), itemsCount = count});
        }

        [HttpGet("/GetResults")]
        public async Task<ActionResult> GetResults(int pageIndex, int pageSize)
        {
            var results = await _resultIdentifiersRepository.GetResultsAsync(pageIndex, pageSize);
            return Json(results.Select(ResultIdentifierViewModel.FromResultIdentifier));
        }

        [HttpPost("/SetResultName")]
        public async Task SetResultName(Guid id, string name)
        {
            await _resultIdentifiersRepository.SetResultName(id, name);
        }
    }
}