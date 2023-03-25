using Data_Storage_Submission.Models.Entities;

namespace Data_Storage_Submission.Services;

internal class AddressService : GenericServices<AddressEntity>
{
    public override async Task<AddressEntity> SaveAsync(AddressEntity entity)
    {
        var item = await GetAsync(x => x.Street == entity.Street && x.City == entity.City && x.PostalCode == entity.PostalCode);

        if (item == null)
            return await base.SaveAsync(entity);

        throw new ArgumentException("Address already exists in database.");
    }
}
