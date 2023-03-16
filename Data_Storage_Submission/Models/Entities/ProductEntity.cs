using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Storage_Submission.Models.Entities;

internal class ProductEntity
{
    public int Id { get; set; }

    [Column(TypeName = "nvarchar(100)")]
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;

    [Column(TypeName = "nvarchar(150)")]
    public string Manufacturer { get; set; } = null!;
    public ICollection<ComplaintEntity> Complaints { get; set; } = new HashSet<ComplaintEntity>();
}
