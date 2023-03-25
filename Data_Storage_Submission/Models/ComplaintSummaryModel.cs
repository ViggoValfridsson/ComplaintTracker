using Data_Storage_Submission.Models.Entities;

namespace Data_Storage_Submission.Models;

internal class ComplaintSummaryModel
{
    public ComplaintSummaryModel(ComplaintEntity complaintEntity)
    {
        SubmittedAt = complaintEntity.SubmittedAt;
        Title = complaintEntity.Title;
        Product = complaintEntity.Product.Name;
        Status = complaintEntity.StatusType.StatusName;
        UserFullName = $"{complaintEntity.Customer.FirstName} {complaintEntity.Customer.LastName}";
    }

    public DateTime SubmittedAt { get; set; } 
    public string Title { get; set; } = null!;
    public string Product { get; set; } = null!;
    public string Status { get; set; } = null!;
    public string UserFullName { get; set; } = null!;
}