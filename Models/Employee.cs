using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeePortalDemo.Models;

public class Employee
{
    public int EmployeeId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime HireDate { get; set; }

    public int DepartmentId { get; set; }
    [ValidateNever]

    public Department Department { get; set; }

    public string? PhotoPath { get; set; }


}

