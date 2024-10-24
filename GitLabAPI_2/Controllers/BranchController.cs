using EntityLayer;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GitLabAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BranchController : ControllerBase
    {
        private readonly BranchService _branchService;

        public BranchController(BranchService branchService)
        {
            _branchService = branchService;
        }

        [HttpGet("projects/{id}/branches")]
        public async Task<IActionResult> GetBranches(int id)
        {
            var branches = await _branchService.getBranch(id);
            return Ok(branches);

        }


        [HttpPost("projects/{id}/createbranch")]
        public async Task<IActionResult> CreateBranch(int id, [FromBody] CreateBranchRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Branch) || string.IsNullOrWhiteSpace(request.Ref))
            {
                return BadRequest("Branch name and ref are required.");
            }

            var branch = await _branchService.CreateBranch(id, request.Branch, request.Ref);

            if (branch == null)
            {
                return NotFound("Branch could not be created.");
            }

            return Ok(branch);


        }
    }
}
