
namespace TRTB4.MMProverbs.WebApi.Services
{
    public interface IMmProverbService
    {
        Task<MmProverbTitleResponseModel> GetAllTitleAsync();
        Task<MmProverbsResponseModel> GetProverbsByIdAsync(int id);
        Task<SearchMmProverbsResponseModel> SearchProverbsAsync(string searchkeyword);
    }
}