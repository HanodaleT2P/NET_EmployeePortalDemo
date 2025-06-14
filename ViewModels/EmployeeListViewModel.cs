using EmployeePortalDemo.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeePortalDemo.ViewModels
{
    public class EmployeeListViewModel
    {
        public Employee Employee { get; set; }
        [ValidateNever] // Tells ASP.NET Core to skip validation
        public List<SelectListItem> Department { get; set; }
    }

}
