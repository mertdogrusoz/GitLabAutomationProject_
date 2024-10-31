using Microsoft.AspNetCore.Mvc;
using Services;

namespace GitLabAPI_2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MergeController : Controller
    {
        private readonly MergeService _mergeService;
        private readonly CsprojReader _csprojReader;

        public MergeController(MergeService mergeService, CsprojReader csprojReader)
        {
            _mergeService = mergeService;
            _csprojReader = csprojReader;
        }

        [HttpGet("{id}/merges")]
        public async Task<IActionResult> GetMerges(int id)
        {
            var merges = await _mergeService.GetMergeRequest(id);
            return Ok(merges);
        }
       

    }
}
