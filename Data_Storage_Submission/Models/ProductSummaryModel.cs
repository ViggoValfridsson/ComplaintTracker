using Data_Storage_Submission.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;


namespace Data_Storage_Submission.Models;

internal class ProductSummaryModel
{
    public ProductSummaryModel(ProductEntity productEntity)
    {
        Name = productEntity.Name;
        Description = productEntity.Description;
        Manufacturer = productEntity.Manufacturer;
    }

    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;

    public string Manufacturer { get; set; } = null!;
}
