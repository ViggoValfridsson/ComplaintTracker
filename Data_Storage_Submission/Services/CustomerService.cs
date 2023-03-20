using Data_Storage_Submission.Context;
using Data_Storage_Submission.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data_Storage_Submission.Services;

internal class CustomerService : GenericServices<CustomerEntity>
{
    private readonly DataContext _context = new();

    public override async Task<IEnumerable<CustomerEntity>> GetAllAsync()
    {
        return await _context.Customers
            .Include(x => x.Address)
            .ToListAsync();
    }

    public override async Task<CustomerEntity> GetAsync(Expression<Func<CustomerEntity, bool>> predicate)
    {
        var item = await _context.Customers
            .Include(x => x.Address)
            .Include(x => x.Complaints)
            .FirstOrDefaultAsync(predicate);

        return item ?? null!;
    }

    public override async Task<CustomerEntity> SaveAsync(CustomerEntity entity)
    {
        var item = await GetAsync(x => x.FirstName == entity.FirstName && x.LastName == entity.LastName && x.PhoneNumber == entity.PhoneNumber && x.Email == entity.Email);

        if (item == null)
        {
            return await base.SaveAsync(entity);
        }

        throw new ArgumentException("Customer already exists in database.");
    }
}
