using Data_Storage_Submission.Context;
using Data_Storage_Submission.Models.Entities;
using Data_Storage_Submission.Services.Menus;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data_Storage_Submission.Services;

internal class ComplaintService : GenericServices<ComplaintEntity>
{
    private readonly DataContext _context = new();

    public override async Task<IEnumerable<ComplaintEntity>> GetAllAsync()
    {
        return await _context.Complaints
            .Include(x => x.Customer)
            .Include(x => x.StatusType)
            .Include(x => x.Product)
            .ToListAsync();
    }

    public override async Task<ComplaintEntity> GetAsync(Expression<Func<ComplaintEntity, bool>> predicate)
    {
        var item = await _context.Complaints
            .Include(x => x.Customer)
            .Include(x => x.Product)
            .Include(x => x.StatusType)
            .Include(x => x.Comments)
            .ThenInclude(x => x.Employee)
            .FirstOrDefaultAsync(predicate);

        return item ?? null!;
    }

    public override async Task<ComplaintEntity> SaveAsync(ComplaintEntity entity)
    {
        var item = await GetAsync(x => x.CustomerId == entity.CustomerId && x.ProductId == entity.ProductId && x.Title == entity.Title && x.Description == entity.Description);

        if (item == null)
            return await base.SaveAsync(entity);

        throw new ArgumentException("Complaint already exists in database.");
    }

    public async Task<ComplaintEntity> ChangeStatusAsync(Guid complaintId, int statusTypeId)
    {
        var item = await GetAsync(x => x.Id == complaintId) ?? throw new ArgumentException("Could not find complaint.");

        item.StatusTypeId = statusTypeId;
        
        _context.Update(item);
        await _context.SaveChangesAsync();

        return item;
    }

    public async Task DeleteAsync(Guid complaintId)
    {
        var item = await GetAsync(x => x.Id == complaintId) ?? throw new ArgumentException("Could not find complaint.");

        _context.Remove(item);
        await _context.SaveChangesAsync();
    }
}
