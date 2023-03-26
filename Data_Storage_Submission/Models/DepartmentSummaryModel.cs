using Data_Storage_Submission.Models.Entities;

namespace Data_Storage_Submission.Models;

internal class DepartmentSummaryModel
{
	public DepartmentSummaryModel(DepartmentEntity departmentEntity)
	{
			Name = departmentEntity.Name;
	}

    public string Name { get; set; } = null!;
}
