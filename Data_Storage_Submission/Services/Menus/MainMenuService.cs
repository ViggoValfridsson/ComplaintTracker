namespace Data_Storage_Submission.Services.Menus;

internal class MainMenuService
{
    public async Task DisplayMainMenu()
    {
        var isRunning = true;

        while (isRunning)
        {
            Console.Clear();
            Console.WriteLine("Please select an option:\n");
            Console.WriteLine("1. Complaints");
            Console.WriteLine("2. Comments");
            Console.WriteLine("3. Quit\n");

            var input = Console.ReadKey(true);

            switch (input.KeyChar)
            {
                case '1':
                    var complaintMenuService = new ComplaintsMenuService();
                    await complaintMenuService.DisplayOptionsMenu();
                    break;
                case '2':
                    var commentMenuService = new CommentsMenuService();
                    await commentMenuService.DisplayOptionsMenu();
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
}
