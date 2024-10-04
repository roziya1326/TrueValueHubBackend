using Microsoft.EntityFrameworkCore;
using TrueValueHub.Data;
using TrueValueHub.Interfaces;
using TrueValueHub.Models;

namespace TrueValueHub.Repositories
{
    public class MaterialRepository:IMaterialRepository
    {
        private readonly ApiDbContext _context;
        private readonly IPartRepository _partRepository;

        public MaterialRepository(ApiDbContext context, IPartRepository partRepository)
        {
            _context = context;
            _partRepository = partRepository;
        }

        public async Task<Material> GetMaterialById(int materialId)
        {
            return await _context.Set<Material>().FindAsync(materialId);
        }
        public async Task<bool> UpdateMaterial(Material material, int materialId)
        {
            if (materialId != material.MaterialId)
            {
                return false; 
            }

            var existingMaterial = await _context.Materials.FirstOrDefaultAsync(m => m.MaterialId == materialId);
            if (existingMaterial == null)
            {
                Console.WriteLine("No material found with the given materialId.");
                return false; 
            }

            existingMaterial.MaterialDescription = material.MaterialDescription;
            existingMaterial.Cost = material.Cost;
            existingMaterial.ProcessGroup = material.ProcessGroup;
            existingMaterial.SubProcess = material.SubProcess;
            existingMaterial.MaterialCategory = material.MaterialCategory;
            existingMaterial.Family = material.Family;
            existingMaterial.Grade = material.Grade;
            existingMaterial.Volume = material.Volume;
            existingMaterial.Price = material.Price;
            existingMaterial.Density = material.Density;
            existingMaterial.MoldBoxLength = material.MoldBoxLength;
            existingMaterial.MoldBoxWidth = material.MoldBoxWidth;
            existingMaterial.MoldBoxHeight = material.MoldBoxHeight;
            existingMaterial.MoldSandWeight = material.MoldSandWeight;
            existingMaterial.MSWR = material.MSWR;
            existingMaterial.NetMaterialCost = material.NetMaterialCost;
            existingMaterial.TotalMaterialCost = material.TotalMaterialCost;
            existingMaterial.PartId = material.PartId; 

            try
            {
                await _context.SaveChangesAsync();
                return true; 
            }
            catch (DbUpdateException ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteMaterial(int materialId)
        {
            var material = await _context.Materials.FindAsync(materialId);
            if (material == null)
            {
                return false;
            }

            _context.Materials.Remove(material);
            await _context.SaveChangesAsync(); 

            return true; 
        }
        public async Task<bool> AddMaterialUsingPartId(int partId, Material newMaterial)
        {
            var part = await _partRepository.GetPartById(partId);
            if (part == null)
            {
                return false;

            }
            try
            {
                await _context.Materials.AddAsync(newMaterial);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                return false;
            }
        }

    }
}
