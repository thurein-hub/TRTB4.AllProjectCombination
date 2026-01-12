using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TRTB4.WebApi.Database.AppDbContextModels;
using TRTB4.WebApi.Services;

namespace TRTB4.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }


    [HttpGet("{pageNo}/{pageSize}")]
    public async Task<IActionResult> GetProducts(int pageNo, int pageSize)
    {
        var result = await _productService.GetProductsAsync(pageNo, pageSize);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductAsync(int id)
    {
        var result = await _productService.GetProductAsync(id);
        if (!result.IsSuccess)
        {
            return NotFound(result);
        }
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProductAsync(ProductCreateRequestDto requestDto)
    {
        var result = await _productService.CreateProductAsync(requestDto);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProductAsync(int id, ProductUpdateRequestDto requestDto)
    {
        var result = await _productService.UpdateProductAsync(id, requestDto);
        if (!result.IsSuccess)
        {
            return NotFound(result);
        }
        return Ok(result);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchProductAsync(int id, ProductPatchRequestDto requestDto)
    {
        var result = await _productService.PatchProduct(id, requestDto);
        if (!result.IsSuccess)
        {
            return NotFound(result);
        }
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductAsync(int id)
    {
        var result = await _productService.DeleteProduct(id);
        if (!result.IsSuccess)
        {
            return NotFound(result);
        }
        return Ok(result);
    }

    
}


//Request and Response Models
public class ProductGetListResponseDto
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public List<TblProduct> Products { get; set; } = null!;

}

public class ProductGetResponseDto
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public DateTime? ModifiedDateTime { get; set; }
}

public class ProductCreateRequestDto
{
    public string ProductName { get; set; } = null!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}

public class ProductCreateResponseDto
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
}

public class ProductUpdateRequestDto
{
    public string ProductName { get; set; } = null!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}

public class ProductUpdateResponseDto
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public string ProductName { get; set; } = null!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public DateTime ModifiedDateTime { get; set; }
}

public class ProductPatchRequestDto
{
    public string? ProductName { get; set; }
    public decimal? Price { get; set; }
    public int? Quantity { get; set; }
}

public class ProductDeleteResponseDto
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
}


