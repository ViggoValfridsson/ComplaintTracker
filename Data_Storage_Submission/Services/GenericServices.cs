using Data_Storage_Submission.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data_Storage_Submission.Services;

internal abstract class GenericServices<T> where T : class
{
    private readonly DataContext _context = new();

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public virtual async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
    {
        var item = await _context.Set<T>().FirstOrDefaultAsync(predicate);

        return item ?? null!;
    }

    public virtual async Task<T> SaveAsync(T entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity;
    }
}
