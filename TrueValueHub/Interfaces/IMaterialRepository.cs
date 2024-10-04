using TrueValueHub.Models;

namespace TrueValueHub.Interfaces
{
    public interface IMaterialRepository
    {
        Task<Material> GetMaterialById(int materialId);
        Task<bool> UpdateMaterial(Material material, int materialId);
        Task<bool> DeleteMaterial(int materialId);
        Task<bool> AddMaterialUsingPartId(int partId, Material newMaterial);

    }
}
