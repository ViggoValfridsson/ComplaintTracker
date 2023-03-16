using Data_Storage_Submission.Models.Entities;
using System.Linq.Expressions;

namespace Data_Storage_Submission.Services;

internal class DepartmentService : GenericServices<DepartmentEntity>
{
    public override Task<DepartmentEntity> GetAsync(Expression<Func<DepartmentEntity, bool>> predicate)
    {
        
    }
}
