﻿using Data_Storage_Submission.Models;
using Data_Storage_Submission.Models.Entities;

namespace Data_Storage_Submission.Services.Menus;

internal class ProductsMenuService
{
    private readonly ProductService _productService = new();

    public async Task DisplayProductsMenu()
    {
        bool inProductsMenu = true;

        while (inProductsMenu)
        {
            Console.Clear();
            Console.WriteLine("Loading...");

            var products = await _productService.GetAllAsync();

            Console.Clear();

            if (products.Count() < 1)
            {
                Console.WriteLine("No products found.");
            }
            else
            {
                var displayTableService = new DisplayTableService<ProductSummaryModel>();
                var productSummaries = new List<ProductSummaryModel>();

                // Convert ProductEntites to ProductSummaryModels to prevent table from becoming to wide
                foreach (var product in products)
                {
                    var productsSummary = new ProductSummaryModel(product);
                    productSummaries.Add(productsSummary);
                }

                displayTableService.DisplayTable(productSummaries);
            }

            Console.WriteLine("\nCommands:");
            Console.WriteLine("delete <#>".PadRight(25) + "Delete specific product. <#> = row number");
            Console.WriteLine("new".PadRight(25) + "Add new product");
            Console.WriteLine("exit".PadRight(25) + "Return to main menu");
            Console.WriteLine();

            var input = Console.ReadLine()!.ToLower();

            if (input!.Contains("delete"))
            {
                var rowNumber = new string(input.Where(c => char.IsDigit(c)).ToArray());
                ProductEntity choosenProduct;

                Console.Clear();

                try
                {
                    // Uses input that has been stripped of non-numeric characters to index the collection
                    int productId = (products!.ToList()[Convert.ToInt32(rowNumber) - 1]).Id;
                    choosenProduct = await _productService.GetAsync(x => x.Id == productId);
                }
                catch
                {
                    Console.WriteLine("Not a valid row number, press enter to try again.");
                    Console.ReadLine();
                    continue;
                }

                try
                {
                    await _productService.DeleteAsync(choosenProduct);
                    Console.WriteLine($"Successfully deleted product on row {rowNumber}, press enter to go back.");
                    Console.ReadLine();
                }
                catch
                {
                    Console.WriteLine("Something went wrong when deleting, please try again.");
                    Console.ReadLine();
                }

            }
            else
            {
                switch (input)
                {
                    case "new":
                        await DisplayProductCreation();
                        break;
                    case "exit":
                        inProductsMenu = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Not a valid command, press enter to try again");
                        Console.ReadLine();
                        break;
                }
            }
        }
    }

    private async Task DisplayProductCreation()
    {
        var product = new ProductEntity();
        Console.Clear();
        Console.WriteLine("Please fill in the form to add a product.");
        Console.WriteLine("Product name:");
        product.Name = Console.ReadLine()!;
        Console.WriteLine("Description");
        product.Description = Console.ReadLine()!;
        Console.WriteLine("Manufacturer: ");
        product.Manufacturer = Console.ReadLine()!;

        try
        {
            Console.Clear();
            Console.WriteLine("Loading...");
            await _productService.SaveAsync(product);
            Console.Clear();
            Console.WriteLine("Successfully added product!");
        }
        catch (ArgumentException ex)
        {
            // Catch for inputing already existing item
            Console.Clear();
            Console.WriteLine(ex.Message + "\nPress enter to go back.");
        }
        catch
        {
            Console.Clear();
            Console.WriteLine("Something went wrong when trying to save the product. Make sure that you entered valid information and try again.");
            Console.WriteLine("Press enter to go back.");
        }

        Console.ReadLine();
    }
}
