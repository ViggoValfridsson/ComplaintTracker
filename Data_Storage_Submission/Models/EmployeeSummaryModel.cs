﻿using Data_Storage_Submission.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Storage_Submission.Models;

internal class EmployeeSummaryModel
{
    public EmployeeSummaryModel(EmployeeEntity employee)
    {
        FirstName = employee.FirstName;
        LastName = employee.LastName;
        Department = employee.Department.Name;
    }

    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Department { get; set; } = null!;

}
