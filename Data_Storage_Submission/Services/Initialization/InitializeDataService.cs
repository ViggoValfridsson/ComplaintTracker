using Data_Storage_Submission.Context;
using Data_Storage_Submission.Models.Entities;

namespace Data_Storage_Submission.Services.Initialization;

internal class InitializeDataService
{
    private DataContext _context = new();
    private AddressService _addressService = new();

    public async Task InitializeAll()
    {
        Console.WriteLine("Loading...");
        await InitializeAddresses();
        await InitializeDepartments();
    }

    private async Task InitializeAddresses()
    {
        if (!_context.Addresses.Any())
        {
            var addresses = new List<AddressEntity>()
            {
                new AddressEntity
                {
                    Street = "Storgatan 24",
                    PostalCode = "12345",
                    City = "Stockholm"
                },
                new AddressEntity
                {
                    Street = "Kungsgatan 14",
                    PostalCode = "45678",
                    City = "Göteborg"
                },
                new AddressEntity
                {
                    Street = "Dorttninggatan 8",
                    PostalCode = "78901",
                    City = "Malmö"
                },
            };

            foreach(var address in addresses)
            {
                await _addressService.SaveAsync(address);
            }
        }
    }

    private async Task InitializeDepartments ()
    {

    }
}
