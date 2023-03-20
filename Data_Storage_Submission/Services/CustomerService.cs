using Data_Storage_Submission.Models.Entities;
using System.Linq.Expressions;

namespace Data_Storage_Submission.Services;

internal class CustomerService : GenericServices<CustomerEntity>
{
    public override Task<IEnumerable<CustomerEntity>> GetAllAsync()
    {
        return base.GetAllAsync();
    }

    public override Task<CustomerEntity> GetAsync(Expression<Func<CustomerEntity, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public override Task<CustomerEntity> SaveAsync(CustomerEntity entity)
    {
        throw new NotImplementedException();
    }
}
