using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Storage_Submission.Models.Entities;

internal class EmployeeEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column(TypeName = "nvarchar(100)")]
    public string FirstName { get; set; } = null!;

    [Column(TypeName = "nvarchar(100)")]
    public string LastName { get; set; } = null!;
    public int DepartmentId { get; set; }
    public DepartmentEntity Department { get; set; } = null!;
    public ICollection<CommentEntity> Comments { get; set; } = new HashSet<CommentEntity>();
}
