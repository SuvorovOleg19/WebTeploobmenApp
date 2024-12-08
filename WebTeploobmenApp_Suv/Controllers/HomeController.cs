using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebTeploobmenApp_Suv.Models;

namespace WebTeploobmenApp_Suv.Controllers
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
        public IActionResult Calc(int num1, int num2, int operationType)
        {
            var result = operationType switch
            {
                1 => num1 + num2,

                2 => num1 - num2,

                3 => num1 * num2,

                4 => num1 / num2,

                _ => throw new Exception()
            };
                
                
                
            ViewData["result"] = result;

            return View();
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
