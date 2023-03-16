using Data_Storage_Submission.Models.Entities;
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
//var departmentService = new DepartmentService();
//var result = await departmentService.GetAsync(x => x.Id == 4);
//Console.WriteLine(result.Name);

//foreach (var employee in result.Employees)
//{
//    Console.WriteLine(employee.FirstName + " " + employee.LastName);
//}
#endregion 

#region Department SaveAsync Test
//var departmentService = new DepartmentService();

////already exists
//var oldDepartment = new DepartmentEntity()
//{
//    Name = "TestDepartment"
//};

////new department
//var newDepartment = new DepartmentEntity()
//{
//    Name = "TestDepartment3"
//};

//await departmentService.SaveAsync(newDepartment);
#endregion 

#region Address SaveAsync Test
//var addressService = new AddressService();

//var address = new AddressEntity()
//{
//    Street = "Test Street 10",
//    City = "Test City",
//    PostalCode = "12345"
//};

//await addressService.SaveAsync(address);
#endregion 

#region StatusType GetAsync Test
var statusTypeService = new StatusTypeService();

var result = await statusTypeService.GetAsync(x => x.Id == 4);

Console.WriteLine(result.StatusName);
#endregion

