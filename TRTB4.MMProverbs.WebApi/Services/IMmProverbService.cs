
namespace TRTB4.MMProverbs.WebApi.Services
{
    public interface IMmProverbService
    {
        Task<MmProverbTitleResponseModel> GetAllPilesAsync();
        Task<MmProverbsResponseModel> GetPileByIdAsync(int id);
    }
}