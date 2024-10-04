using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TrueValueHub.Models
{
    public class Material
    {
        [Key]
        public int MaterialId { get; set; }

        [Required]
        [StringLength(100)]
        public string MaterialDescription { get; set; }

        [Required]
        public int Cost { get; set; }
        [Required]
        [StringLength(100)]
        public string ProcessGroup { get; set; }
        [Required]
        [StringLength(100)]
        public string SubProcess { get; set; }
        [Required]
        [StringLength(100)]
        public string MaterialCategory {  get; set; }
        [Required]
        [StringLength(100)]
        public string Family { get; set; }
        [Required]
        [StringLength(100)]
        public string Grade { get; set; }
        [Required]
        public int Volume { get; set; }
        [Required]
        public int Price {  get; set; }
        [Required]
        public int Density { get; set; }
        [Required]
        public int MoldBoxLength {  get; set; }
        [Required]
        public int MoldBoxWidth {  get; set; }
        [Required]
        public int MoldBoxHeight {  get; set; }
        [Required]
        public int MoldSandWeight {  get; set; }
        [Required]
        public int MSWR {  get; set; }
        [Required]
        public int NetMaterialCost { get; set; }
        [Required]
        public int TotalMaterialCost {  get; set; }


        [ForeignKey("Part")]
        public int PartId { get; set; }

        public virtual Part Part { get; set; }
    }
}
