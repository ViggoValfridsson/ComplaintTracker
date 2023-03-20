using Data_Storage_Submission.Context;
using Data_Storage_Submission.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data_Storage_Submission.Services;

internal class CommentService : GenericServices<CommentEntity>
{
    private readonly DataContext _context = new();
    public override async Task<IEnumerable<CommentEntity>> GetAllAsync()
    {
        return await _context.Comments
            .Include(x => x.Complaint)
            .Include(x => x.Employee)
            .ToListAsync();
    }

    public override async Task<CommentEntity> GetAsync(Expression<Func<CommentEntity, bool>> predicate)
    {
        var item = await _context.Comments
            .Include(x => x.Complaint)
            .Include(x => x.Employee)
            .FirstOrDefaultAsync(predicate);

        return item ?? null!;
    }

    public override async Task<CommentEntity> SaveAsync(CommentEntity entity)
    {
        var item = await GetAsync(x => x.Title == entity.Title && x.ComplaintId == entity.ComplaintId && x.EmployeeId == entity.EmployeeId && x.Description == entity.Description);

        if (item == null)
            return await base.SaveAsync(entity);

        throw new ArgumentException("Comment already exists in database.");
    }
}
