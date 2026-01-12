using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace TRTB4.WebApi.ConsoleApp;

internal class ProductApiService
{
    private readonly string domainUrl;
    private readonly string productEndpoint;

    public ProductApiService()
    {
        domainUrl = "https://localhost:7188";

        productEndpoint = $"{domainUrl}/api/product";

    }
    public async Task ReadAsync()
    {
        Console.Write("Enter page number:");
        string? pageNoInput = Console.ReadLine() ?? "1";
        Console.Write("Enter page size:");
        string? pageSizeInput = Console.ReadLine() ?? "10";
        Console.WriteLine($"{productEndpoint}/{pageNoInput}/{pageSizeInput}");


        //HttpClient httpClient = new HttpClient();
        //HttpResponseMessage respone = await httpClient.GetAsync($"{productEndpoint}/{pageNoInput}/{pageSizeInput}");

        //if (respone.IsSuccessStatusCode)
        //{
        //    string jsonData = await respone.Content.ReadAsStringAsync();
        //    Console.WriteLine(jsonData);
        //}


        string endpoint = $"{productEndpoint}/{pageNoInput}/{pageSizeInput}";
        RestClient client = new RestClient();
        RestRequest request = new RestRequest(endpoint);
        RestResponse response = await client.ExecuteAsync(request);
        //RestResponse response = await client.GetAsync(request);
        if (response.IsSuccessStatusCode)
        {
            string jsonData = response.Content!;
            Console.WriteLine(jsonData);
        }

        Console.ReadLine();

    }

    public async Task CreateAsync()
    {
        Console.Write("Enter Product Name : ");
        string name = Console.ReadLine();
        Console.Write("Enter Price : ");
        decimal price = Convert.ToDecimal(Console.ReadLine());
        Console.Write("Enter Quantity :");
        int quantity = Convert.ToInt32(Console.ReadLine());

        //ProductCreateRequestModel requestModel = new ProductCreateRequestModel
        //{
        //    ProductName = name,
        //    Price = price,
        //    Quantity = quantity,
        //};

        ProductCreateRequestDto requestDto = new ProductCreateRequestDto
        {
            ProductName = name,
            Price = price,
            Quantity = quantity,
        };

      
        //string json = JsonConvert.SerializeObject(requestDto);
        //var content = new StringContent(json, Encoding.UTF8, Application.Json);

        //HttpClient httpClient = new HttpClient();
        //HttpResponseMessage respone = await httpClient.PostAsync(productEndpoint, content);

        //if (respone.IsSuccessStatusCode)
        //{
        //    string jsonData = await respone.Content.ReadAsStringAsync();
        //    Console.WriteLine(jsonData);
        //}

        string endpoint = productEndpoint;
        RestClient client = new RestClient();
        RestRequest request = new RestRequest(endpoint, Method.Post);
        request.AddJsonBody(requestDto);
        //RestResponse response = await client.PostAsync(request);
        RestResponse response = await client.ExecuteAsync(request);
        if (response.IsSuccessStatusCode)
        {
            string jsonData = response.Content!;
            Console.WriteLine(jsonData);

        }

        Console.ReadLine();

    }

    public async Task UpdateAsync()
    {
        Console.Write("Enter product id: ");
        int id = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter product name: ");
        string name = Console.ReadLine();

        Console.Write("Enter product price: ");
        decimal price = Convert.ToDecimal(Console.ReadLine());

        Console.Write("Enter product quantity:");
        int quantity = Convert.ToInt32(Console.ReadLine());

        ProductUpdateRequestDto requestDto = new ProductUpdateRequestDto
        {
            ProductName = name,
            Price = price,
            Quantity = quantity,

        };
        //string json = JsonConvert.SerializeObject(requestDto);
        //var content = new StringContent(json, Encoding.UTF8, Application.Json);

        //HttpClient httpClient = new HttpClient();
        //HttpResponseMessage respone = await httpClient.PutAsync($"{productEndpoint}/{id}", content);

        //if (respone.IsSuccessStatusCode)
        //{
        //	string data = await respone.Content.ReadAsStringAsync();
        //	Console.WriteLine("Update Success: " + data);
        //}
        //else
        //{
        //	Console.WriteLine("Update Failed: " + respone.StatusCode);
        //}
        string endpoint = $"{productEndpoint}/{id}";
        RestClient client = new RestClient();
        RestRequest request = new RestRequest(endpoint, Method.Put);
        request.AddJsonBody(requestDto);
        RestResponse response = await client.ExecuteAsync(request);
        if (response.IsSuccessStatusCode)
        {
            string jsonData = response.Content!;
            Console.WriteLine(jsonData);
        }
    }

    public async Task PatchAsync()
    {
        Console.Write("Enter  product id to patch: ");
        int patchId = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter new product name: ");
        string? patchNewName = Console.ReadLine();
        string? patchName = string.IsNullOrEmpty(patchNewName) ? null : patchNewName;

        Console.Write("Enter new price: ");
        string? patchNewPrice = Console.ReadLine();
        decimal? patchPrice = string.IsNullOrEmpty(patchNewPrice) ? null : Convert.ToDecimal(patchNewPrice);

        Console.Write("Enter new quantity: ");
        string? patchNewQty = Console.ReadLine();
        int? patchQuantity = string.IsNullOrEmpty(patchNewQty) ? null : Convert.ToInt32(patchNewQty);

        ProductPatchRequestDto patchRequestModel = new ProductPatchRequestDto
        {
            ProductName = patchName,
            Price = patchPrice,
            Quantity = patchQuantity
        };

        //string jsonPatch = JsonConvert.SerializeObject(patchRequestModel);
        //var contentPatch = new StringContent(jsonPatch, Encoding.UTF8, "application/json");

        //HttpClient clientPatch = new HttpClient();
        //HttpResponseMessage responsePatch = await clientPatch.PatchAsync($"{productEndpoint}/{patchId}", contentPatch);

        //if (responsePatch.IsSuccessStatusCode)
        //{
        //	string data = await responsePatch.Content.ReadAsStringAsync();
        //	Console.WriteLine("Patch Success: " + data);
        //}
        //else
        //{
        //	Console.WriteLine("Patch Failed: " + responsePatch.StatusCode);
        //}
        string endpoint = $"{productEndpoint}/{patchId}";
        RestClient client = new RestClient();
        RestRequest request = new RestRequest(endpoint, Method.Patch);
        request.AddJsonBody(patchRequestModel);
        RestResponse response = await client.ExecuteAsync(request);
        if (response.IsSuccessStatusCode)
        {
            string jsonData = response.Content!;
            Console.WriteLine(jsonData);
        }
        
    }

    public async Task DeleteAsync()
    {
        Console.Write("Enter product id to delete: ");
        int deleteId = Convert.ToInt32(Console.ReadLine());

        //HttpClient httpClient = new HttpClient();
        //HttpResponseMessage respone = await httpClient.DeleteAsync($"{productEndpoint}/{deleteId}");

        //if (respone.IsSuccessStatusCode)
        //{
        //	string jsonData = await respone.Content.ReadAsStringAsync();
        //	Console.WriteLine(jsonData);
        //}


        string endpoint = $"{productEndpoint}/{deleteId}";
        RestClient client = new RestClient();
        RestRequest request = new RestRequest(endpoint, Method.Delete);
        RestResponse response = await client.ExecuteAsync(request);
        if (response.IsSuccessStatusCode)
        {
            string jsonData = response.Content!;
            Console.WriteLine(jsonData);
        }
    }


}
