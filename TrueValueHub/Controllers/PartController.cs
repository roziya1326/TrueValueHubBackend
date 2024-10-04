using Microsoft.AspNetCore.Mvc;
using TrueValueHub.Interfaces;
using TrueValueHub.Models;

namespace TrueValueHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartController : ControllerBase
    {
        private readonly IPartRepository _partRepository;
        private readonly IMaterialRepository _materialRepository;

        public PartController(IPartRepository partRepository, IMaterialRepository materialRepository)
        {
            _partRepository = partRepository;
            _materialRepository = materialRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Part>>> GetParts()
        {
            var parts = await _partRepository.GetAllParts();
            return Ok(parts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Part>> GetPart(string id)
        {
            var part = await _partRepository.GetPartByInternalPartNo(id);

            if (part == null)
            {
                return NotFound();
            }

            return Ok(part);
        }

        [HttpPost]
        public async Task<ActionResult<Part>> PostPart(Part part)
        {
            await _partRepository.AddPart(part);
            return CreatedAtAction(nameof(GetPart), new { id = part.PartId }, part);
        }

        [HttpPut("{internalPartNumber}")]
        public async Task<IActionResult> PutPart(string internalPartNumber, Part part)
        {
            Console.WriteLine($"Route internalPartNumber: {internalPartNumber}, Body internalPartNumber: {part.InternalPartNumber}");


            var isUpdated = await _partRepository.UpdatePart(part,internalPartNumber);
            if (isUpdated == true)
            {
                return Ok(" parts Updated Successfully");
            }
            return BadRequest();
        }
      
    }
}
