using Data_Storage_Submission.Models.Entities;

namespace Data_Storage_Submission.Models;

internal class ProductSummaryModel
{
    public ProductSummaryModel(ProductEntity productEntity)
    {
        Name = productEntity.Name;
        Manufacturer = productEntity.Manufacturer;

        // Makes sure the description isn't too long which would prevent table view from displaying correctly.
        if (productEntity.Description.Length > 50)
        {
            Description = productEntity.Description.Substring(0, 47) + "...";
        }
        else
        {
            Description = productEntity.Description;
        }
    }

    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Manufacturer { get; set; } = null!;
}
