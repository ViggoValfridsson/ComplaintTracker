using Data_Storage_Submission.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Storage_Submission.Models;

internal class ComplaintSummaryModel
{
    public ComplaintSummaryModel(ComplaintEntity complaintEntity)
    {
        Id = complaintEntity.Id;
        SubmittedAt = complaintEntity.SubmittedAt;
        Title = complaintEntity.Title;
        ProductId = complaintEntity.ProductId;
        StatusTypeId = complaintEntity.StatusTypeId;
        UserFullName = $"{complaintEntity.Customer.FirstName} {complaintEntity.Customer.LastName}";
    }

    public Guid Id { get; set; }
    public DateTime SubmittedAt { get; set; } 
    public string Title { get; set; } = null!;
    public int ProductId { get; set; }
    public int StatusTypeId { get; set; }
    public string UserFullName { get; set; } = null!;
}