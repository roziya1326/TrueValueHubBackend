using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TrueValueHub.Models
{
    public class Part
    {
        [Key]
        public int PartId { get; set; }

        [Required]
        [StringLength(100)]
        public string InternalPartNumber { get; set; } 

        [Required]
        [StringLength(100)]
        public string SupplierName { get; set; } 

        [Required]
        [StringLength(100)]
        public string DeliverySiteName { get; set; }

        [StringLength(50)]
        public string DrawingNumber { get; set; } 

        [Required]
        [StringLength(10)]
        public string IncoTerms { get; set; } 

        [Required]
        public int AnnualVolume { get; set; } 

        [Required]
        public int BomQty { get; set; } 

        [Required]
        public int DeliveryFrequency { get; set; } 

        [Required]
        public int LotSize { get; set; } 

        [Required]
        [StringLength(50)]
        public string ManufacturingCategory { get; set; } 

        [Required]
        [StringLength(50)]
        public string PackagingType { get; set; } 

        [Required]
        public int ProductLifeRemaining { get; set; } 

        [Required]
        [StringLength(20)]
        public string PaymentTerms { get; set; } 

        [Required]
        public int LifetimeQuantityRemaining { get; set; }
        [Required]
        public int ProjectId { get; set; }

        [JsonIgnore]
        public virtual Project Project { get; set; }
        public int ParentId { get; set; } = 0; 

        [JsonIgnore]
        public virtual Part ParentPart { get; set; } 

        public virtual ICollection<Part> ChildParts { get; set; } = new List<Part>();  
        public virtual ICollection<Material> Materials { get; set; } = new List<Material>();

        [Required]
        public PartComplexity PartComplexity { get; set; }

    }
}
