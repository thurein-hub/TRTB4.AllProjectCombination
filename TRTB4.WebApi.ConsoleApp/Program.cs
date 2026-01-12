// See https://aka.ms/new-console-template for more information
using TRTB4.WebApi.ConsoleApp;

Console.WriteLine("Hello, World!");

Console.WriteLine("Waiting for Web API...");
Console.ReadLine();

ProductApiService productApiService = new ProductApiService();
Start:
Console.WriteLine("----------------- ***** -------------------------");
Console.WriteLine("Please select option:");
Console.WriteLine("1. Get All Products");
Console.WriteLine("2. Create Product");
Console.WriteLine("3. Update Product");
Console.WriteLine("4. Patch Product");
Console.WriteLine("5. Delete Product");
Console.WriteLine("6. Exit");
Console.WriteLine("------------------ ***** -----------------------");
Console.Write("Enter your choice: ");

string? choice = Console.ReadLine();

switch (choice)
{
    case "1":
        await productApiService.ReadAsync();
        Console.ReadLine();
        goto Start;
    case "2":
        await productApiService.CreateAsync();
        Console.ReadLine();
        goto Start;
    case "3":
        await productApiService.UpdateAsync();
        Console.ReadLine();
        goto Start;
    case "4":
        await productApiService.PatchAsync();
        Console.ReadLine();
        goto Start;
    case "5":
        await productApiService.DeleteAsync();
        Console.ReadLine();
        goto Start;
    case "6":
        Console.WriteLine("Exit from the application...");
        break;
    default:
        Console.WriteLine("Invalid!");
        break;

}
public class ProductCreateRequestDto
{
    public string ProductName { get; set; } = null!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}

public class ProductUpdateRequestDto
{
    public string ProductName { get; set; } = null!;

    public decimal Price { get; set; }

    public int Quantity { get; set; }
}

public class ProductPatchRequestDto
{
    public string? ProductName { get; set; } = null!;

    public decimal? Price { get; set; }

    public int? Quantity { get; set; }
}
