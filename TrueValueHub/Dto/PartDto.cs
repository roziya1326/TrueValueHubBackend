﻿namespace TrueValueHub.Dto
{
    public class PartDto
    {
        public int PartId { get; set; }
        public string InternalPartNumber { get; set; }
        public string SupplierName { get; set; }
        public string DeliverySiteName { get; set; }
        public string DrawingNumber { get; set; }
        public string IncoTerms { get; set; }
        public int AnnualVolume { get; set; }
        public int BomQty { get; set; }
        public int DeliveryFrequency { get; set; }
        public int LotSize { get; set; }
        public string ManufacturingCategory { get; set; }
        public string PackagingType { get; set; }
        public int ProductLifeRemaining { get; set; }
        public string PaymentTerms { get; set; }
        public int LifetimeQuantityRemaining { get; set; }
        public int ProjectId { get; set; }

        public int ParentId { get; set; }
        public List<PartDto> ChildParts { get; set; } = new List<PartDto>();
    }
}
