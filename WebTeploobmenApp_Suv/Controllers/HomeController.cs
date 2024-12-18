using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using WebTeploobmenApp_Suv.Data;
using WebTeploobmenApp_Suv.Models;

namespace WebTeploobmenApp_Suv.Controllers
{
    // ���������� ��� ������� �������� � �������� � ���������
    public class HomeController : Controller
    {
        // ������ ��� ������ ���������� � ������ �����������
        private readonly ILogger<HomeController> _logger;

        // �������� ���� ������ ��� ������ � �������� � �������
        private readonly TeploobmenContext _context;

        // �����������, ���������������� ������ � �������� ���� ������
        public HomeController(ILogger<HomeController> logger, TeploobmenContext context)
        {
            _logger = logger;
            _context = context;
        }

        // ����� ����������� ������� ��������
        public IActionResult Index()
        {
            // �������� ������ ���������, ��������� �������� ������������
            var variants = _context.Variants
                .Where(x => x.UserId == null || x.UserId == GetUserId())
                .ToList();

            // �������� �������� � �������������
            return View(variants);
        }

        // ����� ��� �������� �������� (GET-������)
        [HttpGet]
        public IActionResult Delete(int id)
        {
            // ���� ������� �� ID, ������� ����������� �������� ������������ ��� �������� ����
            var variant = _context.Variants.FirstOrDefault(x => x.Id == id && (x.UserId == GetUserId() || x.UserId == null));
            if (variant != null)
            {
                // ������� ��������� ������� � ��������� ��������� � ���� ������
                _context.Variants.Remove(variant);
                _context.SaveChanges();
            }

            // �������������� �� ������� ��������
            return RedirectToAction("Index");
        }

        // ����� ��� ��������� ������ �������� � ����������� �������� ������� (GET-������)
        [HttpGet]
        public IActionResult Calc(int id)
        {
            // ���� ������� �� ID, ������� ����������� �������� ������������ ��� �������� ����
            var variant = _context.Variants.FirstOrDefault(x => x.Id == id && (x.UserId == GetUserId() || x.UserId == null));
            var viewModel = new HomeCalcViewModel();

            // ���� ������� ������, ��������� ViewModel ������� �� ��������
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

            // ���������� ViewModel � �������������
            return View(viewModel);
        }

        // ����� ��� ���������� ������� � ���������� ����������� (POST-������)
        [HttpPost]
        public IActionResult Calc(CalcModel model, string action)
        {
            // ��������� ������ ����� ����� CalcResult
            var result = model.CalcResult();

            // ������� ViewModel ��� �������� ������ � �������������
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

            // ���� �������� - "��������", ��������� ����� ������� � ���� ������
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

            // ���������� ViewModel � �������������
            return View(viewModel);
        }

        // ����� ��� ��������� ID �������� ������������ �� claims
        private int? GetUserId()
        {
            // ��������� UserId �� claims �������� ������������
            var userIdStr = User.FindFirst("UserId")?.Value;
            int? userId = userIdStr == null ? null : int.Parse(userIdStr);

            return userId;
        }

        // ����� ��� ����������� �������� "�������� ������������������"
        public IActionResult Privacy()
        {
            return View();
        }

        // ����� ��� ��������� ������
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
