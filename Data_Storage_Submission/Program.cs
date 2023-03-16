using Data_Storage_Submission.Services;

#region Generic GetAllAsync Test
//var departmentService = new DepartmentService();

//var results = await departmentService.GetAllAsync();

//foreach (var result in results)
//{
//    Console.WriteLine(result.Name);
//}
#endregion

#region Department GetAsync test
var departmentService = new DepartmentService();
var result = await departmentService.GetAsync(x => x.Id == 1);
Console.WriteLine( result.Name);

foreach(var employee in result.Employees)
{
    Console.WriteLine( employee.FirstName + " " + employee.LastName);
}
#endregion 

