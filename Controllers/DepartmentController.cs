using EmployeePortal.Models;
using EmployeePortalDemo.Interfaces;
using EmployeePortalDemo.Models;
using EmployeePortalDemo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeePortalDemo.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IEmployeeEFService _repo;
        private readonly ApplicationDbContext _context;

        public DepartmentController(IEmployeeEFService repo,ApplicationDbContext context)
        {
            _repo = repo;
            _context = context;
        }


        public async Task<IActionResult> Index() // ✅ Must be async
        {
            var departments = await _repo.GetDepartmentsAsync(); // ✅ Await it
            return View(departments); // ✅ Now you're passing the actual list
        }

        public IActionResult Edit(int id)
        {
            var department = _context.Departments.Include(d => d.Employees)
                                                 .FirstOrDefault(d => d.DepartmentId == id);

            if (department == null) return NotFound();

            var viewModel = new Department
            {
                DepartmentId=department.DepartmentId,
                Name = department.Name,
                LocationId=department.LocationId,

                Employees = department.Employees.ToList()
            };

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Edit(Department model)
        {
        

            var dept = _context.Departments.FirstOrDefault(d => d.DepartmentId == model.DepartmentId);
            if (dept != null)
            {
                dept.Name = model.Name;
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Department updated successfully!";
            }

            return RedirectToAction("Index");
        }




    }

}
