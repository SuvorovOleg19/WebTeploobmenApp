using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebTeploobmenApp_Suv.Data;
using WebTeploobmenApp_Suv.Models;

namespace WebTeploobmenApp_Suv.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly TeploobmenContext _context;

        public HomeController(ILogger<HomeController> logger, TeploobmenContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var variants = _context.Variants.ToList();


            return View(variants);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var variant = _context.Variants.FirstOrDefault(x => x.Id == id);
            if(variant != null)
            {
                _context.Variants.Remove(variant);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Calc(int id)
        {
            var variant = _context.Variants.FirstOrDefault(x => x.Id == id);
            var viewModel = new HomeCalcViewModel();

            if (variant != null)
            {
                viewModel.Num1 = variant.Num1;
                viewModel.Num2 = variant.Num2;
                viewModel.OperationType = variant.OperationType;
            } 

            return View(viewModel);
        }

            [HttpPost]
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

            _context.Variants.Add(new Variant
            {
                Num1 = model.Num1,
                Num2 = model.Num2,
                OperationType = model.OperationType
            });
            _context.SaveChanges();
                
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
