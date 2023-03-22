using Data_Storage_Submission.Models;
using Data_Storage_Submission.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

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
            Console.WriteLine("3. Update Complaint Status");
            Console.WriteLine("4. Submit Complaint");
            Console.WriteLine("5. Delete Complaint");
            Console.WriteLine("6. Go Back\n");

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
                    await UpdateComplaintStatus();
                    break;
                case '4':
                    await CreateComplaint();
                    break;
                case '5':
                    await DeleteComplaint();
                    break;
                case '6':
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

        if (complaints.Count() <= 0)
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

        Console.Clear();
        Console.WriteLine("Loading...");

        try
        {
            complaint = await _complaintService.GetAsync(x => x.Id == Guid.Parse(id!));
        }
        catch (InvalidOperationException)
        {
            Console.Clear();
            Console.WriteLine("Invalid Id format. Press enter to try again.");
            Console.ReadLine();
            return;
        }

        Console.Clear();
        PrintDetailedComplaint(complaint);

        Console.ReadLine();
    }

    private async Task GetComplaintByTitle()
    {
        Console.Clear();
        Console.WriteLine("Enter the Title of the complaint you wish to view.");
        var title = Console.ReadLine();

        Console.Clear();
        Console.WriteLine("Loading...");

        var complaint = await _complaintService.GetAsync(x => x.Title == title);

        Console.Clear();
        PrintDetailedComplaint(complaint);

        Console.ReadLine();
    }

    private void PrintDetailedComplaint(ComplaintEntity complaint)
    {
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

            if (complaint.Comments.Count > 0)
            {
                Console.WriteLine("\nComments:");

                foreach (var comment in complaint.Comments)
                {
                    Console.WriteLine($"Id: {comment.Id}");
                    Console.WriteLine($"Title: {comment.Title}");
                    Console.WriteLine($"Descripion: {comment.Description}");
                    Console.WriteLine($"Posted at: {comment.CreatedAt}");
                    Console.WriteLine($"Created by: {comment.Employee.FirstName} {comment.Employee.LastName}\n");
                }
            }
            else
            {
                Console.WriteLine("\nThis complaint has no comments yet.");
            }

            Console.WriteLine("\nPress enter to go back.");
        }
    }

    private async Task UpdateComplaintStatus()
    {
        Console.Clear();
        Console.WriteLine("Enter the id of the complaint you wish to update.");
        Guid complaintId;
        int statusTypeId;

        try
        {
            complaintId = Guid.Parse(Console.ReadLine()!);
        }
        catch
        {
            Console.WriteLine("Not a valid complaint id, press enter to try again.");
            Console.ReadLine();
            return;
        }

        Console.Clear();

        Console.WriteLine("Pick the new status type");
        Console.WriteLine("1. Not started");
        Console.WriteLine("2. Under investigation");
        Console.WriteLine("3. Closed\n");

        var input = Console.ReadKey(true);

        switch (input.KeyChar)
        {
            case '1':
                statusTypeId = 1;
                break;
            case '2':
                statusTypeId = 2;
                break;
            case '3':
                statusTypeId = 3;
                break;
            default:
                Console.Clear();
                Console.WriteLine("Not a valid status type, press enter to try again.");
                Console.ReadLine();
                return;
        }

        Console.Clear();
        Console.WriteLine("Loading...");

        try
        {
            await _complaintService.ChangeStatusAsync(complaintId, statusTypeId);
            var complaint = await _complaintService.GetAsync(x => x.Id == complaintId);
            Console.Clear();
            PrintDetailedComplaint(complaint);
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

    private async Task DeleteComplaint()
    {
        Guid complaintId;
        Console.Clear();
        Console.WriteLine("Enter the id of the complaint you wish to delete or press enter to go back.");

        var input = Console.ReadLine();

        if (String.IsNullOrWhiteSpace(input))
        {
            return;
        }

        try
        {
            complaintId = Guid.Parse(input);
        }
        catch
        {
            Console.WriteLine("Not a valid complaint id, press enter to try again.");
            Console.ReadLine();
            return;
        }

        Console.Clear();
        Console.WriteLine("Loading...");

        try
        {
            await _complaintService.DeleteAsync(complaintId);
            Console.Clear();
            Console.WriteLine("Succesfully deleted complaint. Press enter to return.");
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

    private async Task CreateComplaint()
    {
        while (true)
        {
            var complaint = new ComplaintEntity();
            Console.Clear();
            Console.WriteLine("Please fill in the form to create a complaint.");
            Console.WriteLine("Title: ");
            complaint.Title = Console.ReadLine()!;
            Console.WriteLine("Description: ");
            complaint.Description = Console.ReadLine()!;

            Console.WriteLine("Product Id: ");
            try
            {
                complaint.ProductId = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Not a valid product id, press enter to try again.");
                Console.ReadLine();
                continue;
            }

            Console.WriteLine("Customer Id: ");
            try
            {
                complaint.CustomerId = Guid.Parse(Console.ReadLine()!);
            }
            catch
            {
                Console.WriteLine("Not a valid customer id, press enter to try again.");
                Console.ReadLine();
                continue;
            }

            Console.Clear();

            try
            {
                Console.WriteLine("Loading...");
                complaint = await _complaintService.SaveAsync(complaint);
                var detailedComplaint = await _complaintService.GetAsync(x => x.Id == complaint.Id);
                Console.Clear();
                Console.WriteLine("Successfully added complaint: \n");
                PrintDetailedComplaint(detailedComplaint);
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
}
