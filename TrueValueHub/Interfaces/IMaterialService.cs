using TrueValueHub.Models;

namespace TrueValueHub.Interfaces
{
    public interface IMaterialService
    {
        Task<Material> GetMaterialById(int materialId);
        Task<bool> UpdateMaterial(Material material, int materialId);
        Task<bool> DeleteMaterial(int materialId);
        Task<bool> AddMaterialUsingPartId(int partId, Material newMaterial);

    }
}
