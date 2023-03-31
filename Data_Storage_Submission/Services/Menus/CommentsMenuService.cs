using Data_Storage_Submission.Models;
using Data_Storage_Submission.Models.Entities;

namespace Data_Storage_Submission.Services.Menus;

internal class CommentsMenuService
{
    private readonly CommentService _commentService = new();
    private readonly EmployeeService _employeeService = new();
    private readonly DisplayTableService<EmployeeSummaryModel> _displayTableService = new();

    public async Task CreateComment(Guid complaintId)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Loading...");

            var employees = await _employeeService.GetAllAsync();

            if (employees.Count() < 1)
            {
                Console.Clear();
                Console.WriteLine("There are no employees in the database. Please add employees before trying to create a comment.");
                Console.WriteLine("Press enter to return");
                Console.ReadLine();
                break;
            }

            var employeeSummaries = new List<EmployeeSummaryModel>();
            var comment = new CommentEntity();

            // Converts EmployeeEntities to EmployeeSummaryModels to prevent table from becoming too wide and filled with unnecessary information
            foreach (var employee in employees)
            {
                var employeeSummary = new EmployeeSummaryModel(employee);
                employeeSummaries.Add(employeeSummary);
            }

            Console.Clear();
            Console.WriteLine("Please fill in the form to create a comment.");
            comment.ComplaintId = complaintId;
            Console.WriteLine("Title: ");
            comment.Title = Console.ReadLine()!;
            Console.WriteLine("Description: ");
            comment.Description = Console.ReadLine()!;

            Console.WriteLine("Who is writing? Write the row number matching the employee. Example command: \"1\"");
            _displayTableService.DisplayTable(employeeSummaries);

            try
            {
                var input = Console.ReadLine();
                // Remove all non-numeric characters from the string.
                var employeeRow = new string(input!.Where(c => char.IsDigit(c)).ToArray());
                // Uses the numeric string to index the list. The -1 subtraction is because arrays start at 0.
                var employee = employees.ToList()[Convert.ToInt32(employeeRow) - 1];
                comment.EmployeeId = employee.Id;
            }
            catch 
            {
                Console.WriteLine("Not a valid employee.");
                Console.ReadLine();
                break;
            }

            Console.Clear();

            try
            {
                Console.WriteLine("Loading...");
                await _commentService.SaveAsync(comment);
                Console.Clear();
                Console.WriteLine("Successfully added comment.");
            }
            // Catches the exception if you tried to add an already existing item.
            catch (ArgumentException ex)
            {
                Console.Clear();
                Console.WriteLine(ex.Message + "\nPress enter to go back.");
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("Something went wrong when trying to save your comment. Make sure that you entered valid information and try again.");
                Console.WriteLine("Press enter to go back.");
            }

            Console.ReadLine();
            break;
        }
    }
}
