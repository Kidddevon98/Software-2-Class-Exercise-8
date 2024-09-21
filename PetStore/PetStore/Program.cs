using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PetStore;
using PetStore.Logic;
using PetStore.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.Results;
using PetStore.Validators;


var services = CreateServiceCollection();
var productLogic = services.GetService<IProductLogic>();

string userInput = DisplayMenuAndGetInput();

while (userInput?.ToLower() != "exit")
{
    if (userInput == "1")
    {
        Console.WriteLine("Enter the product details as JSON:");

        string jsonInput = Console.ReadLine();

        try
        {
            var dogLeash = JsonSerializer.Deserialize<DogLeash>(jsonInput);

            if (dogLeash != null)
            {
                // Validate the input using FluentValidation
                DogLeashValidator validator = new DogLeashValidator();
                ValidationResult results = validator.Validate(dogLeash);

                if (results.IsValid)
                {
                    productLogic?.AddProduct(dogLeash);
                    Console.WriteLine("Added a dog leash");
                }
                else
                {
                    foreach (var failure in results.Errors)
                    {
                        Console.WriteLine($"Property {failure.PropertyName} failed validation. Error: {failure.ErrorMessage}");
                    }
                }
            }
        }
        catch (JsonException ex)
        {
            Console.WriteLine("Failed to deserialize JSON input. Please make sure the format is correct.");
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    if (userInput == "2")
    {
        Console.Write("What is the name of the product you would like to view? ");
        var productName = Console.ReadLine();

        // Generic method to get any product, e.g., DogLeash
        var dogLeash = productLogic?.GetProductByName<DogLeash>(productName ?? "Unknown");

        Console.WriteLine(JsonSerializer.Serialize(dogLeash));
        Console.WriteLine();
    }
    if (userInput == "3")
    {
        Console.WriteLine("The following products are in stock: ");
        var inStock = productLogic?.GetOnlyInStockProducts();
        foreach (var item in inStock ?? new List<string>())
        {
            Console.WriteLine(item);
        }
        Console.WriteLine();
    }
    if (userInput == "4")
    {
        Console.WriteLine($"The total price of inventory on hand is {productLogic?.GetTotalPriceOfInventory():C}");
        Console.WriteLine();
    }

    userInput = DisplayMenuAndGetInput();
}

static string DisplayMenuAndGetInput()
{
    Console.WriteLine("Press 1 to add a product (input as JSON)");
    Console.WriteLine("Press 2 to view a product by name");
    Console.WriteLine("Press 3 to view in-stock products");
    Console.WriteLine("Press 4 to view the total price of current inventory");
    Console.WriteLine("Type 'exit' to quit");

    return Console.ReadLine();
}

static IServiceProvider CreateServiceCollection()
{
    return new ServiceCollection()
        .AddTransient<IProductLogic, ProductLogic>()
        .BuildServiceProvider();
}
