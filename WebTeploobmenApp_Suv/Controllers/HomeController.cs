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
        public IActionResult Calc(CalcModel model)
        {
            var result = model.OperationType switch
            {
                1 => model.Num1 + model.Num2,

                2 => model.Num1 - model.Num2,

                3 => model.Num1 * model.Num2,

                4 => model.Num1 / model.Num2,

                _ => throw new Exception()
            };

            var viewModel = new HomeCalcViewModel()
            {
                Result = result,
                Num1 = model.Num1,
                Num2 = model.Num2,
                OperationType = model.OperationType
            };
                
            //ViewData["result"] = result;


            return View(viewModel);
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
