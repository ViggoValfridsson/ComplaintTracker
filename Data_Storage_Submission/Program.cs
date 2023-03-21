using Data_Storage_Submission.Models.Entities;
using Data_Storage_Submission.Services;
using Data_Storage_Submission.Services.Menus;

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

////SaveAsync
//var employeeService = new EmployeeService();

//var employee = new EmployeeEntity
//{
//    Id = Guid.Parse("c2193e33-db32-424a-9a9c-fb4e54d836cd"),
//    FirstName = "SaveMethodTest",
//    LastName = "Test",
//    DepartmentId = 1
//};

//var saved = await employeeService.SaveAsync(employee);

//Console.WriteLine(saved.FirstName);

#endregion

#region Customer test
//var customerService = new CustomerService();

////Get All
//var customers = await customerService.GetAllAsync();
//foreach (var customer in customers)
//{
//    Console.WriteLine(customer.FirstName + " " + customer.LastName);
//    Console.WriteLine(customer.Address.Street + " " + customer.Address.PostalCode +" " + customer.Address.City);
//}

////GetAsync
//var customer = await customerService.GetAsync(x => x.Id == Guid.Parse("68352aec-af93-4c59-a32c-b25ecc68c52a"));

//Console.WriteLine(customer.FirstName + " " + customer.Id);
//Console.WriteLine(customer.Address.Street + " " + customer.Address.PostalCode + " " + customer.Address.City);
//foreach (var complaint in customer.Complaints)
//{
//    Console.WriteLine("complaint: " + complaint.Title);
//}

////SaveAsync
//var customer = new CustomerEntity
//{
//    FirstName = "MethodTest2",
//    LastName = "Test3",
//    Email = "email@domain.com",
//    PhoneNumber = "1234567890",
//    Id = Guid.NewGuid(),
//    AddressId = 1
//};

//var created = await customerService.SaveAsync(customer);
//Console.WriteLine(created.FirstName);

#endregion

#region Comment test
//var commentService = new CommentService();

////GetAllAsync
//var comments = await commentService.GetAllAsync();

//foreach(var comment in comments)
//{
//    Console.WriteLine(comment.Title);
//    Console.WriteLine(comment.Employee.FirstName);
//    Console.WriteLine(comment.Complaint.Title);
//    Console.WriteLine();
//}

////GetAsync
//var comment = await commentService.GetAsync(x => x.Id == 2);
//Console.WriteLine(comment.Title);
//Console.WriteLine(comment.Employee.FirstName);
//Console.WriteLine(comment.Complaint.Title);

////SaveAsync
//var comment = new CommentEntity
//{
//    Title = "methodTest",
//    Description = "desc",
//    EmployeeId = Guid.Parse("d42def70-5b22-4304-9e48-889b6e2a434"),
//    ComplaintId = Guid.Parse("258744f0-f99e-45d2-bbbd-a70bafcea550"),
//    CreatedAt = DateTime.Now,
//};

//var saved = await commentService.SaveAsync(comment);
//Console.WriteLine(saved.Title);

#endregion

#region complaint test

//var complaintService = new ComplaintService();

////GetAllAsync
//var complaints = await complaintService.GetAllAsync();

//foreach (var complaint in complaints)
//{
//    Console.WriteLine(complaint.Title);
//    Console.WriteLine(complaint.Customer.FirstName);
//    Console.WriteLine(complaint.Product.Name);
//    Console.WriteLine();
//}


////GetAsync
//var complaint = await complaintService.GetAsync(x => x.Id == Guid.Parse("258744f0-f99e-45d2-bbbd-a70bafcea550"));
//Console.WriteLine(complaint.Title);
//Console.WriteLine(complaint.Customer.FirstName);
//Console.WriteLine(complaint.Product.Name);

////SaveAsync
//var complaint = new ComplaintEntity
//{
//    Id = Guid.NewGuid(),
//    Title = "Method",
//    Description = "Description",
//    CustomerId = Guid.Parse("68352aec-af93-4c59-a32c-b25ecc68c52a"),
//    ProductId = 1,
//    StatusTypeId = 1
//};

//var saved = await complaintService.SaveAsync(complaint);
//Console.WriteLine(saved.Title);

#endregion

var mainMenu = new MainMenuService();
await mainMenu.DisplayMainMenu();