using TrueValueHub.Interfaces;
using TrueValueHub.Models;

namespace TrueValueHub.Services
{
    public class PartService : IPartService
    {
        private readonly IPartRepository _partRepository;

        public PartService(IPartRepository partRepository)
        {
            _partRepository = partRepository;
        }

        public async Task<IEnumerable<Part>> GetAllParts()
        {
            return await _partRepository.GetAllParts();
        }

        public async Task<List<Part>> GetPartByInternalPartNo(string id)
        {
            try
            {
                var parts = await _partRepository.GetPartByInternalPartNo(id);
                if (parts == null || !parts.Any())
                {
                    throw new KeyNotFoundException("No parts found with the given internal part number.");
                }
                return parts;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching the part in the service.", ex);
            }
        }

        public async Task AddPart(Part part)
        {
            try
            {
                await _partRepository.AddPart(part);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the part in the service.", ex);
            }
        }

        public async Task<bool> UpdatePart(Part part, string internalPartNumber)
        {
            try
            {
                if (internalPartNumber != part.InternalPartNumber)
                {
                    throw new ArgumentException("The internal part number in the body does not match the one in the route.");
                }

                var isUpdated = await _partRepository.UpdatePart(part, internalPartNumber);
                if (!isUpdated)
                {
                    throw new Exception("No part found with the given internal part number.");
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the part in the service.", ex);
            }
        }
    }
}
