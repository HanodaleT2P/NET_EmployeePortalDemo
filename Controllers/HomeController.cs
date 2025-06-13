using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EmployeePortalDemo.Models;

namespace EmployeePortalDemo.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    
        public IActionResult Index()
        {
            var user = HttpContext.Session.GetString("User");
            if (string.IsNullOrEmpty(user))
                return RedirectToAction("Index", "Login");

            ViewBag.User = user;
            return View();
        }
    
}
