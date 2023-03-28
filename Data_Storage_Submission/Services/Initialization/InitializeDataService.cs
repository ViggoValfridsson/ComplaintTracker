using Data_Storage_Submission.Context;
using Data_Storage_Submission.Models.Entities;

namespace Data_Storage_Submission.Services.Initialization;

internal class InitializeDataService
{
    private DataContext _context = new();
    private StatusTypeService _statusTypeService = new();
    private AddressService _addressService = new();
    private DepartmentService _departmentService = new();
    private ProductService _productService = new();
    private EmployeeService _employeeService = new();
    private CustomerService _customerService = new();
    private ComplaintService _complaintService = new();
    private CommentService _commentService = new();

    public async Task InitializeAll()
    {
        Console.WriteLine("Loading...");
        await InitializeStatusTypes();
        await InitializeAddresses();
        await InitializeDepartments();
        await InitializeProducts();
        await InitializeEmployees();
        await InitializeCustomers();
        await InitializeComplaints();
        await InitializeComments();
    }

    //This method is public since if you don't want to use the dummy data you may still wish to create the statuses.
    public async Task InitializeStatusTypes()
    {
        if (!_context.StatusTypes.Any())
        {
            var statusTypes = new List<StatusTypeEntity>
            {
                new StatusTypeEntity
                {
                    StatusName = "Not started"
                },
                new StatusTypeEntity
                {
                    StatusName = "Under investigation"
                },
                new StatusTypeEntity
                {
                    StatusName = "Closed"
                }
            };

            foreach (var statusType in statusTypes)
            {
                await _statusTypeService.SaveAsync(statusType);
            }
        }
    }

    private async Task InitializeAddresses()
    {
        if (!_context.Addresses.Any())
        {
            var addresses = new List<AddressEntity>
            {
                new AddressEntity
                {
                    Street = "Storgatan 24",
                    PostalCode = "12345",
                    City = "Stockholm"
                },
                new AddressEntity
                {
                    Street = "Kungsgatan 14",
                    PostalCode = "45678",
                    City = "Göteborg"
                },
                new AddressEntity
                {
                    Street = "Dorttninggatan 8",
                    PostalCode = "78901",
                    City = "Malmö"
                },
            };

            foreach (var address in addresses)
            {
                await _addressService.SaveAsync(address);
            }
        }
    }

    private async Task InitializeDepartments()
    {
        if (!_context.Departments.Any())
        {
            var departments = new List<DepartmentEntity>
            {
                new DepartmentEntity
                {
                    Name = "The Vinyl Vault"
                },
                new DepartmentEntity
                {
                    Name = "The CD Corner"
                },
                new DepartmentEntity
                {
                    Name = "The Retro Record Room"
                },
            };

            foreach (var department in departments)
            {
                await _departmentService.SaveAsync(department);
            }
        }
    }

    private async Task InitializeProducts()
    {
        if (!_context.Products.Any())
        {
            var products = new List<ProductEntity>
            {
                new ProductEntity
                {
                    Name = "Cannibal Corpse - Butchered At Birth. Audio CD",
                    Description = "The second album by Cannibal Corpse.",
                    Manufacturer = "Caroline Records"
                },
                new ProductEntity
                {
                    Name = "Cryptopsy - None So Vile. Vinyl",
                    Description = "Cryptopsy's second album.",
                    Manufacturer = "Wrong Again Records"
                },
                new ProductEntity
                {
                    Name = "Suffocation - Effigy Of The Forgotten. Red Vinyl",
                    Description = "Debut album by New York Death Metal Band Suffocation",
                    Manufacturer = "RC Records"
                }
            };

            foreach (var product in products)
            {
                await _productService.SaveAsync(product);
            }
        }

    }

    private async Task InitializeEmployees()
    {
        if (!_context.Employees.Any())
        {
            var employees = new List<EmployeeEntity>
            {
                new EmployeeEntity
                {
                    FirstName = "Abigail",
                    LastName = "Winters",
                    DepartmentId = (await _departmentService.GetAsync(x => x.Name == "The Vinyl Vault")).Id
                },
                new EmployeeEntity
                {
                    FirstName = "Brandon",
                    LastName = "Lawrence",
                    DepartmentId = (await _departmentService.GetAsync(x => x.Name == "The CD Corner")).Id
                },
                new EmployeeEntity
                {
                    FirstName = "Coraline",
                    LastName = "Gonzales",
                    DepartmentId = (await _departmentService.GetAsync(x => x.Name == "The Retro Record Room")).Id
                }
            };

            foreach (var employee in employees)
            {
                await _employeeService.SaveAsync(employee);
            }
        }
    }

