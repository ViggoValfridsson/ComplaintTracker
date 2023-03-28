using Data_Storage_Submission.Models;
using Data_Storage_Submission.Models.Entities;

namespace Data_Storage_Submission.Services.Menus;

internal class EmployeeMenuService
{
    private readonly EmployeeService _employeeService = new();
    private readonly DepartmentService _departmentService = new();

    public async Task DisplayEmployeeMenu()
    {
        bool inEmployeeMenu = true;

        while (inEmployeeMenu)
        {
            Console.Clear();
            Console.WriteLine("Loading...");

            var employees = await _employeeService.GetAllAsync();

            Console.Clear();

            if (employees.Count() < 1)
            {
                Console.WriteLine("No employees found.");
            }
            else
            {
                var displayTableService = new DisplayTableService<EmployeeSummaryModel>();
                var employeeSummaries = new List<EmployeeSummaryModel>();

                // Converts from EmployeeEntity to EmployeeSummaryModel to make table view less cluttered
                foreach (var employee in employees)
                {
                    var employeeSummary = new EmployeeSummaryModel(employee);
                    employeeSummaries.Add(employeeSummary);
                }

                displayTableService.DisplayTable(employeeSummaries);
            }

            Console.WriteLine("\nCommands:");
            Console.WriteLine("delete <#>".PadRight(25) + "Delete specific employee. <#> = row number");
            Console.WriteLine("new".PadRight(25) + "Add new employee");
            Console.WriteLine("exit".PadRight(25) + "Return to main menu");
            Console.WriteLine();

            var input = Console.ReadLine()!.ToLower();

            if (input!.Contains("delete"))
            {
                //Strips input of letters
                var rowNumber = new string(input.Where(c => char.IsDigit(c)).ToArray());
                EmployeeEntity choosenEmployee;

                Console.Clear();

                try
                {
                    // Numeric input is used to index employee colletion
                    Guid employeeId = (employees!.ToList()[Convert.ToInt32(rowNumber) - 1]).Id;
                    choosenEmployee = await _employeeService.GetAsync(x => x.Id == employeeId);
                }
                catch
                {
                    Console.WriteLine("Not a valid row number, press enter to try again.");
                    Console.ReadLine();
                    continue;
                }

                try
                {
                    await _employeeService.DeleteAsync(choosenEmployee);
                    Console.WriteLine($"Successfully deleted employee on row {rowNumber}, press enter to go back.");
                    Console.ReadLine();
                }
                catch
                {
                    Console.WriteLine("Something went wrong when deleting, please try again.");
                    Console.ReadLine();
                }

            }
            else
            {
                switch (input)
                {
                    case "new":
                        await DisplayEmployeeCreation();
                        break;
                    case "exit":
                        inEmployeeMenu = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Not a valid command, press enter to try again");
                        Console.ReadLine();
                        break;
                }
            }
        }
    }

    private async Task DisplayEmployeeCreation()
    {
        var employee = new EmployeeEntity();

        Console.Clear();
        Console.WriteLine("Please fill in the form to add a new employee.");
        Console.WriteLine("First name:");
        employee.FirstName = Console.ReadLine()!;
        Console.WriteLine("Last name");
        employee.LastName = Console.ReadLine()!;

        Console.Clear();
        Console.WriteLine("Do you wish to connect the complaint to an existing department or a new one?");
        Console.WriteLine("\nCommands:");
        Console.WriteLine("existing".PadRight(25) + "Pick an already existing department.");
        Console.WriteLine("new".PadRight(25) + "Create a new department and connect employee to it.");
        Console.WriteLine();

        var departmentType = Console.ReadLine()!.ToLower();

        switch (departmentType)
        {
            case "existing":
                try
                {
                    employee.DepartmentId = await ChooseExistingDepartment();
                }
                catch (ArgumentException ex)
                {
                    // Catches invalid input
                    Console.Clear();
                    Console.WriteLine(ex.Message + ". Press enter to try again");
                    Console.ReadLine();
                    return;
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message + ". Press enter to try again");
                    Console.ReadLine();
                    return;
                }
                break;
            case "new":
                try
                {
                    employee.DepartmentId = await CreateDepartment();
                }
                catch 
                {
                    Console.Clear();
                    Console.WriteLine("Something went wrong when saving the department. Make sure you entered valid information and try again.");
                    Console.ReadLine();
                    return;
                }
                break;
            default:
                Console.Clear();
                Console.WriteLine("Not a valid command, press enter to try again");
                Console.ReadLine();
                return;
        }

        try
        {
            Console.Clear();
            Console.WriteLine("Loading...");
            await _employeeService.SaveAsync(employee);
            Console.Clear();
            Console.WriteLine("Successfully added employee");
        }
        catch (ArgumentException ex)
        {
            Console.Clear();
            Console.WriteLine(ex.Message + "\nPress enter to go back.");
        }
        catch
        {
            Console.Clear();
            Console.WriteLine("Something went wrong when trying to add employee. Make sure that you entered valid information and try again.");
            Console.WriteLine("Press enter to go back.");
        }

        Console.ReadLine();
    }

    private async Task<int> ChooseExistingDepartment()
    {
        var departments = await _departmentService.GetAllAsync();
        var departmentTableService = new DisplayTableService<DepartmentSummaryModel>();
        var departmentSummaries = new List<DepartmentSummaryModel>();

        foreach (var department in departments)
        {
            var departmentSummary = new DepartmentSummaryModel(department);
            departmentSummaries.Add(departmentSummary);
        }

        Console.Clear();
        departmentTableService.DisplayTable(departmentSummaries);
        Console.WriteLine("\nChoose the department you wish to connect the employee to. For example write \"1\" for row one (not id).");

        try
        {
            var input = Console.ReadLine();
            // Strips input of non-numeric characters and uses it to index department
            var departmentRow = new string(input!.Where(c => char.IsDigit(c)).ToArray());
            var department = departments.ToList()[Convert.ToInt32(departmentRow) - 1];
            return department.Id;
        }
        catch
        {
            throw new ArgumentException("Not a valid department");
        }
    }

    private async Task<int> CreateDepartment()
    {
        var department = new DepartmentEntity();

        Console.Clear();
        Console.WriteLine("Please fill in the form to create a department.");
        Console.WriteLine("Department name:");
        department.Name = Console.ReadLine()!;

        Console.Clear();
        Console.WriteLine("Loading...");

        department = await _departmentService.SaveAsync(department);
        
        Console.Clear();
        Console.WriteLine("Successfully added department.");
        Console.ReadLine();

        return department.Id;
    }
}
