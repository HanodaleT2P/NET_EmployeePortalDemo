
using EmployeePortal.Models;
using EmployeePortalDemo.Interfaces;
using EmployeePortalDemo.Models;
using EmployeePortalDemo.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Data;


namespace EmployeePortalDemo.Services
{
    public class DepartmentEFService : IDepartmentEFService
    {
        private readonly ApplicationDbContext _context;

        public DepartmentEFService(ApplicationDbContext context)
        {
            _context = context;
        }

       
        public async Task<List<DepartmentViewModel>> GetDepartmentsAsync()
        {
            return await _context.Departments
                .Include(e => e.Employees)
                .Select(e => new DepartmentViewModel
                {
                    Department = new Department
                    {
                        DepartmentId = e.DepartmentId,
                        Name = e.Name,
                        LocationId = e.LocationId
                    },
                   
                })
                .ToListAsync();
        }

    }

}