    private async Task InitializeCustomers()
    {
        if (!_context.Customers.Any())
        {
            var customers = new List<CustomerEntity>
            {
                new CustomerEntity
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "johndoe@domain.com",
                    PhoneNumber = "1234567890",
                    AddressId = (await _addressService.GetAsync(x => x.Street== "Storgatan 24")).Id
                },
                new CustomerEntity
                {
                    FirstName = "Bill",
                    LastName = "Smith",
                    Email = "billsmith@domain.com",
                    PhoneNumber = "1234567890",
                    AddressId = (await _addressService.GetAsync(x => x.Street== "Kungsgatan 14")).Id
                },
                new CustomerEntity
                {
                    FirstName = "Wilma",
                    LastName = "Andersson",
                    Email = "wilmaandersson@domain.com",
                    PhoneNumber = "1234567890",
                    AddressId = (await _addressService.GetAsync(x => x.Street== "Dorttninggatan 8")).Id
                }
            };

            foreach (var customer in customers)
            {
                await _customerService.SaveAsync(customer);
            }
        }
    }

    private async Task InitializeComplaints()
    {
        if (!_context.Complaints.Any())
        {
            var complaints = new List<ComplaintEntity>
            {
                new ComplaintEntity
                {
                    Title = "Weird skipping on vinyl",
                    Description = "When i play the vinyl it randomly skips a few seconds.",
                    CustomerId = (await _customerService.GetAsync(x => x.FirstName == "Wilma")).Id,
                    ProductId = (await _productService.GetAsync(x => x.Name == "Suffocation - Effigy Of The Forgotten. Red Vinyl")).Id,
                },
                new ComplaintEntity
                {
                    Title = "Broken case",
                    Description = "CD arrived with a cracked case.",
                    CustomerId = (await _customerService.GetAsync(x => x.FirstName == "John")).Id,
                    ProductId = (await _productService.GetAsync(x => x.Name == "Cannibal Corpse - Butchered At Birth. Audio CD")).Id,
                },
                new ComplaintEntity
                {
                    Title = "Wrong album!",
                    Description = "I ordered None So Vile but i recieved Abba greatest hits instead!",
                    CustomerId = (await _customerService.GetAsync(x => x.FirstName == "Bill")).Id,
                    ProductId = (await _productService.GetAsync(x => x.Name == "Cryptopsy - None So Vile. Vinyl")).Id,
                }
            };

            foreach (var complaint in complaints)
            {
                await _complaintService.SaveAsync(complaint);
            }
        }
    }

    private async Task InitializeComments()
    {
        if (!_context.Comments.Any())
        {
            var comments = new List<CommentEntity>
            {
                new CommentEntity
                {
                    Title = "Skipping issues",
                    Description = "Hello, we are sorry you are experiencing this issue. Please make sure that your turntable and stylus are working properly. Make sure that there is no dust or debris. Lastly check if the vinyl is warped, if it is you will recieve a refund.",
                    EmployeeId = (await _employeeService.GetAsync(x => x.LastName == "Winters")).Id,
                    ComplaintId = (await _complaintService.GetAsync(x => x.Title == "Weird skipping on vinyl")).Id,
                },
                new CommentEntity
                {
                    Title = "Broken case",
                    Description = "We inspected your CD before sending it and it seems it was broken during shipping. Please contact your parcel service, if they refuse a refund please contact us again.",
                    EmployeeId = (await _employeeService.GetAsync(x => x.LastName == "Lawrence")).Id,
                    ComplaintId = (await _complaintService.GetAsync(x => x.Title == "Broken case")).Id,
                },
                new CommentEntity
                {
                    Title = "Album mixup",
                    Description = "We are so sorry for this. There was a mixup during one of our shipments last night. To make it up to you we will send you 3 free vinyls of your choice.",
                    EmployeeId = (await _employeeService.GetAsync(x => x.LastName == "Gonzales")).Id,
                    ComplaintId = (await _complaintService.GetAsync(x => x.Title == "Wrong album!")).Id,
                }
             };

            foreach (var comment in comments)
            {
                await _commentService.SaveAsync(comment);
            }
        }
    }
}
