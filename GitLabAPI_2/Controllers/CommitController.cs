using EntityLayer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GitLabAPI_2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommitController : Controller
    {

        private readonly CommitService _commitService;

        public CommitController(CommitService commitService)
        {
            _commitService = commitService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("project/{projectId}/commites")]
        public async Task<IActionResult> GetCommites(int projectId)
        {
            var commits = await _commitService.GetCommites(projectId);
            if (commits == null)
            {
                return NotFound();
            }
            return Ok(commits);


        }

        [HttpPost("project/{projectId}/Createcommit")]
        public async Task<IActionResult> CreateCommites(int projectId, [FromBody] CreateCommitRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Branch) || string.IsNullOrWhiteSpace(request.CommitMessage))
            {
                return BadRequest("Branch name and commit message are required.");
            }

            var commit = await _commitService.CreateCommit(projectId, request.Branch, request.CommitMessage, request.Actions);
            if (commit == null)
            {
                return NotFound("Commit could not be created.");
            }

            return Ok(commit);
        }
       

    }


    public class CreateCommitRequest
    {
        public string Branch { get; set; }
        public string CommitMessage { get; set; }
        public List<ActionItem> Actions { get; set; } 
    }
    public class ModelEntity
    {
        public string Name  { get; set; }
        public int Age { get; set; }
    }
}
