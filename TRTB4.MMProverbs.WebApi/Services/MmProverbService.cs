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

            string mmProverbSql = "SELECT * FROM Tbl_Mmproverbs";
            var mmProverbs = (await _db.QueryAsync<MmproverbsDto>(mmProverbSql)).ToList();


            foreach (var t in titleSql)
            {
                t.title = mmProverbs.Where(a => a.TitleId == t.TitleId).ToList();

            }
            return new MmProverbTitleResponseModel
            {
                IsSuccess = true,
                Message = "MMProverbs retrieved successfully.",
                Data = title
            };

        }


        public async Task<MmProverbsResponseModel> GetProverbByIdAsync(int id)
        {


            string titleSql = @"
            SELECT * FROM Tbl_Mmproverbstitle
            WHERE TitleId = @TitleId";

            var title = await _db.QueryFirstOrDefaultAsync<MmproverbstitleDto>(
                titleSql,
                new { TitleId = id }
            );

            if (title is null)
            {

                return new MmProverbsResponseModel
                {
                    IsSuccess = false,
                    Message = "Pile not found.",

                };
            }
            string proverbSql = @"
            SELECT * FROM Tbl_Mmproverbs
            WHERE TitleId = @TitleId";

            var proverb = (await _db.QueryAsync<MmproverbsDto>(
                proverbSql,
                new { TitleId = id }
            )).ToList();

            proverb.Answers = proverb;

            return new MmProverbsResponseModel
            {
                IsSuccess = true,
                Message = "Pile retrieved successfully.",
                Data = proverb
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
        public MmproverbsDto Data { get; set; }
    }



}
