using Microsoft.AspNetCore.Mvc;
using TrueValueHub.Dto;
using TrueValueHub.Interfaces;
using TrueValueHub.Models;

namespace TrueValueHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartController : ControllerBase
    {
        private readonly IPartService _partService;

        public PartController(IPartService partService)
        {
            _partService = partService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Part>>> GetParts()
        {
            var parts = await _partService.GetAllParts();
            return Ok(parts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Part>> GetPart(string id)
        {
            var part = await _partService.GetPartByInternalPartNo(id);

            if (part == null)
            {
                return NotFound();
            }

            return Ok(part);
        }

        [HttpPost]
        public async Task<ActionResult<Part>> PostPart(Part part)
        {
            await _partService.AddPart(part);
            return CreatedAtAction(nameof(GetPart), new { id = part.PartId }, part);
        }

        [HttpPut("{internalPartNumber}")]
        public async Task<IActionResult> PutPart(string internalPartNumber, Part part)
        {
            Console.WriteLine($"Route internalPartNumber: {internalPartNumber}, Body internalPartNumber: {part.InternalPartNumber}");


            var isUpdated = await _partService.UpdatePart(part,internalPartNumber);
            if (isUpdated == true)
            {
                return Ok(" parts Updated Successfully");
            }
            return BadRequest();
        }
      
    }
}
