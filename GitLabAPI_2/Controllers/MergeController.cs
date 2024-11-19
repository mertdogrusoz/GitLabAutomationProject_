using EntityLayer;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Security.Cryptography.X509Certificates;

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

        public async Task<IActionResult> GetOpenMerges(int id)
        {
            return Ok();

        }

        [HttpPost("{projectId}/merge-request")]
        public async Task<IActionResult> CreateMerge(int projectId, [FromBody] MergeRequest request)
        {
            // Gelen istekte sourceBranch, targetBranch ve title bilgileri zorunludur
            if (request == null || string.IsNullOrEmpty(request.SourceBranch) || string.IsNullOrEmpty(request.TargetBranch) || string.IsNullOrEmpty(request.Title))
            {
                return BadRequest("Source branch, target branch, and title are required.");
            }

          
            var mergeRequest = await _mergeService.CreateMerge(projectId, request.SourceBranch, request.TargetBranch, request.Title);

            if (mergeRequest == null)
            {
                return BadRequest("Merge request could not be created.");

            }

            return Ok(mergeRequest);
        }
        

    }
}
