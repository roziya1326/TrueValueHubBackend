using TrueValueHub.Dto;
using TrueValueHub.Models;

namespace TrueValueHub.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> GetProjectsAsync();
        Task<Project> GetProjectByIdAsync(int id);
        Task<Project> UpdateProjectAsync(Project project);
        Task<bool> DeleteProjectAsync(int id);
        Task<Project> AddProject(ProjectDto projectDto);
    }
}
