using Data_Storage_Submission.Models;
using Data_Storage_Submission.Models.Entities;

namespace Data_Storage_Submission.Services.Menus;

internal class ComplaintsMenuService
{
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
                    break;
                case '3':
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

        var complaintsService = new ComplaintService();
        var complaints = await complaintsService.GetAllAsync();

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
}
