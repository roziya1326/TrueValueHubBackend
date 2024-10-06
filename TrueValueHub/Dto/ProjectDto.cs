using TrueValueHub.Models;

namespace TrueValueHub.Dto
{
    public class ProjectDto
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }

        public List<Part> Parts { get; set; } 
    }
}
