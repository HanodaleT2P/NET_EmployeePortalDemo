using EmployeePortal.Models;
using EmployeePortalDemo.Models;
using EmployeePortalDemo.ViewModels;



namespace EmployeePortalDemo.Interfaces
{
    public interface IEmployeeEFService
    {
        Task<List<EmployeeFormViewModel>> GetAllAsync();
        Task<Employee?> GetByIdAsync(int id);
        Task AddAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(int id);
        Task<List<DepartmentViewModel>> GetDepartmentsAsync();
        bool IsEmailDuplicate(string email, int? employeeId = null);
    }
}
