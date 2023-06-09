﻿using Data_Storage_Submission.Models.Entities;

namespace Data_Storage_Submission.Models;

internal class CustomerSummaryModel
{
    public CustomerSummaryModel(CustomerEntity customerEntity)
    {
        FirstName = customerEntity.FirstName;
        LastName = customerEntity.LastName;
        Email = customerEntity.Email;
    }

    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;

}

