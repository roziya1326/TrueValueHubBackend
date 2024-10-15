using TrueValueHub.Models;

namespace TrueValueHub.Dto
{
    public class ProjectDto
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;


        public List<Part> Parts { get; set; } 
    }
}
