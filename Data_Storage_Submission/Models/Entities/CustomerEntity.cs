using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Storage_Submission.Models.Entities;

internal class CustomerEntity
{
    public Guid Id { get; set; }
    [Column(TypeName = "nvarchar(100)")]
    public string FirstName { get; set; } = null!;

    [Column(TypeName = "nvarchar(100)")]
    public string LastName { get; set; } = null!;

    [Column(TypeName = "nvarchar(320)")]
    public string Email { get; set; } = null!;

    [Column(TypeName = "char(13)")]
    public string PhoneNumber { get; set; } = null!;
    public int AddressId { get; set; }
    public AddressEntity Address { get; set; } = null!;
    public ICollection<ComplaintEntity> Complaints { get; set; } = new HashSet<ComplaintEntity>();
}
