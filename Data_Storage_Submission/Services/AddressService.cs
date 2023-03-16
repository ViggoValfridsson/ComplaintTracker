using Data_Storage_Submission.Models.Entities;

namespace Data_Storage_Submission.Services;

internal class AddressService : GenericServices<AddressEntity>
{
    public override Task<AddressEntity> SaveAsync(AddressEntity entity)
    {
        return base.SaveAsync(entity);
    }
}
