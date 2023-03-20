using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Storage_Submission.Models.Entities
{
    internal class CommentEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column(TypeName = "nvarchar(100)")]
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Guid EmployeeId { get; set; }
        public Guid ComplaintId { get; set; }
        public EmployeeEntity Employee { get; set; } = null!;
        public ComplaintEntity Complaint { get; set; } = null!;
    }
}
