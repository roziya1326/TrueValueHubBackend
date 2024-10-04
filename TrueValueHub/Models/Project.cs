using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

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


        // One-to-Many relationship with Part
        public virtual ICollection<Part> Parts { get; set; } = new List<Part>();
    }
}
