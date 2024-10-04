using Microsoft.AspNetCore.Mvc;
using TrueValueHub.Interfaces;
using TrueValueHub.Models;
using TrueValueHub.Repositories;

namespace TrueValueHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialController : ControllerBase
    {
        private readonly IMaterialRepository _materialRepository;

        public MaterialController(IMaterialRepository materialRepository)
        {
            _materialRepository = materialRepository;
        }
        [HttpPut("{materialId}")]
        public async Task<IActionResult> PutMaterial(int materialId, Material material)
        {
            Console.WriteLine($"Route materialId: {materialId}, Body MaterialId: {material.MaterialId}");

            var isUpdated = await _materialRepository.UpdateMaterial(material, materialId);
            if (isUpdated)
            {
                return Ok("Material updated successfully");
            }
            return BadRequest();
        }
        [HttpDelete("{materialId}")]
        public async Task<IActionResult> DeleteMaterial(int materialId)
        {
            Console.WriteLine($"Deleting material with ID: {materialId}");

            var isDeleted = await _materialRepository.DeleteMaterial(materialId);
            if (isDeleted)
            {
                return Ok("Material deleted successfully");
            }
            return NotFound("Material not found");
        }
        [HttpGet("{materialId}")]
        public async Task<IActionResult> GetMaterialById(int materialId)
        {
            Console.WriteLine($"Fetching material with ID: {materialId}");

            var material = await _materialRepository.GetMaterialById(materialId);
            if (material == null)
            {
                return NotFound("Material not found");
            }
            return Ok(material);
        }

        [HttpPost("{partId}")]
        public async Task<IActionResult> AddMaterial(int partId, [FromBody] Material newMaterial)
        {
            if (newMaterial == null)
            {
                return BadRequest("Material cannot be null.");
            }

            var isAdded = await _materialRepository.AddMaterialUsingPartId(partId, newMaterial);

            if (isAdded == true)
            {
                return Ok(newMaterial);
            }

            return NotFound("Part not found or material could not be added.");
        }


    }
}
