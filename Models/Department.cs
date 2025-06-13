using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace EmployeePortalDemo.Models;

public  class Department
{
    public int DepartmentId { get; set; }

    public string Name { get; set; } = null!;

    public int LocationId { get; set; }
    public List<Employee> Employees { get; set; }




}


public class DepartmentViewModel
{
    public Department Department { get; set; }
 


}
