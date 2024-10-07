using TrueValueHub.Models;

namespace TrueValueHub.Dto
{
    public class PartOutputDto
    {
            public int PartId { get; set; }
            public string InternalPartNumber { get; set; }
            public string SupplierName { get; set; }
            public string DeliverySiteName { get; set; }
            public string DrawingNumber { get; set; }
            public string IncoTerms { get; set; }
            public int AnnualVolume { get; set; }
            public int BomQty { get; set; }
            public string DeliveryFrequency { get; set; }
            public int LotSize { get; set; }
            public string ManufacturingCategory { get; set; }
            public string PackagingType { get; set; }
            public int ProductLifeRemaining { get; set; }
            public string PaymentTerms { get; set; }
            public int LifetimeQuantityRemaining { get; set; }
            public int ProjectId { get; set; }
            public PartComplexity PartComplexity { get; set; }

            public List<Material> Materials { get; set; } 

        
    }
}
