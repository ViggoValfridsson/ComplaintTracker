using Data_Storage_Submission.Models;
using Data_Storage_Submission.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Storage_Submission.Services.Menus;

internal class ComplaintsMenuService
{
    private readonly ComplaintService _complaintService = new();

    public async Task DisplayOptionsMenu()
    {
        bool isRunning = true;

        while (isRunning)
        {
            Console.Clear();
            Console.WriteLine("Please select an option: \n");
            Console.WriteLine("1. View All Complaints");
            Console.WriteLine("2. View Specific Complaint");
            Console.WriteLine("3. Submit Complaint");
            Console.WriteLine("4. Go Back\n");

            var input = Console.ReadKey(true);

            switch (input.KeyChar)
            {
                case '1':
                    await DisplayAllComplaints();
                    break;
                case '2':
                    await DisplaySpecificComplain();
                    break;
                case '3':
                    await CreateComplaint();
                    break;
                case '4':
                    isRunning = false;
                    break;
                default:
                    Console.WriteLine("Invalid option, press enter to try again.");
                    Console.ReadLine();
                    break;
            }
        }
    }

    private async Task DisplayAllComplaints()
    {
        Console.Clear();
        Console.WriteLine("Loading...");

        var complaints = await _complaintService.GetAllAsync();

        Console.Clear();

        if (complaints == null)
        {
            Console.WriteLine("No complaints found.");
        }
        else
        {
            var displayTableService = new DisplayTableService<ComplaintSummaryModel>();
            var complaintSummaries = new List<ComplaintSummaryModel>();

            foreach (var complaint in complaints)
            {
                var complaintSummary = new ComplaintSummaryModel(complaint);
                complaintSummaries.Add(complaintSummary);
            }

            displayTableService.DisplayTable(complaintSummaries);
        }

        Console.WriteLine("\nPress enter to go back:");
        Console.ReadLine();
    }

    private async Task DisplaySpecificComplain()
    {
        bool isRunning = true;

        while (isRunning)
        {
            Console.Clear();
            Console.WriteLine("Please select an option: \n");
            Console.WriteLine("1. Get By Id");
            Console.WriteLine("2. Get By Title");
            Console.WriteLine("3. Go Back\n");

            var input = Console.ReadKey(true);

            switch (input.KeyChar)
            {
                case '1':
                    await GetComplaintById();
                    break;
                case '2':
                    await GetComplaintByTitle();
                    break;
                case '3':
                    isRunning = false;
                    break;
                default:
                    Console.WriteLine("Invalid option, press enter to try again.");
                    Console.ReadLine();
                    break;
            }
        }
    }

    private async Task GetComplaintById()
    {
        Console.Clear();
        Console.WriteLine("Enter the Id of the complaint you wish to view.");
        var id = Console.ReadLine();
        ComplaintEntity complaint = null!;

        try
        {
            complaint = await _complaintService.GetAsync(x => x.Id == Guid.Parse(id!));
        }
        catch (InvalidOperationException)
        {
            Console.WriteLine("Invalid Id format, please try again. Press enter to try again.");
            Console.ReadLine();
            return;
        }

        PrintDetailedComplaint(complaint);

        Console.ReadLine();
    }

    private async Task GetComplaintByTitle()
    {
        Console.Clear();
        Console.WriteLine("Enter the Title of the complaint you wish to view.");
        var title = Console.ReadLine();

        var complaint = await _complaintService.GetAsync(x => x.Title == title);

        PrintDetailedComplaint(complaint);

        Console.ReadLine();
    }

    private void PrintDetailedComplaint(ComplaintEntity complaint)
    {
        Console.Clear();

        if (complaint == null)
        {
            Console.WriteLine("Could not find complaint, please make sure that you entered a valid Id/title.  Press enter to try again.");
        }

        else
        {
            Console.WriteLine($"Id: {complaint.Id}");
            Console.WriteLine($"Title: {complaint.Title}");
            Console.WriteLine($"Description: {complaint.Description}");
            Console.WriteLine($"Product: {complaint.Product.Name}");
            Console.WriteLine($"Customer: {complaint.Customer.FirstName} {complaint.Customer.LastName}");
            Console.WriteLine($"Status: {complaint.StatusType.StatusName}");
            Console.WriteLine($"Submitted at: {complaint.SubmittedAt}");
            Console.WriteLine("\nComments:");

            foreach (var comment in complaint.Comments)
            {
                Console.WriteLine($"Id: {comment.Id}");
                Console.WriteLine($"Title: {comment.Title}");
                Console.WriteLine($"Descripion: {comment.Description}");
                Console.WriteLine($"Posted at: {comment.CreatedAt}");
                Console.WriteLine($"Created by: {comment.Employee.FirstName} {comment.Employee.LastName}\n");
            }
            Console.WriteLine("Press enter to go back.");
        }
    }
}
