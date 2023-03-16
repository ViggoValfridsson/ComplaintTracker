using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Storage_Submission.Models.Entities;

internal class StatusTypeEntity
{
    public int Id { get; set; }

    [Column(TypeName = "nvarchar(100)")]
    public string StatusName { get; set; } = null!;
    public ICollection<ComplaintEntity> Complaints { get; set; } = new HashSet<ComplaintEntity>();
}
