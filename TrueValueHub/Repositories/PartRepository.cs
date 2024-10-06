using Microsoft.AspNetCore.Mvc;
using TrueValueHub.Data;
using TrueValueHub.Interfaces;
using TrueValueHub.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using TrueValueHub.Dto;


namespace TrueValueHub.Repositories
{
    public class PartRepository: IPartRepository
    {
       
            private readonly ApiDbContext _context;
            private readonly IMapper _mapper;

            public PartRepository(ApiDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IEnumerable<Part>> GetAllParts()
            {
                return await _context.Parts.ToListAsync();
            }
            public async Task<List<Part>> GetPartByInternalPartNo(string id)
            {
                try
                {
                return await _context.Parts.Where(p => p.InternalPartNumber.Contains(id))
                     .Select(p => new Part
                     {
                         PartId = p.PartId,
                         InternalPartNumber = p.InternalPartNumber,
                         SupplierName = p.SupplierName,
                         DeliverySiteName = p.DeliverySiteName,
                         DrawingNumber = p.DrawingNumber,
                         IncoTerms = p.IncoTerms,
                         AnnualVolume = p.AnnualVolume,
                         BomQty = p.BomQty,
                         DeliveryFrequency = p.DeliveryFrequency,
                         LotSize = p.LotSize,
                         ManufacturingCategory = p.ManufacturingCategory,
                         PackagingType = p.PackagingType,
                         ProductLifeRemaining = p.ProductLifeRemaining,
                         PaymentTerms = p.PaymentTerms,
                         LifetimeQuantityRemaining = p.LifetimeQuantityRemaining,
                         ProjectId = p.ProjectId,
                         Materials = p.Materials
                     })
                     .ToListAsync();
            }
                catch (DbUpdateException dbEx)
                {
                    throw new Exception("A database error occurred while retrieving parts.", dbEx);
                }
                catch (Exception ex)
                {
                    throw new Exception("An unexpected error occurred in the repository while fetching parts.", ex);
                }
            }

            public async Task AddPart(Part part)
            {
                try
                {
                    await _context.Parts.AddAsync(part);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException dbEx)
                {
                    throw new Exception("A database error occurred while adding the part.", dbEx);
                }
                catch (Exception ex)
                {
                    throw new Exception("An unexpected error occurred in the repository while adding the part.", ex);
                }
            }
            public async Task<bool> UpdatePart(Part part, string internalPartNumber)
            {
                try
                {
                    var existingPart = await _context.Parts.FirstOrDefaultAsync(p => p.InternalPartNumber == internalPartNumber);

                    if (existingPart == null)
                    {
                        return false;
                    }

                    _mapper.Map(part, existingPart);

                    await _context.SaveChangesAsync();
                    return true;
                }
                catch (DbUpdateException dbEx)
                {
                    throw new Exception("A database error occurred while updating the part.", dbEx);
                }
                catch (Exception ex)
                {
                    throw new Exception("An unexpected error occurred in the repository while updating the part.", ex);
                }
            }


            public async Task<Part> GetPartById(int partId)
            {
                return await _context.Set<Part>().FindAsync(partId);
            }

    }
}
