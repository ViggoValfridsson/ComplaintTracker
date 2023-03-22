using Data_Storage_Submission.Models;
using Data_Storage_Submission.Models.Entities;

namespace Data_Storage_Submission.Services.Menus;

internal class ComplaintsMenuService
{
    private readonly ComplaintService _complaintService = new();
    private readonly CommentsMenuService _commentMenuService = new();

    public async Task DisplayComplaintsMenu()
    {
        var inComplaintsMenu = true;

        while (inComplaintsMenu)
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

            Console.WriteLine("\nCommands:");
            Console.WriteLine("open <#>".PadRight(25) + "View complaint details. <#> = row number");
            Console.WriteLine("new".PadRight(25) + "Add new complaint");
            Console.WriteLine("exit".PadRight(25) + "Return to main menu");

            var input = Console.ReadLine()!.ToLower();

            if (input!.Contains("open"))
            {
                var rowNumber = new string(input.Where(c => char.IsDigit(c)).ToArray());
                ComplaintEntity choosenComplaint;

                Console.Clear();

                try
                {
                    Guid complaintId = (complaints.ToList()[Convert.ToInt32(rowNumber) - 1]).Id;
                    choosenComplaint = await _complaintService.GetAsync(x => x.Id == complaintId);
                }
                catch
                {
                    Console.WriteLine("Not a valid row number, press enter to try again.");
                    Console.ReadLine();
                    continue;
                }

                await DisplayComplaintOptions(choosenComplaint);
            }
            else
            {
                switch (input)
                {
                    case "new":
                        Console.WriteLine("new");
                        //glöm inte try catchen för argument exception
                        break;
                    case "exit":
                        inComplaintsMenu = false;
                        break;
                    default:
                        Console.WriteLine("Not a valid command, press enter to try again");
                        Console.ReadLine();
                        break;
                }
            }
        }
    }
    public async Task DisplayComplaintOptions(ComplaintEntity complaint)
    {
        bool inComplaintOptions = true;

        while (inComplaintOptions)
        {
            Console.Clear();
            PrintDetailedComplaint(complaint);
            Console.WriteLine("\nCommands:");
            Console.WriteLine("status <number>".PadRight(25) + "Change status. 1: Not started | 2: Under investigation | 3. Closed");
            Console.WriteLine("comment".PadRight(25) + "Create a new comment.");
            Console.WriteLine("exit".PadRight(25) + "Go back to complaints menu.");

            var input = Console.ReadLine()!.ToLower();

            switch (input)
            {
                case "status 1":
                case "status 2":
                case "status 3":
                    var statusId = new string(input.Where(c => char.IsDigit(c)).ToArray());
                    await _complaintService.ChangeStatusAsync(complaint.Id, Convert.ToInt32(statusId));
                    inComplaintOptions = false;
                    break;
                case "comment":
                    await _commentMenuService.CreateComment(complaint.Id);
                    inComplaintOptions = false;
                    break;
                case "exit":
                    inComplaintOptions = false;
                    break;
                default:
                    Console.WriteLine("Not a valid command, press enter to try again");
                    Console.ReadLine();
                    break;
            }
        }
    }

    public void PrintDetailedComplaint(ComplaintEntity complaint)
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
                string indentation = new string(' ', 4);
                var comments = complaint.Comments.ToList();
                Console.WriteLine("Comments: [");

                for (int i = 0; i < complaint.Comments.Count(); i++)
                {
                    Console.WriteLine($"{indentation}{{");
                    Console.WriteLine($"{indentation}{indentation}Id: {comments[i].Id}");
                    Console.WriteLine($"{indentation}{indentation}Title: {comments[i].Title}");
                    Console.WriteLine($"{indentation}{indentation}Descripion: {comments[i].Description}");
                    Console.WriteLine($"{indentation}{indentation}Posted at: {comments[i].CreatedAt}");
                    Console.WriteLine($"{indentation}{indentation}Created by: {comments[i].Employee.FirstName} {comments[i].Employee.LastName}");


                    if (i == complaint.Comments.Count() - 1)
                        Console.WriteLine($"{indentation}}}");
                    else
                        Console.WriteLine($"{indentation}}},");
                }
                Console.WriteLine("]");
            }
            else
            {
                Console.WriteLine("Comments: this complaint has no comments yet.");
            }
        }
    }



    //private async Task UpdateComplaintStatus()
    //{
    //    Console.Clear();
    //    Console.WriteLine("Enter the id of the complaint you wish to update.");
    //    Guid complaintId;
    //    int statusTypeId;

    //    try
    //    {
    //        complaintId = Guid.Parse(Console.ReadLine()!);
    //    }
    //    catch
    //    {
    //        Console.WriteLine("Not a valid complaint id, press enter to try again.");
    //        Console.ReadLine();
    //        return;
    //    }

    //    Console.Clear();

    //    Console.WriteLine("Pick the new status type");
    //    Console.WriteLine("1. Not started");
    //    Console.WriteLine("2. Under investigation");
    //    Console.WriteLine("3. Closed\n");

    //    var input = Console.ReadKey(true);

    //    switch (input.KeyChar)
    //    {
    //        case '1':
    //            statusTypeId = 1;
    //            break;
    //        case '2':
    //            statusTypeId = 2;
    //            break;
    //        case '3':
    //            statusTypeId = 3;
    //            break;
    //        default:
    //            Console.Clear();
    //            Console.WriteLine("Not a valid status type, press enter to try again.");
    //            Console.ReadLine();
    //            return;
    //    }

    //    Console.Clear();
    //    Console.WriteLine("Loading...");

    //    try
    //    {
    //        await _complaintService.ChangeStatusAsync(complaintId, statusTypeId);
    //        var complaint = await _complaintService.GetAsync(x => x.Id == complaintId);
    //        Console.Clear();
    //        PrintDetailedComplaint(complaint);
    //    }
    //    catch (ArgumentException ex)
    //    {
    //        Console.Clear();
    //        Console.WriteLine(ex.Message + " Press enter to try again.");
    //    }
    //    catch
    //    {
    //        Console.Clear();
    //        Console.WriteLine("Something went wrong, press enter to try again.");
    //    }

    //    Console.ReadLine();
    //}

    //private async Task DeleteComplaint()
    //{
    //    Guid complaintId;
    //    Console.Clear();
    //    Console.WriteLine("Enter the id of the complaint you wish to delete or press enter to go back.");

    //    var input = Console.ReadLine();

    //    if (String.IsNullOrWhiteSpace(input))
    //    {
    //        return;
    //    }

    //    try
    //    {
    //        complaintId = Guid.Parse(input);
    //    }
    //    catch
    //    {
    //        Console.WriteLine("Not a valid complaint id, press enter to try again.");
    //        Console.ReadLine();
    //        return;
    //    }

    //    Console.Clear();
    //    Console.WriteLine("Loading...");

    //    try
    //    {
    //        await _complaintService.DeleteAsync(complaintId);
    //        Console.Clear();
    //        Console.WriteLine("Succesfully deleted complaint. Press enter to return.");
    //    }
    //    catch (ArgumentException ex)
    //    {
    //        Console.Clear();
    //        Console.WriteLine(ex.Message + " Press enter to try again.");
    //    }
    //    catch
    //    {
    //        Console.Clear();
    //        Console.WriteLine("Something went wrong, press enter to try again.");
    //    }

    //    Console.ReadLine();
    //}

    //private async Task CreateComplaint()
    //{
    //    while (true)
    //    {
    //        var complaint = new ComplaintEntity();
    //        Console.Clear();
    //        Console.WriteLine("Please fill in the form to create a complaint.");
    //        Console.WriteLine("Title: ");
    //        complaint.Title = Console.ReadLine()!;
    //        Console.WriteLine("Description: ");
    //        complaint.Description = Console.ReadLine()!;

    //        Console.WriteLine("Product Id: ");
    //        try
    //        {
    //            complaint.ProductId = Convert.ToInt32(Console.ReadLine());
    //        }
    //        catch
    //        {
    //            Console.WriteLine("Not a valid product id, press enter to try again.");
    //            Console.ReadLine();
    //            continue;
    //        }

    //        Console.WriteLine("Customer Id: ");
    //        try
    //        {
    //            complaint.CustomerId = Guid.Parse(Console.ReadLine()!);
    //        }
    //        catch
    //        {
    //            Console.WriteLine("Not a valid customer id, press enter to try again.");
    //            Console.ReadLine();
    //            continue;
    //        }

    //        Console.Clear();

    //        try
    //        {
    //            Console.WriteLine("Loading...");
    //            complaint = await _complaintService.SaveAsync(complaint);
    //            var detailedComplaint = await _complaintService.GetAsync(x => x.Id == complaint.Id);
    //            Console.Clear();
    //            Console.WriteLine("Successfully added complaint: \n");
    //            PrintDetailedComplaint(detailedComplaint);
    //        }
    //        catch (ArgumentException ex)
    //        {
    //            Console.Clear();
    //            Console.WriteLine(ex.Message + "\n\nPress enter to go back.");
    //        }
    //        catch
    //        {
    //            Console.Clear();
    //            Console.WriteLine("Something went wrong when trying to save your complaint. Make sure that you entered valid information and try again.");
    //            Console.WriteLine("\nPress enter to go back.");
    //        }

    //        Console.ReadLine();
    //        break;
    //    }
    //}

    //private async Task DisplaySpecificComplain()
    //{
    //    bool isRunning = true;

    //    while (isRunning)
    //    {
    //        Console.Clear();
    //        Console.WriteLine("Please select an option: \n");
    //        Console.WriteLine("1. Get By Id");
    //        Console.WriteLine("2. Get By Title");
    //        Console.WriteLine("3. Go Back\n");

    //        var input = Console.ReadKey(true);

    //        switch (input.KeyChar)
    //        {
    //            case '1':
    //                await GetComplaintById();
    //                break;
    //            case '2':
    //                await GetComplaintByTitle();
    //                break;
    //            case '3':
    //                isRunning = false;
    //                break;
    //            default:
    //                Console.WriteLine("Invalid option, press enter to try again.");
    //                Console.ReadLine();
    //                break;
    //        }
    //    }
    //}

    //private async Task GetComplaintById()
    //{
    //    Console.Clear();
    //    Console.WriteLine("Enter the Id of the complaint you wish to view.");
    //    var id = Console.ReadLine();
    //    ComplaintEntity complaint = null!;

    //    Console.Clear();
    //    Console.WriteLine("Loading...");

    //    try
    //    {
    //        complaint = await _complaintService.GetAsync(x => x.Id == Guid.Parse(id!));
    //    }
    //    catch (InvalidOperationException)
    //    {
    //        Console.Clear();
    //        Console.WriteLine("Invalid Id format. Press enter to try again.");
    //        Console.ReadLine();
    //        return;
    //    }

    //    Console.Clear();
    //    PrintDetailedComplaint(complaint);

    //    Console.ReadLine();
    //}

    //private async Task GetComplaintByTitle()
    //{
    //    Console.Clear();
    //    Console.WriteLine("Enter the Title of the complaint you wish to view.");
    //    var title = Console.ReadLine();

    //    Console.Clear();
    //    Console.WriteLine("Loading...");

    //    var complaint = await _complaintService.GetAsync(x => x.Title == title);

    //    Console.Clear();
    //    PrintDetailedComplaint(complaint);

    //    Console.ReadLine();
    //}
}
