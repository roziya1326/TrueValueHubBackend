using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrueValueHub.Dto;
using TrueValueHub.Interfaces;
using TrueValueHub.Models;
using TrueValueHub.Repositories;

namespace TrueValueHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            var projects = await _projectService.GetProjectsAsync();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProjectById(int id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);

            if (project == null)
                return NotFound();

            return Ok(project);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(int id, Project project)
        {
            if (id != project.ProjectId)
                return BadRequest("Project ID mismatch");

            try
            {
                var updatedProject = await _projectService.UpdateProjectAsync(project);
                return Ok(updatedProject);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _projectService.GetProjectByIdAsync(id) == null)
                    return NotFound();
                else
                    throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var result = await _projectService.DeleteProjectAsync(id);

            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpPost("upload")]
        public async Task<IActionResult> CreateProject([FromBody] ProjectDto projectDto)
        {
            if (projectDto == null)
            {
                return BadRequest("Project data is required.");
            }

            var savedProject = await _projectService.AddProject(projectDto);

            return CreatedAtAction(nameof(CreateProject), new { id = savedProject.ProjectId }, savedProject);
        }
    }
}
