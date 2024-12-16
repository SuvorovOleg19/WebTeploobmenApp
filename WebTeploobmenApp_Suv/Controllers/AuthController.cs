using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebTeploobmenApp_Suv.Data;

namespace WebTeploobmenApp_Suv.Controllers
{
    public class AuthController : Controller
    {
        private readonly TeploobmenContext _context;
        public AuthController(TeploobmenContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Logout()
        {

            await HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Index(string login, string password)
        {
            var user = _context.Users.FirstOrDefault(x => x.Login == login && x.Password == password);
            if (user != null)
            {
                var claims = new List<Claim> { 
                    new("UserId", user.Id.ToString()),
                    new Claim(ClaimTypes.Name, login) 
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
