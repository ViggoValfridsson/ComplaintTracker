using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Storage_Submission.Models.Entities;

internal class ComplaintEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime SubmittedAt { get; set; } = DateTime.Now;

    [Column(TypeName = "nvarchar(100)")]
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public Guid CustomerId { get; set; }
    public int ProductId { get; set; }
    public int StatusTypeId { get; set; }
    public CustomerEntity Customer { get; set; } = null!;
    public ProductEntity Product { get; set; } = null!;
    public StatusTypeEntity StatusType { get; set; } = null!;
    public ICollection<CommentEntity> Comments { get; set; } = new HashSet<CommentEntity>();
}
