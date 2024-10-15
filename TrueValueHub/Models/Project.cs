using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrueValueHub.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }

        [Required]
        [StringLength(200)]
        public string ProjectName { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // One-to-Many relationship with Part
        public virtual ICollection<Part> Parts { get; set; } = new List<Part>();
    }
}
