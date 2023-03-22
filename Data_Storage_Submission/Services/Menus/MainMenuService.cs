using Data_Storage_Submission.Models;
using Data_Storage_Submission.Models.Entities;

namespace Data_Storage_Submission.Services.Menus;

internal class MainMenuService
{
    private readonly ComplaintsMenuService _complaintsMenuService = new();

    public async Task DisplayMainMenu()
    {
        bool inMainMenu = true;

        while(inMainMenu)
        {
            Console.Clear();
            Console.WriteLine("Commands:\n");
            Console.WriteLine("complaints".PadRight(25) + "View and modify complaints");
            Console.WriteLine("exit".PadRight(25) + "Close application");

            var input = Console.ReadLine();

            switch (input!.ToLower())
            {
                case "complaints": 
                    await _complaintsMenuService.DisplayComplaintsMenu();
                    break;
                case "exit":
                    inMainMenu = false;
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Not a valid command.");
                    Console.ReadLine();
                    break;
            }
        }
    }


}
