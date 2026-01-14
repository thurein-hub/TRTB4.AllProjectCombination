using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TRTB4.MMProverbs.WebApi.Services;

namespace TRTB4.MMProverbs.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MmProverbController : ControllerBase
    {
        private readonly IMmProverbService _mmProverbsService;
        public MmProverbController(IMmProverbService mmProverbService)
        {
            _mmProverbsService = mmProverbService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTitleAsync()
        {
            var title = await _mmProverbsService.GetAllTitleAsync();
            return Ok(title);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProverbsByIdAsync(int id)
        {
            var proverb = await _mmProverbsService.GetProverbsByIdAsync(id);
            return Ok(proverb);
        }

       
        [HttpGet("search")]
        public async Task<IActionResult> SearchProverbsAsync(string searchkeyword)
        {
            var result = await _mmProverbsService.SearchProverbsAsync(searchkeyword);
            return Ok(result);
        }
    }
}
