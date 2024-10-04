using AutoMapper;
using TrueValueHub.Interfaces;
using TrueValueHub.Models;

namespace TrueValueHub.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly IMaterialRepository _materialRepository;
        private readonly IPartRepository _partRepository;
        private readonly IMapper _mapper;

        public MaterialService(IMaterialRepository materialRepository, IPartRepository partRepository, IMapper mapper)
        {
            _materialRepository = materialRepository;
            _partRepository = partRepository;
            _mapper = mapper;
        }

        // Get material by ID
        public async Task<Material> GetMaterialById(int materialId)
        {
            return await _materialRepository.GetMaterialById(materialId);
        }

        // Update material
        public async Task<bool> UpdateMaterial(Material material, int materialId)
        {
            /*if (materialId != material.MaterialId)
            {
                return false; // The ID in the body and URL do not match
            }

            var existingMaterial = await _materialRepository.GetMaterialById(materialId);
            if (existingMaterial == null)
            {
                return false; // Material not found
            }

            // Map the incoming changes
            _mapper.Map(material, existingMaterial);*/

            return await _materialRepository.UpdateMaterial(material, materialId);
        }

        // Delete material
        public async Task<bool> DeleteMaterial(int materialId)
        {
            return await _materialRepository.DeleteMaterial(materialId);
        }

        // Add material using part ID
        public async Task<bool> AddMaterialUsingPartId(int partId, Material newMaterial)
        {
            var part = await _partRepository.GetPartById(partId);
            if (part == null)
            {
                return false; // Part not found
            }

            newMaterial.PartId = partId;
            return await _materialRepository.AddMaterialUsingPartId(partId, newMaterial);
        }
    }

}
