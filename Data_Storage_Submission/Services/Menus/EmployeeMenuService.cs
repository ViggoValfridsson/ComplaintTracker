using Data_Storage_Submission.Models;
using Data_Storage_Submission.Models.Entities;

namespace Data_Storage_Submission.Services.Menus;

internal class EmployeeMenuService
{
    private readonly EmployeeService _employeeService = new();

    public async Task DisplayEmployeeMenu()
    {
        bool inEmployeeMenu = true;

        while (inEmployeeMenu)
        {
            Console.Clear();
            Console.WriteLine("Loading...");

            var employees = await _employeeService.GetAllAsync();

            Console.Clear();

            if (employees == null)
            {
                Console.WriteLine("No employees found.");
            }
            else
            {
                var displayTableService = new DisplayTableService<EmployeeSummaryModel>();
                var employeeSummaries = new List<EmployeeSummaryModel>();

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
                var rowNumber = new string(input.Where(c => char.IsDigit(c)).ToArray());
                EmployeeEntity choosenEmployee;

                Console.Clear();

                try
                {
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

    public async Task DisplayEmployeeCreation() { }
}
