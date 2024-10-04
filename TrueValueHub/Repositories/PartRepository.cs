using Microsoft.AspNetCore.Mvc;
using TrueValueHub.Data;
using TrueValueHub.Interfaces;
using TrueValueHub.Models;
using Microsoft.EntityFrameworkCore;


namespace TrueValueHub.Repositories
{
    public class PartRepository: IPartRepository
    {
       
            private readonly ApiDbContext _context;

            public PartRepository(ApiDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Part>> GetAllParts()
            {
                return await _context.Parts.ToListAsync();
            }

            public async Task<List<Part>> GetPartByInternalPartNo(string id)
            {
            return await _context.Parts.Include(p => p.Materials).Where(p => p.InternalPartNumber.Contains(id)).ToListAsync();
            }

            public async Task AddPart(Part part)
            {
                await _context.Parts.AddAsync(part);
                await _context.SaveChangesAsync();
            }

            public async Task<bool> UpdatePart(Part part, string internalPartNumber)
            {

                if (internalPartNumber != part.InternalPartNumber)
                {
                    return false;
                }

                var existingPart = await _context.Parts.FirstOrDefaultAsync(p => p.InternalPartNumber == internalPartNumber);
                if (existingPart == null)
                {
                    Console.WriteLine("No part found with the given internalPartNumber.");

                    return false; 
                }

                existingPart.InternalPartNumber = part.InternalPartNumber;
                existingPart.SupplierName = part.SupplierName;
                existingPart.DeliverySiteName = part.DeliverySiteName;
                existingPart.DrawingNumber = part.DrawingNumber;
                existingPart.IncoTerms = part.IncoTerms;
                existingPart.AnnualVolume = part.AnnualVolume;
                existingPart.BomQty = part.BomQty;
                existingPart.DeliveryFrequency = part.DeliveryFrequency;
                existingPart.LotSize = part.LotSize;
                existingPart.ManufacturingCategory = part.ManufacturingCategory;
                existingPart.PackagingType = part.PackagingType;
                existingPart.ProductLifeRemaining = part.ProductLifeRemaining;
                existingPart.PaymentTerms = part.PaymentTerms;
                existingPart.LifetimeQuantityRemaining = part.LifetimeQuantityRemaining;
                existingPart.PartComplexity = part.PartComplexity;

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
            public async Task<Part> GetPartById(int partId)
            {
                return await _context.Set<Part>().FindAsync(partId);

            }

    }
}
