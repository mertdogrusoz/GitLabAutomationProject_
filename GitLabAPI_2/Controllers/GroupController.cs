
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using EntityLayer;

namespace GitLabAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly GitLabService _gitLabService;
        private readonly CsprojReader _csprojReader;
        private readonly string _baseProjectPath = @"C:\Users\yigit\source\repos\"; 

        public GroupController(GitLabService gitLabService, CsprojReader csprojReader)
        {
            _gitLabService = gitLabService;
            _csprojReader = csprojReader    ;
        }

        [HttpGet("groups")]
        public async Task<IActionResult> GetGroups()
        {
            var groups = await _gitLabService.GetGroups();
            return Ok(groups);
        }

        [HttpGet("groups/{groupId}/projects")]
        public async Task<IActionResult> GetProjectsByGroupId(int groupId)
        {
            var projects = await _gitLabService.GetProjectByGroup(groupId);
            return Ok(projects);
        }

        [HttpGet("groups/{groupId}/projects/packages")]
        public async Task<IActionResult> GetNuGetPackagesByGroupId(int groupId, [FromQuery] string? searchTerm = null)
        {
            var projects = await _gitLabService.GetProjectByGroup(groupId);

            if (projects == null || projects.Count == 0)
            {
                return NotFound("No projects found for this group.");
            }

            var allPackagesWithProject = new List<NuGetPackage>();

            foreach (var project in projects)
            {
                var projectPath = Path.Combine(_baseProjectPath, project.name, project.name, $"{project.name}.csproj");

                if (!System.IO.File.Exists(projectPath))
                {
                    return NotFound($"Project file not found: {projectPath}");
                }

                var packages = _csprojReader.GetNuGetPackagesFromCsproj(projectPath);

                foreach (var package in packages)
                {
                  
                    allPackagesWithProject.Add(new NuGetPackage
                    {
                        ProjectName = project.name,
                        PackageId = package.PackageId,
                        Version = package.Version
                    });
                }
            }

          
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                allPackagesWithProject = allPackagesWithProject
                    .Where(p => p.PackageId.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            return Ok(allPackagesWithProject);
        }
        [HttpPut("update-package-version")]
        public IActionResult UpdatePackageVersion([FromQuery] string projectName, [FromQuery] string packageId, [FromQuery] string version)
        {
            if (string.IsNullOrEmpty(projectName) || string.IsNullOrEmpty(packageId) || string.IsNullOrEmpty(version))
            {
                return BadRequest("Project name, package ID, and version must be provided.");
            }

           
            var projectFilePath = Path.Combine(_baseProjectPath, projectName, projectName, $"{projectName}.csproj");

          
            var packageUpdates = new Dictionary<string, string>
    {
        { packageId, version }
    };

          
            var isUpdated = _gitLabService.UpdatePackageVersion(projectFilePath, packageUpdates);

            if (isUpdated)
            {
                return Ok("Package version updated successfully.");
            }

            return NotFound("Package or project file not found, or version could not be updated.");
        }

    }
}
