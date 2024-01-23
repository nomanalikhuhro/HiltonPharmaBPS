using Microsoft.AspNetCore.Mvc;
using PASSForm_BPS.Models;
using System.Diagnostics;

namespace PASSForm_BPS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet] 
        public IActionResult PartialIndex(int id, string inputValue)
        {

            string result = $"Received ID: {id}, Input Value: {inputValue}";

            return View("PartialIndex"); // You can return any content you need.
        }

        public IActionResult Privacy()
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