using Data_Storage_Submission.Models;
using Data_Storage_Submission.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Storage_Submission.Services.Menus;

internal class CommentsMenuService
{
    private readonly CommentService _commentService = new();

    public async Task DisplayOptionsMenu()
    {
        bool isRunning = true;

        while (isRunning)
        {
            Console.Clear();
            Console.WriteLine("Please select an option: \n");
            Console.WriteLine("1. Write New Comment");
            Console.WriteLine("2. View All Comments");
            Console.WriteLine("3. View Specific Comment");
            Console.WriteLine("4. Delete Comment");
            Console.WriteLine("5. Go Back\n");

            var input = Console.ReadKey(true);

            switch (input.KeyChar)
            {
                case '1':
                    await CreateComment();
                    break;
                case '2':
                    await DisplayAllComments();
                    break;
                case '3':
                    await DisplaySpecificComment();
                    break;
                case '4':
                    await DeleteComment();
                    break;
                case '5':
                    isRunning = false;
                    break;
                default:
                    Console.WriteLine("Invalid option, press enter to try again.");
                    Console.ReadLine();
                    break;
            }
        }

    }
    private async Task CreateComment()
    {
        while (true)
        {
            var comment = new CommentEntity();
            Console.Clear();
            Console.WriteLine("Please fill in the form to create a comment.");

            Console.WriteLine("Complaint Id: ");
            try
            {
                comment.ComplaintId = Guid.Parse(Console.ReadLine()!);
            }
            catch
            {
                Console.WriteLine("Not a valid comment id, press enter to try again.");
                Console.ReadLine();
                continue;
            }

            Console.WriteLine("Title: ");
            comment.Title = Console.ReadLine()!;
            Console.WriteLine("Description: ");
            comment.Description = Console.ReadLine()!;

            Console.WriteLine("Employee Id: ");
            try
            {
                comment.EmployeeId = Guid.Parse(Console.ReadLine()!);
            }
            catch
            {
                Console.WriteLine("Not a valid employee id, press enter to try again.");
                Console.ReadLine();
                continue;
            }

            Console.Clear();

            try
            {
                Console.WriteLine("Loading...");
                comment = await _commentService.SaveAsync(comment);
                var detailedComment = await _commentService.GetAsync(x => x.Id == comment.Id);
                Console.Clear();
                Console.WriteLine("Successfully added comment: \n");
                PrintDetailedComment(detailedComment);
            }
            catch (ArgumentException ex)
            {
                Console.Clear();
                Console.WriteLine(ex.Message + "\n\nPress enter to go back.");
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("Something went wrong when trying to save your complaint. Make sure that you entered valid information and try again.");
                Console.WriteLine("\nPress enter to go back.");
            }

            Console.ReadLine();
            break;
        }
    }

    private async Task DisplayAllComments()
    {
        Console.Clear();
        Console.WriteLine("Loading...");

        var comments = await _commentService.GetAllAsync();

        Console.Clear();

        if (comments == null)
        {
            Console.WriteLine("No complaints found.");
        }
        else
        {
            var displayTableService = new DisplayTableService<CommentSummaryModel>();
            var summaries = new List<CommentSummaryModel>();

            foreach (var comment in comments)
            {
                var summary = new CommentSummaryModel(comment);
                summaries.Add(summary);
            }

            displayTableService.DisplayTable(summaries);
        }

        Console.WriteLine("\nPress enter to go back:");
        Console.ReadLine();
    }

    private async Task DisplaySpecificComment()
    {
        Console.Clear();
        Console.WriteLine("Enter the Id of the comment you wish to view.");
        var id = Console.ReadLine();
        CommentEntity comment = null!;

        Console.Clear();
        Console.WriteLine("Loading...");

        try
        {
            comment = await _commentService.GetAsync(x => x.Id == Convert.ToInt32(id));
        }
        catch (InvalidOperationException)
        {
            Console.Clear();
            Console.WriteLine("Invalid Id format. Press enter to try again.");
            Console.ReadLine();
            return;
        }

        Console.Clear();
        PrintDetailedComment(comment);

        Console.ReadLine();
    }

    private async Task DeleteComment()
    {
        int commentId;
        Console.Clear();
        Console.WriteLine("Enter the id of the comment you wish to delete or press enter to go back.");

        var input = Console.ReadLine();

        if (String.IsNullOrWhiteSpace(input))
        {
            return;
        }

        try
        {
            commentId = Convert.ToInt32(input);
        }
        catch
        {
            Console.WriteLine("Not a valid comment id, press enter to try again.");
            Console.ReadLine();
            return;
        }

        Console.Clear();
        Console.WriteLine("Loading...");

        try
        {
            await _commentService.DeleteAsync(commentId);
            Console.Clear();
            Console.WriteLine("Succesfully deleted comment. Press enter to return.");
        }
        catch (ArgumentException ex)
        {
            Console.Clear();
            Console.WriteLine(ex.Message + " Press enter to try again.");
        }
        catch
        {
            Console.Clear();
            Console.WriteLine("Something went wrong, press enter to try again.");
        }

        Console.ReadLine();
    }

    private void PrintDetailedComment(CommentEntity comment)
    {
        if (comment == null)
        {
            Console.WriteLine("Could not find complaint, please make sure that you entered a valid Id/title.  Press enter to try again.");
        }
        else
        {
            Console.WriteLine($"Id: {comment.Id}");
            Console.WriteLine($"Title: {comment.Title}");
            Console.WriteLine($"Description: {comment.Description}");
            Console.WriteLine($"Written by: {comment.Employee.FirstName} {comment.Employee.LastName}");
            Console.WriteLine($"Submitted at: {comment.CreatedAt}");
            Console.WriteLine($"Complaint Id: {comment.ComplaintId}");

            Console.WriteLine("\nPress enter to go back.");
        }
    }
}
