using Data_Storage_Submission.Services.Initialization;
using Data_Storage_Submission.Services.Menus;

var initializeDataService = new InitializeDataService();
var mainMenu = new MainMenuService();

// If you do not wish do use dummy data change this line to "await initializeDataService.InitializeStatusTypes();"
await initializeDataService.InitializeAll();

await mainMenu.DisplayMainMenu();