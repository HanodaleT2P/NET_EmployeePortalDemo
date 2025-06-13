using System;
using System.Collections.Generic;

namespace EmployeePortalDemo.Models;

public  class Location
{
    public int LocationId { get; set; }

    public string Name { get; set; } = null!;

    public string City { get; set; } = null!;
}
