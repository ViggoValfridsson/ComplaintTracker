using Data_Storage_Submission.Services.Initialization;
using Data_Storage_Submission.Services.Menus;

var initializeDataService = new InitializeDataService();
var mainMenu = new MainMenuService();

await initializeDataService.InitializeAll();
await mainMenu.DisplayMainMenu();