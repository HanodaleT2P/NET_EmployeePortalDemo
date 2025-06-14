using EmployeePortalDemo.Models;

namespace EmployeePortalDemo.ViewModels
{
    public class DepartmentFormViewModel
    {
      
            public int DepartmentId { get; set; }

            public string Name { get; set; } = null!;

            public int LocationId { get; set; }
            public List<Employee> Employees { get; set; }


    }
}
