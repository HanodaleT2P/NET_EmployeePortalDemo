using EmployeePortal.Models;
using EmployeePortalDemo.Interfaces;
using EmployeePortalDemo.Models;
using EmployeePortalDemo.Services;
using EmployeePortalDemo.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeePortalDemo.Controllers
{
    [Authorize]
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
        public IActionResult Create()
        {
            var model = new DepartmentViewModel
            {
                Department = new Department()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _context.Departments.Add(model.Department);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Department created successfully.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> View(int id)
        {
            var department = _context.Departments.Include(d => d.Employees)
                                                   .FirstOrDefault(d => d.DepartmentId == id);

            if (department == null) return NotFound();

            var viewModel = new DepartmentFormViewModel
            {
                DepartmentId = department.DepartmentId,
                Name = department.Name,
                LocationId = department.LocationId,

                Employees = department.Employees.ToList()
            }; 
            return View(viewModel);
        }

        public IActionResult Edit(int id)
        {
            var department = _context.Departments.Include(d => d.Employees)
                                                 .FirstOrDefault(d => d.DepartmentId == id);

            if (department == null) return NotFound();

            var viewModel = new DepartmentFormViewModel
            {
                DepartmentId=department.DepartmentId,
                Name = department.Name,
                LocationId=department.LocationId,

                Employees = department.Employees.ToList()
            };

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Edit(DepartmentFormViewModel model)
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
