using Microsoft.EntityFrameworkCore;
using TRTB4.WebApi.Controllers;
using TRTB4.WebApi.Database.AppDbContextModels;

namespace TRTB4.WebApi.Services;

public class ProductService : IProductService
{
    private readonly AppDbContext _db;

    public ProductService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<ProductGetListResponseDto> GetProductsAsync(int pageNo, int pageSize)
    {
        if (pageNo <= 0)
        {
            return new ProductGetListResponseDto
            {
                IsSuccess = false,
                Products = new List<TblProduct>(),
                Message = "Invalid page number."
            };
        }
        if (pageSize <= 0)
        {
            return new ProductGetListResponseDto
            {
                IsSuccess = false,
                Products = new List<TblProduct>(),
                Message = "Invalid page size."
            };
        }

        var lst = await _db.TblProducts
            .OrderByDescending(x => x.ProductId)
            .Skip((pageNo - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        var responseDto = new ProductGetListResponseDto
        {
            IsSuccess = true,
            Products = lst,
            Message = "Data found successfully."
        };

        return responseDto;
    }

    public async Task<ProductGetResponseDto> GetProductAsync(int id)
    {
        var item = await _db.TblProducts.FirstOrDefaultAsync(x => x.ProductId == id);
        if (item is null)
        {
            return new ProductGetResponseDto
            {
                IsSuccess = false,
                Message = "No data found."
            };
        }

        var responseDto = new ProductGetResponseDto
        {
            IsSuccess = true,
            Message = "Data found successfully.",
            ProductId = item.ProductId,
            ProductName = item.ProductName,
            Price = item.Price,
            Quantity = item.Quantity,
            CreatedDateTime = item.CreatedDateTime,
            ModifiedDateTime = item.ModifiedDateTime
        };
        return responseDto;
    }

    public async Task<ProductCreateResponseDto> CreateProductAsync(ProductCreateRequestDto requestDto)
    {
        TblProduct product = new TblProduct
        {
            ProductName = requestDto.ProductName,
            Price = requestDto.Price,
            Quantity = requestDto.Quantity,
            CreatedDateTime = DateTime.Now,
            IsDelete = false
        };
        _db.TblProducts.Add(product);
        var result = await _db.SaveChangesAsync();
        var message = result > 0 ? "Data inserted successfully." : "Data insertion failed.";

        var responseDto = new ProductCreateResponseDto
        {
            IsSuccess = result > 0,
            Message = message
        };
        return responseDto;
    }

    public async Task<ProductUpdateResponseDto> UpdateProductAsync(int id, ProductUpdateRequestDto requestDto)
    {
        var item = await _db.TblProducts.FirstOrDefaultAsync(x => x.ProductId == id);
        if (item is null)
        {
            return new ProductUpdateResponseDto
            {
                IsSuccess = false,
                Message = "No data found."
            };
        }

        item.ProductName = requestDto.ProductName;
        item.Price = requestDto.Price;
        item.Quantity = requestDto.Quantity;
        item.ModifiedDateTime = DateTime.Now;

        var result = await _db.SaveChangesAsync();
        var message = result > 0 ? "Data updated successfully." : "Data updation failed.";


        var responseDto = new ProductUpdateResponseDto
        {
            IsSuccess = result > 0,
            Message = message,
            ModifiedDateTime = item.ModifiedDateTime ?? default,
            Price = item.Price,
            ProductName = item.ProductName,
            Quantity = item.Quantity
        };

        return responseDto;
    }

    public async Task<ProductUpdateResponseDto> PatchProduct(int id, ProductPatchRequestDto requestDto)
    {
        var item = _db.TblProducts.FirstOrDefault(x => x.ProductId == id);
        if (item is null)
        {
            return new ProductUpdateResponseDto
            {
                IsSuccess = false,
                Message = "No data found."
            };
        }

        if (requestDto.ProductName is not null)
        {
            item.ProductName = requestDto.ProductName;
        }
        if (requestDto.Price is not null && requestDto.Price > 0)
        {
            item.Price = Convert.ToDecimal(requestDto.Price);
        }
        if (requestDto.Quantity is not null && requestDto.Quantity > 0)
        {
            item.Quantity = Convert.ToInt32(requestDto.Quantity);
        }
        item.ModifiedDateTime = DateTime.Now;

        var result = await _db.SaveChangesAsync();
        var message = result > 0 ? "Data updated successfully." : "Data updation failed.";

        
        var responseDto = new ProductUpdateResponseDto
        {
            IsSuccess = result > 0,
            Message = message,
            ModifiedDateTime = item.ModifiedDateTime ?? default,
            Price = item.Price,
            ProductName = item.ProductName,
            Quantity = item.Quantity
        };
        return responseDto;
    }

    public async Task<ProductDeleteResponseDto> DeleteProduct(int id)
    {
        var item = await _db.TblProducts.FirstOrDefaultAsync(x => x.ProductId == id);
        if (item is null)
        {
            return new ProductDeleteResponseDto
            {
                IsSuccess = false,
                Message = "No data found."
            };
        }
        item.IsDelete = true;
        var result = await _db.SaveChangesAsync();
        var message = result > 0 ? "Data deleted successfully." : "Data deletion failed.";
        var responseDto = new ProductDeleteResponseDto
        {
            IsSuccess = result > 0,
            Message = message
        };
        return responseDto;
    }
}
