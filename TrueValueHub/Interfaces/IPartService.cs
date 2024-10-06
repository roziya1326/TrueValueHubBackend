using TrueValueHub.Models;

namespace TrueValueHub.Interfaces
{
    public interface IPartService
    {
        Task<IEnumerable<Part>> GetAllParts();
        Task<List<Part>> GetPartByInternalPartNo(string id);
        Task AddPart(Part part);
        Task<bool> UpdatePart(Part part, string internalPartNumber);
    }
}
