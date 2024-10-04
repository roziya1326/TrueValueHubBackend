using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrueValueHub.Dto;
using TrueValueHub.Models;
using TrueValueHub.Repositories;

namespace TrueValueHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            var projects = await _projectRepository.GetProjectsAsync();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProjectById(int id)
        {
            var project = await _projectRepository.GetProjectByIdAsync(id);

            if (project == null)
                return NotFound();

            return Ok(project);
        }

        [HttpPost]
        public async Task<ActionResult<Project>> PostProject(Project project)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdProject = await _projectRepository.AddProjectAsync(project);
            return CreatedAtAction(nameof(GetProjectById), new { id = createdProject.ProjectId }, createdProject);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(int id, Project project)
        {
            if (id != project.ProjectId)
                return BadRequest("Project ID mismatch");

            try
            {
                var updatedProject = await _projectRepository.UpdateProjectAsync(project);
                return Ok(updatedProject);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _projectRepository.GetProjectByIdAsync(id) == null)
                    return NotFound();
                else
                    throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var result = await _projectRepository.DeleteProjectAsync(id);

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

            var project = new Project
            {
                ProjectName = projectDto.ProjectName,
                Description = projectDto.Description,
                Parts = new List<Part>()
            };

            var savedProject = await _projectRepository.AddProjectAsync(project);

            foreach (var partDto in projectDto.Parts)
            {
                var part = new Part
                {
                    InternalPartNumber = partDto.InternalPartNumber,
                    SupplierName = partDto.SupplierName,
                    DeliverySiteName = partDto.DeliverySiteName,
                    DrawingNumber = partDto.DrawingNumber,
                    IncoTerms = partDto.IncoTerms,
                    AnnualVolume = partDto.AnnualVolume,
                    BomQty = partDto.BomQty,
                    DeliveryFrequency = partDto.DeliveryFrequency,
                    LotSize = partDto.LotSize,
                    ManufacturingCategory = partDto.ManufacturingCategory,
                    PackagingType = partDto.PackagingType,
                    ProductLifeRemaining = partDto.ProductLifeRemaining,
                    PaymentTerms = partDto.PaymentTerms,
                    LifetimeQuantityRemaining = partDto.LifetimeQuantityRemaining,
                    ProjectId = savedProject.ProjectId,
                    ParentId = partDto.ParentId 
                };

                savedProject.Parts.Add(part);
                var savedPart = await _projectRepository.AddPart(part);


                if (partDto.ChildParts != null)
                {
                    foreach (var childPartDto in partDto.ChildParts)
                    {
                        var childPart = new Part
                        {
                            InternalPartNumber = childPartDto.InternalPartNumber,
                            SupplierName = childPartDto.SupplierName,
                            DeliverySiteName = childPartDto.DeliverySiteName,
                            DrawingNumber = childPartDto.DrawingNumber,
                            IncoTerms = childPartDto.IncoTerms,
                            AnnualVolume = childPartDto.AnnualVolume,
                            BomQty = childPartDto.BomQty,
                            DeliveryFrequency = childPartDto.DeliveryFrequency,
                            LotSize = childPartDto.LotSize,
                            ManufacturingCategory = childPartDto.ManufacturingCategory,
                            PackagingType = childPartDto.PackagingType,
                            ProductLifeRemaining = childPartDto.ProductLifeRemaining,
                            PaymentTerms = childPartDto.PaymentTerms,
                            LifetimeQuantityRemaining = childPartDto.LifetimeQuantityRemaining,
                            ProjectId = savedProject.ProjectId,
                            ParentId = savedPart.PartId 
                        };

                        savedProject.Parts.Add(childPart);
                    }
                }
            }

            await _projectRepository.UpdateProjectPartsAsync(savedProject);

            return CreatedAtAction(nameof(CreateProject), new { id = savedProject.ProjectId }, savedProject);
        }
    }
}
