using Data_Storage_Submission.Context;
using Data_Storage_Submission.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data_Storage_Submission.Services;

internal class ProductService : GenericServices<ProductEntity>
{
    private readonly DataContext _context = new();
    public override async Task<ProductEntity> GetAsync(Expression<Func<ProductEntity, bool>> predicate)
    {
        var item = await _context.Products
            .Include(x => x.Complaints)
            .FirstOrDefaultAsync(predicate);

        return item ?? null!;
    }

    public override async Task<ProductEntity> SaveAsync(ProductEntity entity)
    {
        var item = await GetAsync(x => x.Manufacturer == entity.Manufacturer && x.Name == entity.Name);

        if (item == null)
            return await base.SaveAsync(entity);

        throw new ArgumentException("Product already exists in database.");
    }
}
