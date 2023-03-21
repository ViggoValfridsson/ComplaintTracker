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
            Console.WriteLine("2. Customers");
            Console.WriteLine("3. Comments");
            Console.WriteLine("4. Address"); //Behövs inte en egen meny för. Skapa addressen från customer
            Console.WriteLine("5. Departments"); //Behövs inte en meny för
            Console.WriteLine("6. Employees");
            Console.WriteLine("7. Products");
            Console.WriteLine("8. Status Types");  //Behövs inte en meny för
            Console.WriteLine("9. Quit\n");

            var input = Console.ReadKey(true);

            switch (input.KeyChar)
            {
                case '1':
                    var complaintMenuService = new ComplaintsMenuService();
                    await complaintMenuService.DisplayOptionsMenu();
                    break;
                case '2':
                    break;
                case '3':
                    break;
                case '4':
                    break;
                case '5':
                    break;
                case '6':
                    break;
                case '7':
                    break;
                case '8':
                    break;
                case '9':
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
