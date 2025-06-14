using EmployeePortal.Models;
using EmployeePortalDemo.Interfaces;
using EmployeePortalDemo.Models;
using EmployeePortalDemo.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EmployeePortalDemo.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeEFService _repo;

        public EmployeeController(IEmployeeEFService repo)
        {
            _repo = repo;
        }
      
        public async Task<IActionResult> Index()
        {
            var employees = await _repo.GetAllAsync();
            return View(employees);
        }

        public async Task<IActionResult> Create()
        {
            var departments = await _repo.GetDepartmentsAsync();
            var model = new EmployeeListViewModel
            {
                Employee = new Employee
                {
                    HireDate = DateTime.Today
                },
                Department = departments.Select(d => new SelectListItem
                {
                    Value = d.Department.DepartmentId.ToString(),
                    Text = d.Department.Name
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeListViewModel model)
        {
            if (model.Employee.DepartmentId == 0)
            {
                ModelState.AddModelError("Employee.DepartmentId", "Please select a department.");
            }

            // Check email duplicate
            if (_repo.IsEmailDuplicate(model.Employee.Email))
            {
                ModelState.AddModelError("Employee.Email", "Email already exists.");
            }

            model.Department = (await _repo.GetDepartmentsAsync()).Select(d => new SelectListItem
            {
                Value = d.Department. DepartmentId.ToString(),
                Text = d.Department. Name
            }).ToList();
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _repo.AddAsync(model.Employee);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _repo.GetByIdAsync(id);
            if (employee == null)
                return NotFound();

            var departments = await _repo.GetDepartmentsAsync();
            var model = new EmployeeListViewModel
            {
                Employee = employee,
                Department = departments.Select(d => new SelectListItem
                {
                    Value = d.Department.DepartmentId.ToString(),
                    Text = d.Department.Name
                }).ToList()
            };

            return View("Create", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EmployeeListViewModel model)
        {
            if (id != model.Employee.EmployeeId)
                return NotFound();

            if (!ModelState.IsValid)
            {
                model.Department = (await _repo.GetDepartmentsAsync()).Select(d => new SelectListItem
                {
                    Value = d.Department.DepartmentId.ToString(),
                    Text = d.Department.Name
                }).ToList();
                return View("Create", model);
            }

            await _repo.UpdateAsync(model.Employee);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var emp = await _repo.GetByIdAsync(id);
            if (emp == null)
                return NotFound();

            return View(emp);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var emp = await _repo.GetByIdAsync(id);
            if (emp == null)
                return NotFound();

            return View(emp);
        }

    }
}
