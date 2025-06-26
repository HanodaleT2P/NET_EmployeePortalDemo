using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace EmployeePortalDemo.ViewModels
{
    public class EmployeeFormViewModel
    {
        public int EmployeeId { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }

        [Required]
        public int DepartmentId { get; set; }


        public string Department { get; set; }

        [ValidateNever]
        public List<SelectListItem> Departments { get; set; } = new();

        public string? PhotoPath { get; set; }
    }

}
