using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Storage_Submission.Models.Entities;

internal class AddressEntity
{
    public int Id { get; set; }
    [Column(TypeName = "nvarchar(100)")]
    public string Street { get; set; } = null!;
    [Column(TypeName = "char(5)")]
    public string PostalCode { get; set; } = null!;
    [Column(TypeName = "nvarchar(100)")]
    public string City { get; set; } = null!;
    public ICollection<CustomerEntity> Customers { get; set; } = new HashSet<CustomerEntity>();
}
