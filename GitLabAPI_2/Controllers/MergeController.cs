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

        [HttpPost("{projectId}/merge-request")]
        public async Task<IActionResult> CreateMerge(int projectId, [FromBody] MergeRequest request)
        {
           
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

        [HttpPut("MergeMergeRequest")]
        public async Task<IActionResult> MergeMergeRequest([FromBody] MergeRequestDTO request)
        {
            try
            {
                if (request == null || request.Id <= 0 || request.MergeRequestIid <= 0)
                {
                    return BadRequest("Eksik veya hatalı veri gönderildi.");
                }

                var mergeResult = await _mergeService.MergeAMergeRequest(request.Id, request.MergeRequestIid, request.MergeCommitMessage, request.ShouldRemoveSourceBranch);
                return Ok(mergeResult);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Merge işlemi başarısız", error = ex.Message });
            }

        }
      
      

    }
}
