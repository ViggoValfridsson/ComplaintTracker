using Data_Storage_Submission.Models;

namespace Data_Storage_Submission.Services.Menus;

internal class MainMenuService
{
    private readonly ComplaintService _complaintService = new();
    
    public async Task DisplayMainMenu()
    {
        var isRunning = true;

        while (isRunning)
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
    }
}
