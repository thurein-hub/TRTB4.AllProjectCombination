using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TRTB4.MMProverbs.WebApi.Services;

namespace TRTB4.MMProverbs.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MmProverbController : ControllerBase
    {
        private readonly IMmProverbService _mmProverbeService;

        public MmProverbController(IMmProverbService pickAPileService)
        {
            _mmProverbeService = pickAPileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTitleAsync()
        {
            var title = await _mmProverbeService.GetAllTitleAsync();
            return Ok(title);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProverbById(int id)
        {
            var proverb = await _mmProverbeService.GetProverbByIdAsync(id);
            return Ok(proverb);
        }
    }
}
