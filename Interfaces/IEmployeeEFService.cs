using EmployeePortal.Models;
using EmployeePortalDemo.Models;



namespace EmployeePortalDemo.Interfaces
{
    public interface IEmployeeEFService
    {
        Task<List<EmployeeEFViewModel>> GetAllAsync();
        Task<Employee?> GetByIdAsync(int id);
        Task AddAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(int id);
        Task<List<DepartmentViewModel>> GetDepartmentsAsync();
        bool IsEmailDuplicate(string email, int? employeeId = null);
    }
}
