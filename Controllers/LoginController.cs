using EmployeePortal.Models;
using EmployeePortalDemo.Models;
using EmployeePortalDemo.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EmployeePortalDemo.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users
                    .FirstOrDefault(e => e.UserName == model.UserName && e.PasswordHash == model.Password);

                if (user != null)
                {
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                // optionally add role/ID/etc.
            };

                    var identity = new ClaimsIdentity(claims, "Cookies");
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync("Cookies", principal);
                    TempData["SuccessMessage"] = "Login successful!";
                    return RedirectToAction("Index", "Dashboard");
                }

                ModelState.AddModelError("", "Invalid username or password");
            }

            return View(model);
        }



        public IActionResult Logout()
        {
            HttpContext.SignOutAsync("Cookies"); // ⬅️ Signs out the cookie
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
