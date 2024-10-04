using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TrueValueHub.Data;
using TrueValueHub.Interfaces;
using TrueValueHub.Models;

namespace TrueValueHub.Repositories
{
    public class MaterialRepository : IMaterialRepository
    {
        private readonly ApiDbContext _context;
        private readonly IPartRepository _partRepository;
        private readonly IMapper _mapper;

        public MaterialRepository(ApiDbContext context, IPartRepository partRepository, IMapper mapper)
        {
            _context = context;
            _partRepository = partRepository;
            _mapper = mapper;
        }

        public async Task<Material> GetMaterialById(int materialId)
        {
            try
            {
                return await _context.Set<Material>().FindAsync(materialId);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching the material.", ex);
            }
        }

        public async Task<bool> UpdateMaterial(Material material, int materialId)
        {
            try
            {
                var existingMaterial = await _context.Materials.FirstOrDefaultAsync(m => m.MaterialId == materialId);

                if (existingMaterial == null)
                {
                    return false; 
                }

                _mapper.Map(material, existingMaterial);

                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("A database error occurred while updating the material.", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred in the repository while updating the material.", ex);
            }
        }

        public async Task<bool> DeleteMaterial(int materialId)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the material.", ex);
            }
        }

        public async Task<bool> AddMaterialUsingPartId(int partId, Material newMaterial)
        {
            try
            {
                var part = await _partRepository.GetPartById(partId);
                if (part == null)
                {
                    return false;
                }

                await _context.Materials.AddAsync(newMaterial);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("A database error occurred while adding the material.", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the material.", ex);
            }
        }
    }

}
