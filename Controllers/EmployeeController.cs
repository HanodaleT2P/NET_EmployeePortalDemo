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
        public async Task<IActionResult> Create(EmployeeListViewModel model, IFormFile PhotoFile)
        {
            if (PhotoFile != null && PhotoFile.Length > 0)
            {
                var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/employees");
                Directory.CreateDirectory(uploads);

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(PhotoFile.FileName);
                var filePath = Path.Combine(uploads, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await PhotoFile.CopyToAsync(stream);
                }

                model.Employee.PhotoPath = "/uploads/employees/" + fileName;
            }
            if (model.Employee.DepartmentId == 0)
            {
                ModelState.AddModelError("Employee.DepartmentId", "Please select a department.");
            }
            if (PhotoFile == null || PhotoFile.Length == 0)
            {
                ModelState.AddModelError("PhotoFile", "Please upload a photo.");
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
        public async Task<IActionResult> Edit(int id, EmployeeListViewModel model, IFormFile PhotoFile)
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
            // Handle photo upload
            if (PhotoFile != null && PhotoFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/employees");
                Directory.CreateDirectory(uploadsFolder); // Ensure folder exists

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(PhotoFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await PhotoFile.CopyToAsync(stream);
                }

                // Set the relative path to be saved in the DB
                model.Employee.PhotoPath = "/uploads/employees/" + fileName;
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
            var departments = await _repo.GetDepartmentsAsync();
      
         
            var deptName = departments
          .FirstOrDefault(d => d.Department.DepartmentId == emp.DepartmentId)?.Department?.Name;

            // Initialize emp.Department if it's null
            if (emp.Department == null)
                emp.Department = new Department();

            emp.Department.Name = deptName;
            if (emp == null)
                return NotFound();

            return View(emp);
        }

    }
}
