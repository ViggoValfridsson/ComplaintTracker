using Data_Storage_Submission.Context;
using Data_Storage_Submission.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data_Storage_Submission.Services;

internal class EmployeeService : GenericServices<EmployeeEntity>
{
    private readonly DataContext _context = new();

    public override async Task<IEnumerable<EmployeeEntity>> GetAllAsync()
    {
        return await _context.Employees
            .Include(x => x.Department)
            .ToListAsync();
    }

    public override async Task<EmployeeEntity> GetAsync(Expression<Func<EmployeeEntity, bool>> predicate)
    {
        var item = await _context.Employees
            .Include(x => x.Comments)
            .Include(x => x.Department)
            .FirstOrDefaultAsync(predicate);

        return item ?? null!;
    }

    public override async Task<EmployeeEntity> SaveAsync(EmployeeEntity entity)
    {
        var item = await GetAsync(x => x.FirstName == entity.FirstName && x.LastName == entity.LastName && x.DepartmentId == entity.DepartmentId);

        if (item == null)
            return await base.SaveAsync(entity);

        throw new ArgumentException("Employee already exists in database.");
    }
}
