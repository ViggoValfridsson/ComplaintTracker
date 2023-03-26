namespace Data_Storage_Submission.Services.Menus;

internal class MainMenuService
{
    private readonly ComplaintsMenuService _complaintsMenuService = new();
    private readonly ProductsMenuService _productsMenuService = new();
    private readonly EmployeeMenuService _employeeMenuService = new();

    public async Task DisplayMainMenu()
    {
        bool inMainMenu = true;

        while (inMainMenu)
        {
            Console.Clear();
            Console.WriteLine("Commands:\n");
            Console.WriteLine("complaints".PadRight(25) + "View and modify complaints");
            Console.WriteLine("products".PadRight(25) + "View, create or delete products");
            Console.WriteLine("employees".PadRight(25) + "View, create or delete products");
            Console.WriteLine("exit".PadRight(25) + "Close application");
            Console.WriteLine();

            var input = Console.ReadLine()!.ToLower();

            switch (input)
            {
                case "complaints":
                    await _complaintsMenuService.DisplayComplaintsMenu();
                    break;
                case "products":
                    await _productsMenuService.DisplayProductsMenu();
                    break;
                case "employees":
                    await  _employeeMenuService.DisplayEmployeeMenu();
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
