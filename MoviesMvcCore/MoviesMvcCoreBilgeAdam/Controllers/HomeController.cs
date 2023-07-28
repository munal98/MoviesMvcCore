using Microsoft.AspNetCore.Mvc;
using MoviesMvcCoreBilgeAdam.Models;
using System.Diagnostics;

namespace MoviesMvcCoreBilgeAdam.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index() // ~/Home/Index
        {
            //return View(); // Views/Home/Index.cshtml
            return View("MyIndex"); // Views/Home/MyIndex.cshtml
        }

        public IActionResult Privacy() // ~/Home/Privacy
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}