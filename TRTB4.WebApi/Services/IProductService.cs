using TRTB4.WebApi.Controllers;

namespace TRTB4.WebApi.Services
{
    public interface IProductService
    {
        Task<ProductCreateResponseDto> CreateProductAsync(ProductCreateRequestDto requestDto);
        Task<ProductDeleteResponseDto> DeleteProduct(int id);
        Task<ProductGetResponseDto> GetProductAsync(int id);
        Task<ProductGetListResponseDto> GetProductsAsync(int pageNo, int pageSize);
        Task<ProductUpdateResponseDto> PatchProduct(int id, ProductPatchRequestDto requestDto);
        Task<ProductUpdateResponseDto> UpdateProductAsync(int id, ProductUpdateRequestDto requestDto);
    }
}