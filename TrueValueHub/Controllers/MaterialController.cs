using Microsoft.AspNetCore.Mvc;
using TrueValueHub.Interfaces;
using TrueValueHub.Models;
using TrueValueHub.Repositories;

namespace TrueValueHub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class MaterialController : ControllerBase
    {
        private readonly IMaterialService _materialService;

        public MaterialController(IMaterialService materialService)
        {
            _materialService = materialService;
        }

        [HttpGet("{materialId}")]
        public async Task<IActionResult> GetMaterialById(int materialId)
        {
            try
            {
                var material = await _materialService.GetMaterialById(materialId);
                if (material == null)
                {
                    return NotFound("Material not found.");
                }
                return Ok(material);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{materialId}")]
        public async Task<IActionResult> PutMaterial(int materialId, [FromBody] Material material)
        {
            try
            {
                var isUpdated = await _materialService.UpdateMaterial(material, materialId);
                if (isUpdated)
                {
                    return Ok("Material updated successfully.");
                }
                return BadRequest("Failed to update material.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{materialId}")]
        public async Task<IActionResult> DeleteMaterial(int materialId)
        {
            try
            {
                var isDeleted = await _materialService.DeleteMaterial(materialId);
                if (isDeleted)
                {
                    return Ok("Material deleted successfully.");
                }
                return NotFound("Material not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("{partId}")]
        public async Task<IActionResult> AddMaterial(int partId, [FromBody] Material newMaterial)
        {
            if (newMaterial == null)
            {
                return BadRequest("Material cannot be null.");
            }

            try
            {
                var isAdded = await _materialService.AddMaterialUsingPartId(partId, newMaterial);
                if (isAdded)
                {
                    return Ok(newMaterial);
                }
                return NotFound("Part not found or material could not be added.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

}
