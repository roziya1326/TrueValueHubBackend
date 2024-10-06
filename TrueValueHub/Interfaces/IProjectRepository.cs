using System.Collections.Generic;
using System.Threading.Tasks;
using TrueValueHub.Dto;
using TrueValueHub.Models;

namespace TrueValueHub.Repositories
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetProjectsAsync();
        Task<Project> GetProjectByIdAsync(int id);
        Task<Project> UpdateProjectAsync(Project project);
        Task<bool> DeleteProjectAsync(int id);
        Task<Project> AddProject(Project project);
        Task UpdateProjectPartsAsync(Project project); 
        Task<Part> AddPart(Part part);

    }
}
