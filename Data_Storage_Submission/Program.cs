using Data_Storage_Submission.Models.Entities;
using Data_Storage_Submission.Services;

Console.WriteLine("Hello World!");

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
//var statusTypeService = new StatusTypeService();

//var result = await statusTypeService.GetAsync(x => x.Id == 4);

//Console.WriteLine(result.StatusName);
#endregion

#region StatusType SaveAsync Test
//var statusTypeService = new StatusTypeService();

//var statusType = new StatusTypeEntity()
//{
//    StatusName = "Test Status",
//};

//await statusTypeService.SaveAsync(statusType);
#endregion

#region Employee tests
////get all
//var employeeService = new EmployeeService();

//var employees = await employeeService.GetAllAsync();

//foreach (var employee in employees)
//{
//    Console.WriteLine(employee.FirstName);
//    Console.WriteLine(employee.Department.Name);
//    foreach (var comment in employee.Comments)
//    {
//        Console.WriteLine(comment.Title);
//    }
//    Console.WriteLine();
//}

////GetAsync
//var employeeService = new EmployeeService();
//var employee = await employeeService.GetAsync(x => x.Id == Guid.Parse("7c12cec7-0d27-4063-98fc-e64e51194ddf"));

//Console.WriteLine(employee.FirstName);
//Console.WriteLine(employee.Department.Name);
//foreach (var comment in employee.Comments)
//{
//    Console.WriteLine(comment.Title);
//}



#endregion
