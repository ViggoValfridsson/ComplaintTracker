using Data_Storage_Submission.Context;
using Data_Storage_Submission.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data_Storage_Submission.Services;

internal class DepartmentService : GenericServices<DepartmentEntity>
{
    private readonly DataContext _context = new DataContext();
    public override async Task<DepartmentEntity> GetAsync(Expression<Func<DepartmentEntity, bool>> predicate)
    {
        var item = await _context.Departments
            .Include(x => x.Employees)
            .FirstOrDefaultAsync(predicate);

        if (item != null)
        {
            return item;
        }

        return null!;
    }

    public override async Task<DepartmentEntity> SaveAsync(DepartmentEntity entity)
    {
        var item = await GetAsync(x => x.Name == entity.Name);

        if (item == null)
        {
            return await base.SaveAsync(entity);
        }

        throw new DbUpdateException("Item already exists in database.");
    }
}
