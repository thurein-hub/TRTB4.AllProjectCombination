using Dapper;
using System.Data;

namespace TRTB4.MMProverbs.WebApi.Services
{
    public class MmProverbService : IMmProverbService
    {
        private readonly IDbConnection _db;

        public MmProverbService(IDbConnection db)
        {
            _db = db;
        }
        public async Task<MmProverbTitleResponseModel> GetAllTitleAsync()
        {

            string titleSql = "SELECT * FROM Tbl_Mmproverbstitle";
            var title = (await _db.QueryAsync<MmproverbstitleDto>(titleSql)).ToList();


            return new MmProverbTitleResponseModel
            {
                IsSuccess = true,
                Message = "MMProverbs retrieved successfully.",
                Data = title
            };

        }


        public async Task<MmProverbsResponseModel> GetProverbsByIdAsync(int id)
        {

            string proverbSql = @"
            SELECT * FROM Tbl_Mmproverbs
            WHERE TitleId = @TitleId";

            var proverbs = (await _db.QueryAsync<MmproverbsDto>(
                proverbSql,
                new { TitleId = id }
            )).ToList();


            return new MmProverbsResponseModel
            {
                IsSuccess = true,
                Message = "PRoverbs retrieved successfully.",
                Data = proverbs
            };

        }

        public async Task<SearchMmProverbsResponseModel> SearchProverbsAsync(string searchkeyword)
        {
            string searchProverbsql = @"
                        SELECT * FROM Tbl_Mmproverbs
                        WHERE ProverbName LIKE '%' + @Keyword + '%'
                           OR ProverbDesp LIKE '%' + @Keyword + '%'
                        ";

            var proverbs = (await _db.QueryAsync<MmproverbsDto>(
                searchProverbsql,
                new { Keyword = searchkeyword }
            )).ToList();


            return new SearchMmProverbsResponseModel
            {
                IsSuccess = true,
                Message = "Proverbs searched successfully.",
                Data = proverbs
            };

        }

    }


    public class MmProverbTitleResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<MmproverbstitleDto> Data { get; set; }
    }
    public class MmProverbsResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<MmproverbsDto> Data { get; set; }
    }

    public class SearchMmProverbsResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<MmproverbsDto> Data { get; set; }
    }



}
