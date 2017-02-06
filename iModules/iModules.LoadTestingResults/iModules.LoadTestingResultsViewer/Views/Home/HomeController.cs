using Microsoft.AspNetCore.Mvc;

namespace iModules.LoadTestingResultsViewer.Views.Home
{
    public class HomeController: Controller
    {
        [HttpGet("/")]
        public ActionResult Index()
        {
            return View();
        }
    }
}