﻿using Data_Storage_Submission.Models;
using Data_Storage_Submission.Models.Entities;
using System.Data.Common;

namespace Data_Storage_Submission.Services.Menus;

internal class ComplaintsMenuService
{
    private readonly ComplaintService _complaintService = new();
    private readonly CommentsMenuService _commentMenuService = new();
    private readonly ProductService _productService = new();
    private readonly CustomerService _customerService = new();
    private readonly CustomerMenuService _customerMenu = new();

    public async Task DisplayComplaintsMenu()
    {
        var inComplaintsMenu = true;

        while (inComplaintsMenu)
        {
            Console.Clear();
            Console.WriteLine("Loading...");

            var complaints = await _complaintService.GetAllAsync();

            Console.Clear();

            if (complaints.Count() < 1)
            {
                Console.WriteLine("No complaints found.");
            }
            else
            {
                // Converts to ComplaintSummaryModels to keep the table from becoming to wide and filled with unnecessary information
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
            Console.WriteLine("delete <#>".PadRight(25) + "Delete specific complaint. <#> = row number");
            Console.WriteLine("new".PadRight(25) + "Add new complaint");
            Console.WriteLine("exit".PadRight(25) + "Return to main menu");
            Console.WriteLine();

            var input = Console.ReadLine()!.ToLower();

            if (input!.Contains("open") || input!.Contains("delete"))
            {
                // Strips string of non-numeric characters
                var rowNumber = new string(input.Where(c => char.IsDigit(c)).ToArray());
                ComplaintEntity choosenComplaint;

                Console.Clear();

                try
                {
                    // Gets the complaint id by indexing the list using the numeric string. The subtraction of 1 is because collections start at 0.
                    Guid complaintId = (complaints!.ToList()[Convert.ToInt32(rowNumber) - 1]).Id;
                    choosenComplaint = await _complaintService.GetAsync(x => x.Id == complaintId);
                }
                catch
                {
                    Console.WriteLine("Not a valid row number, press enter to try again.");
                    Console.ReadLine();
                    continue;
                }

                if (input!.Contains("open"))
                {
                    await DisplayComplaintOptions(choosenComplaint);
                }
                else
                {
                    try
                    {
                        await _complaintService.DeleteAsync(choosenComplaint.Id);
                        Console.WriteLine($"Successfully deleted complaint on row {rowNumber}, press enter to go back.");
                        Console.ReadLine();
                    }
                    catch
                    {
                        Console.WriteLine("Something went wrong when deleting, please try again.");
                        Console.ReadLine();
                    }
                }
            }
            else
            {
                switch (input)
                {
                    case "new":
                        await DisplayComplaintCreateation();
                        break;
                    case "exit":
                        inComplaintsMenu = false;
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
    private async Task DisplayComplaintOptions(ComplaintEntity complaint)
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
            Console.WriteLine();

            var input = Console.ReadLine()!.ToLower();

            switch (input)
            {
                case "status 1":
                case "status 2":
                case "status 3":
                    //Strips the input of non-numeric characters and uses the id as an argument when updating status.
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
                    Console.Clear();
                    Console.WriteLine("Not a valid command, press enter to try again");
                    Console.ReadLine();
                    break;
            }
        }
    }

    private async Task DisplayComplaintCreateation()
    {
        var complaint = new ComplaintEntity();

        Console.Clear();
        Console.WriteLine("Please fill in the form to create a complaint.");
        Console.WriteLine("Title: ");
        complaint.Title = Console.ReadLine()!;
        Console.WriteLine("Description: ");
        complaint.Description = Console.ReadLine()!;

        try
        {
            complaint.ProductId = await ChooseExistingProduct();
        }
        catch (Exception ex)
        {
            Console.Clear();
            Console.WriteLine(ex.Message);
            Console.ReadLine();
            return;
        }

        Console.Clear();
        Console.WriteLine("Do you wish to connect the complaint to an existing customer or a new one?");
        Console.WriteLine("\nCommands:");
        Console.WriteLine("existing".PadRight(25) + "Pick an already existing customer.");
        Console.WriteLine("new".PadRight(25) + "Create a new customer and connect complaint to them.");
        Console.WriteLine();
        var customerType = Console.ReadLine()!.ToLower();

        switch (customerType)
        {
            case "existing":
                try
                {
                    complaint.CustomerId = await ChooseExistingCustomer();
                }
                catch (ArgumentException ex)
                {
                    // Catches duplicate entries
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                    Console.ReadLine();
                    return;
                }
                catch
                {
                    Console.Clear();
                    Console.WriteLine("Make sure that there are customers in the database and try again.");
                    Console.ReadLine();
                    return;
                }
                break;
            case "new":
                try
                {
                    complaint.CustomerId = await _customerMenu.CreateCustomer();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + "\nPress enter to go back.");
                    Console.ReadLine();
                    return;
                }
                break;
            default:
                Console.Clear();
                Console.WriteLine("Not a valid command, press enter to try again");
                Console.ReadLine();
                return;
        }

        try
        {
            Console.Clear();
            Console.WriteLine("Loading...");
            await _complaintService.SaveAsync(complaint);
            Console.Clear();
            Console.WriteLine("Successfully added complaint");
        }
        catch (ArgumentException ex)
        {
            Console.Clear();
            Console.WriteLine(ex.Message + "\nPress enter to go back.");
        }
        catch
        {
            Console.Clear();
            Console.WriteLine("Something went wrong when trying to save your comment. Make sure that you entered valid information and try again.");
            Console.WriteLine("Press enter to go back.");
        }

        Console.ReadLine();
    }

    private async Task<int> ChooseExistingProduct()
    {
        var products = await _productService.GetAllAsync();

        if (products.Count() < 1)
        {
            throw new Exception("No products found. Press enter to return.");
        }

        var productTableService = new DisplayTableService<ProductSummaryModel>();
        var productSummaries = new List<ProductSummaryModel>();

        // Converts ProductEntities to ProductSummaryModels to make sure table isn't to wide and filled with unnecessary information.
        foreach (var product in products)
        {
            var productSummary = new ProductSummaryModel(product);
            productSummaries.Add(productSummary);
        }

        Console.Clear();

        productTableService.DisplayTable(productSummaries);

        Console.WriteLine("\nChoose the product your complaint is refering to. For example write \"1\" for row one.");
        try
        {
            var input = Console.ReadLine();
            // Strips input of non-numeric characters and uses it to index products collection.
            var productRow = new string(input!.Where(c => char.IsDigit(c)).ToArray());
            var product = products.ToList()[Convert.ToInt32(productRow) - 1];
            return product.Id;
        }
        catch
        {
            throw new ArgumentException("Not a valid product");
        }
    }

    private async Task<Guid> ChooseExistingCustomer()
    {
        var customers = await _customerService.GetAllAsync();

        if (customers.Count() < 1)
        {
            throw new Exception("No customers found. Press enter to return.");
        }

        var customerTableService = new DisplayTableService<CustomerSummaryModel>();
        var customerSummaries = new List<CustomerSummaryModel>();

        // Converts CustomerEntities to CustomerSummaryModels to make sure table isn't to wide.
        foreach (var customer in customers)
        {
            var customerSummary = new CustomerSummaryModel(customer);
            customerSummaries.Add(customerSummary);
        }

        Console.Clear();
        customerTableService.DisplayTable(customerSummaries);

        Console.WriteLine("\nChoose the product your complaint is refering to. For example write \"1\" for row one.");
        try
        {
            var input = Console.ReadLine();
            // Strips the input of non-numeric characters. This gets us the rownumber of the choosen customer. The row number is the same as the index + 1.
            var customerRow = new string(input!.Where(c => char.IsDigit(c)).ToArray());
            var customer = customers.ToList()[Convert.ToInt32(customerRow) - 1];
            return customer.Id;
        }
        catch
        {
            throw new ArgumentException("Not a valid product");
        }
    }

    private void PrintDetailedComplaint(ComplaintEntity complaint)
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

            // Displays the comments in the style of a JSON array
            if (complaint.Comments.Count > 0)
            {
                var indentation = new string(' ', 4);
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
}
