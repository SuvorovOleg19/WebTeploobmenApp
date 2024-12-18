using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using WebTeploobmenApp_Suv.Data;
using WebTeploobmenApp_Suv.Models;

namespace WebTeploobmenApp_Suv.Controllers
{
    //  онтроллер дл€ главной страницы и операций с расчетами
    public class HomeController : Controller
    {
        // Ћоггер дл€ записи информации о работе контроллера
        private readonly ILogger<HomeController> _logger;

        //  онтекст базы данных дл€ работы с модел€ми и данными
        private readonly TeploobmenContext _context;

        //  онструктор, инициализирующий логгер и контекст базы данных
        public HomeController(ILogger<HomeController> logger, TeploobmenContext context)
        {
            _logger = logger;
            _context = context;
        }

        // ћетод отображени€ главной страницы
        public IActionResult Index()
        {
            // ѕолучаем список вариантов, доступных текущему пользователю
            var variants = _context.Variants
                .Where(x => x.UserId == null || x.UserId == GetUserId())
                .ToList();

            // ѕередаем варианты в представление
            return View(variants);
        }

        // ћетод дл€ удалени€ варианта (GET-запрос)
        [HttpGet]
        public IActionResult Delete(int id)
        {
            // »щем вариант по ID, который принадлежит текущему пользователю или доступен всем
            var variant = _context.Variants.FirstOrDefault(x => x.Id == id && (x.UserId == GetUserId() || x.UserId == null));
            if (variant != null)
            {
                // ”дал€ем найденный вариант и сохран€ем изменени€ в базе данных
                _context.Variants.Remove(variant);
                _context.SaveChanges();
            }

            // ѕеренаправл€ем на главную страницу
            return RedirectToAction("Index");
        }

        // ћетод дл€ получени€ данных варианта и отображени€ страницы расчета (GET-запрос)
        [HttpGet]
        public IActionResult Calc(int id)
        {
            // »щем вариант по ID, который принадлежит текущему пользователю или доступен всем
            var variant = _context.Variants.FirstOrDefault(x => x.Id == id && (x.UserId == GetUserId() || x.UserId == null));
            var viewModel = new HomeCalcViewModel();

            // ≈сли вариант найден, заполн€ем ViewModel данными из варианта
            if (variant != null)
            {
                viewModel.Height = variant.Height;
                viewModel.Diameter_of_pellets = variant.Diameter_of_pellets;
                viewModel.Temp_pellets = variant.Temp_pellets;
                viewModel.Temp_air = variant.Temp_air;
                viewModel.Speed_air = variant.Speed_air;
                viewModel.Avg_heat_capacity = variant.Avg_heat_capacity;
                viewModel.Consumption_of_pellets = variant.Consumption_of_pellets;
                viewModel.Heat_capacity_of_pellets = variant.Heat_capacity_of_pellets;
                viewModel.Diameter = variant.Diameter;
                viewModel.Volumetric_heat_transfer_coefficient = variant.Volumetric_heat_transfer_coefficient;
            }

            // ¬озвращаем ViewModel в представление
            return View(viewModel);
        }

        // ћетод дл€ выполнени€ расчета и сохранени€ результатов (POST-запрос)
        [HttpPost]
        public IActionResult Calc(CalcModel model, string action)
        {
            // ¬ыполн€ем расчет через метод CalcResult
            var result = model.CalcResult();

            // —оздаем ViewModel дл€ передачи данных в представление
            var viewModel = new HomeCalcViewModel()
            {
                Result = result,
                Height = model.Height,
                Diameter_of_pellets = model.Diameter_of_pellets,
                Temp_pellets = model.Temp_pellets,
                Temp_air = model.Temp_air,
                Speed_air = model.Speed_air,
                Avg_heat_capacity = model.Avg_heat_capacity,
                Consumption_of_pellets = model.Consumption_of_pellets,
                Heat_capacity_of_pellets = model.Heat_capacity_of_pellets,
                Diameter = model.Diameter,
                Volumetric_heat_transfer_coefficient = model.Volumetric_heat_transfer_coefficient,
            };

            // ≈сли действие - "добавить", сохран€ем новый вариант в базе данных
            if (action == "add")
            {
                _context.Variants.Add(new Variant
                {
                    UserId = GetUserId(),
                    Height = model.Height,
                    Diameter_of_pellets = model.Diameter_of_pellets,
                    Temp_pellets = model.Temp_pellets,
                    Temp_air = model.Temp_air,
                    Speed_air = model.Speed_air,
                    Avg_heat_capacity = model.Avg_heat_capacity,
                    Consumption_of_pellets = model.Consumption_of_pellets,
                    Heat_capacity_of_pellets = model.Heat_capacity_of_pellets,
                    Diameter = model.Diameter,
                    Volumetric_heat_transfer_coefficient = model.Volumetric_heat_transfer_coefficient,
                });
                _context.SaveChanges();
            }

            // ¬озвращаем ViewModel в представление
            return View(viewModel);
        }

        // ћетод дл€ получени€ ID текущего пользовател€ из claims
        private int? GetUserId()
        {
            // »звлекаем UserId из claims текущего пользовател€
            var userIdStr = User.FindFirst("UserId")?.Value;
            int? userId = userIdStr == null ? null : int.Parse(userIdStr);

            return userId;
        }

        // ћетод дл€ отображени€ страницы "ѕолитика конфиденциальности"
        public IActionResult Privacy()
        {
            return View();
        }

        // ћетод дл€ обработки ошибок
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
