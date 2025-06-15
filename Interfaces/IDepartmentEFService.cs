using EmployeePortal.Models;
using EmployeePortalDemo.Models;
using EmployeePortalDemo.ViewModels;



namespace EmployeePortalDemo.Interfaces
{
    public interface IDepartmentEFService
    { 
     
        Task<List<DepartmentViewModel>> GetDepartmentsAsync();
       
    }
}
