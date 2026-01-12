namespace TRTB4.MMProverbs.WebApi
{
    public class MmproverbstitleDto
    {
        public int TitleId { get; set; }
        public string TitleName { get; set; }
    }

    public class MmproverbsDto
    {
        public int TitleId { get; set; }
        public int ProverbId { get; set; }
        public string ProverbName { get; set; }
        public string ProverbDesp { get; set; }
    }
}
