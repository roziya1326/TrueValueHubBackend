using AutoMapper;
using TrueValueHub.Dto;
using TrueValueHub.Interfaces;
using TrueValueHub.Models;
using TrueValueHub.Repositories;

namespace TrueValueHub.Services
{
    public class ProjectService:IProjectService
    {
        
            private readonly IProjectRepository _projectRepository;
            private readonly IMapper _mapper;

            public ProjectService(IProjectRepository projectRepository, IMapper mapper)
            {
                _projectRepository = projectRepository;
                _mapper = mapper;
            }

            public async Task<IEnumerable<Project>> GetProjectsAsync()
            {
                return await _projectRepository.GetProjectsAsync();
            }

            public async Task<Project> GetProjectByIdAsync(int id)
            {
                return await _projectRepository.GetProjectByIdAsync(id);
            }

            public async Task<Project> UpdateProjectAsync(Project project)
            {
                return await _projectRepository.UpdateProjectAsync(project);
            }

            public async Task<bool> DeleteProjectAsync(int id)
            {
                return await _projectRepository.DeleteProjectAsync(id);
            }

            public async Task<Project> AddProject(ProjectDto projectDto)
            {
                var project = _mapper.Map<Project>(projectDto);

                var savedProject = await _projectRepository.AddProject(project);
                foreach (var partDto in projectDto.Parts)
                {
                    partDto.ProjectId = savedProject.ProjectId;               
                    var savedPart = await _projectRepository.AddPart(partDto);

                    if (partDto.ChildParts != null)
                    {
                    var childPartsList = partDto.ChildParts.ToList(); 
                    for (int i = 0; i < childPartsList.Count-2; i++)
                    {
                        var childPartDto = childPartsList[i];
                        childPartDto.ProjectId = savedProject.ProjectId;
                        childPartDto.ParentId = savedPart.PartId;
                        childPartDto.PartId = 0;
                        var savedChildPart = await _projectRepository.AddPart(childPartDto);
                    }
                }
            }
                return savedProject;
            }        

    }
}
