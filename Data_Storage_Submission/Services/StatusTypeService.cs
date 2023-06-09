﻿using Data_Storage_Submission.Context;
using Data_Storage_Submission.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data_Storage_Submission.Services;

internal class StatusTypeService : GenericServices<StatusTypeEntity>
{
    private readonly DataContext _context = new();

    public override async Task<StatusTypeEntity> GetAsync(Expression<Func<StatusTypeEntity, bool>> predicate)
    {
        var item = await _context.StatusTypes
            .Include(x => x.Complaints)
            .FirstOrDefaultAsync(predicate);

        return item ?? null!;
    }

    public override async Task<StatusTypeEntity> SaveAsync(StatusTypeEntity entity)
    {
        var item = await GetAsync(x => x.StatusName == entity.StatusName);

        if (item == null)
            return await base.SaveAsync(entity);

        throw new ArgumentException("StatusType already exists in database.");
    }
}
