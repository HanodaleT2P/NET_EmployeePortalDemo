using EmployeePortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeePortalDemo.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize]
        public IActionResult Index()
        {
            ViewBag.EmployeeCount = _context.Employees.Count();
            ViewBag.DepartmentCount = _context.Departments.Count();
            return View();
        }
    }
}
