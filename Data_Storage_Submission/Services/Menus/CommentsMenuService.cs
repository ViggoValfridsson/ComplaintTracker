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
            var employeeSummaries = new List<EmployeeSummaryModel>();
            var comment = new CommentEntity();

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
                var employeeRow = new string(input!.Where(c => char.IsDigit(c)).ToArray());
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
