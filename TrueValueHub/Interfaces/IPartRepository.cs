using Microsoft.AspNetCore.Mvc;
using TrueValueHub.Models;

namespace TrueValueHub.Interfaces
{
    public interface IPartRepository
    {
        Task<IEnumerable<Part>> GetAllParts();
        Task<Part> GetPartById(int partId);
        Task<List<Part>> GetPartByInternalPartNo(string id);
        Task AddPart(Part part);
        Task<bool> UpdatePart(Part part,string internalPartNumber);
    }
}
