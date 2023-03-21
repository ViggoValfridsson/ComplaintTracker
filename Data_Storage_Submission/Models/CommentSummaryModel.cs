
using Data_Storage_Submission.Models.Entities;

namespace Data_Storage_Submission.Models;
internal class CommentSummaryModel
{
    public CommentSummaryModel(CommentEntity commentEntity)
    {
        Id = commentEntity.Id;
        CreatedAt = commentEntity.CreatedAt;
        Title = commentEntity.Title;
        EmployeeName = $"{commentEntity.Employee.FirstName} {commentEntity.Employee.LastName}";
        ComplaintId = commentEntity.ComplaintId;
    }

    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string Title { get; set; } = null!;
    public string EmployeeName { get; set; } = null!;
    public Guid ComplaintId { get; set; }
}
